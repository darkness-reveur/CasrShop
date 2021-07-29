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
      phoneNumber: "",
      age: null,
      userRole: UserRoles.NotAuthorizedUser,
      orders: [],
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
    private userService: UserService) { }

  ngOnInit(): void {
    this.userService.getUser().subscribe(result => {
      if(result) {
          this.router.navigateByUrl('');
      }
    },
    error =>{

    })
  }

  keyUp(): void {
    if(this.timerId) {
      clearTimeout(this.timerId)
    }

    this.timerId = setTimeout(this.checkLoginDelegate, 800);
  }

  checkLoginDelegate = () => { this.checkLogin() }

  checkLogin() {
    this.authService.ChekUserLogin(this.data.login).subscribe(result => {
      if(!result) {

        this.isLoginFree = false;

        this._snackBar.open('Login is already served', '', {
          duration: 2000
        })
      }
      else {
        this.isLoginFree = true;
      }
    })
  }

  register() {
    this.authService.Register(this.data).subscribe(result => {
      if(result) {
        this.router.navigateByUrl('')
      }
      else {
        this._snackBar.open('Error with registration')
      }
    })
  }

  cancelRegistration() {
    this.router.navigateByUrl('');
  }
}
