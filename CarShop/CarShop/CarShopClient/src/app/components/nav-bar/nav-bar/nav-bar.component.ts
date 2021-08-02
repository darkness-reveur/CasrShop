import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {

  constructor(
    private userService: UserService,
    private router: Router,
  ) { }
     loggedIn = false 
  ngOnInit(): void {
    console.dir('OnInitNavBar');
      this.userService.getUser().subscribe(result => {
        if(result){
          this.loggedIn = true;
        }
        else{
          this.loggedIn = false;
        }
      })
    }
  goToProfile() {
    this.router.navigateByUrl('/');
  }
}
