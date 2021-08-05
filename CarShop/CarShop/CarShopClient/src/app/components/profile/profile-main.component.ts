import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Cart } from 'src/app/models/Cart';
import { Order } from 'src/app/models/Order';
import { User } from 'src/app/models/user';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-profile-main',
  templateUrl: './profile-main.component.html',
  styleUrls: ['./profile-main.component.scss']
})
export class ProfileMainComponent implements OnInit {

  user: User

  cart: Cart

  orders: Order[]

  users: User[] = []

  constructor(
    private userService: UserService,
    private router: Router) { }

  isChecked: boolean;

  ngOnInit(): void {
    this.userService.getUser().subscribe(result => {
      if (result) {
        this.isChecked = true;

        this.user = result;

        console.log(result);

        if (this.user.orders) {
          this.getCart();
        }

        if (result.role === 1) {
          this.userService.getAll().subscribe(result => {
            if (result) {
              this.users = result;
            }
          },
            error => {
              console.log(error.message)
            })
        }
      } else {
        this.router.navigateByUrl('login');
      }
    });
  }

  getCart() {
    this.userService.getCart()
  }

  getOrders() {

  }

  updatePersonalInformation() {
    console.dir('UpdateUserStart');
    this.userService.updateUser(this.user).subscribe(result => { },
      error => {
        console.log(error.message);
      })
    console.dir('UpdateUserEnd')
  }
}
