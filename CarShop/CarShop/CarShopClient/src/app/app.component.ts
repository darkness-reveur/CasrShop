import { Component, DoCheck, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from './models/User';
import { UserService } from './services/user.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  title = 'CarShopClient';

  user: User

  isAuthorized: boolean = false

  isDataLoaded: boolean = false

  constructor(
    private userService: UserService,
    private router: Router) {
    this.userService.getUser().subscribe(result => {
      this.user = result
      this.isDataLoaded = true;
    }, error => { })
  }

  ngOnInit(): void {
    console.dir('onInit')
  }

  /* ngDoCheck() {
    if(!this.isAuthorized
      && location.pathname !== '/register'
      && location.pathname !== '/login') {
        this.userService.getUser().subscribe(result => {
          this.user = result;
          this.isAuthorized = true;
        })
    }
  } */
}

