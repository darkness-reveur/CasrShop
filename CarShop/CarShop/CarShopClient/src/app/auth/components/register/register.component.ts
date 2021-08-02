import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserRoles } from 'src/app/models/User';
import { UserService } from 'src/app/services/user.service';
import { RegisterData } from '../../models/registerData';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AuthService } from '../../services/AuthServise';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  
  data: RegisterData = {
    user: {
      id: 0,
      name: "",
      email: "",
      mobilePhoneNumber: "",
      age: null,
      role: UserRoles.AuthorizedUser,
      orders: [],
      carId: null,
      car: null,
      cart: null
    },
    login: "",
    password: ""
  }

  isLoginFree: boolean;

  timerId: any;

  constructor(private authService: AuthService,
    private router: Router,
    private _snackBar: MatSnackBar,
    private userService: UserService,) { }

  ngOnInit(): void {
    console.dir('onInitRegister')
    this.userService.getUser().subscribe(result => {
      if(result) {
          this.router.navigateByUrl('');
      }
    },
    error =>{

    })
    console.dir('onInitREgEnd')
  }

  keyUp(): void {
    console.dir('KeyUp');
    if(this.timerId) {
      clearTimeout(this.timerId)
      console.dir('KeyAppIsEnd');
    }

    this.timerId = setTimeout(this.checkLoginDelegate, 800);
  }

  checkLoginDelegate = () => { this.checkLogin() }

  checkLogin() {
    console.dir(this.isLoginFree);
      console.dir('checkLogin');
    this.authService.ChekUserLogin(this.data.login).subscribe(result => {
      if(!result) {

        this.isLoginFree = false;
        console.dir(this.isLoginFree);

      }
      else {
        this.isLoginFree = true;
        console.dir(this.isLoginFree);
      }
      console.dir('checkLoginEnd');
    })
  }

  register() {
    console.dir('register');
    this.authService.Register(this.data).subscribe(result => {
      if(result) {
        this.router.navigateByUrl('login')
        console.dir('Register is end');
      }
      else {
        this._snackBar.open('Error with registration')
        console.dir('Register is faled');
      }
    })
  }

  cancelRegistration() {
    this.router.navigateByUrl('');
  }
}
