import { Component, Input, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/auth/services/AuthServise';
import { User, UserRoles } from 'src/app/models/user';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {

  user: User

  isUserAdmin: boolean = false

  constructor(
    private dialog: MatDialog,
    private router: Router,
    private userService: UserService,
    private authService: AuthService) { }


  loggedIn = false


  ngOnInit(): void {
    console.dir('OnInitNavBar');
    this.userService.getUser().subscribe(result => {
      if (result) {
        console.dir(result);
        this.user = result;
        this.loggedIn = true;
        if (result.role === 1) {
          this.isUserAdmin = true
        }
      }
      else {
        this.loggedIn = false;
      }
    })
  }

  /* ngOnInit(): void {
    console.dir('NavBarStart')

    if (this.user?.role === UserRoles.Admin) {
      this.isUserAdmin = true;
    }
    if (this.user) {
      this.loggedIn = true
    }
  } */

  louOut() {
    console.dir('Logout')
    this.authService.LogOut().subscribe(result => {
      console.dir(result);
      if (result) {
        this.router.navigateByUrl('/home');
      }
    });

    this.goToHome();
  }

  goToProfile() {
    console.dir('startGoToProfile')
    this.router.navigateByUrl('/profile');
    console.dir('endGoToProfile')
  }

  goToHome() {
    this.router.navigateByUrl('')
  }

  goToOrder() {
    this.router.navigateByUrl('/order')
  }

  goToLogin() {
    this.router.navigateByUrl('/login');
  }

  goToCart() {
    this.router.navigateByUrl('/cart');
  }
}