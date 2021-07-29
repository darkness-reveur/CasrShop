import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { UserService } from 'src/app/services/user.service';
import { LoginData } from '../../models/loginData';
import { AuthService } from '../../services/AuthServise';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  data: LoginData = {
    id: 0,
    login: '',
    password: '',
    userId: 0
  }

  constructor(private snackBar: MatSnackBar,
    private userService: UserService,
    private router: Router,
    private authService: AuthService) {  }

  ngOnInit(): void {
    this.userService.getUser().subscribe(result => {
      if(result) {
        this.router.navigateByUrl('');
      }
    },
    error => {
    })
  }

  logIn() {
    this.authService.LogIn(this.data).subscribe(result => {
      if(result) {
        this.router.navigateByUrl('');
      }
      else {
        this.snackBar.open('Login or password was incorrect')
      }
    })
  }

}
