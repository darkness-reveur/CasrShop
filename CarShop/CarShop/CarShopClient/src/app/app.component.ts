import { Component, DoCheck, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from './models/User';
import { UserService } from './services/user.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit, DoCheck {
  
  title = 'CarShopClient';

  isDataLoaded: boolean = false

  isAuthorized: boolean = false

  user: User

  constructor(
    private userService: UserService,
    private route: Router,) 

    {
      console.dir('ConsoleStartWork');
    this.userService.getUser().subscribe(result => {
      this.user = result
      this.isDataLoaded = true
    }, error => {
      if (location.pathname !== '/register'
      && location.pathname !== '/login') {
        this.route.navigate(['/login'])
        console.dir('Console main end work');
      }
    })
    
  }

  ngOnInit(): void {
    console.dir('onInitMain')
  }

  ngDoCheck(){
    if(!this.isAuthorized
      && this.isDataLoaded
      && location.pathname !== '/register'
      && location.pathname !== '/login') {
      this.userService.getUser().subscribe(result => {
        this.user = result
        this.isDataLoaded = true
        this.isAuthorized = true
      })
    }
  }
}
