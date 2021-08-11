import { AfterContentInit, Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Cart } from 'src/app/models/Cart';
import { Order } from 'src/app/models/Order';
import { User } from 'src/app/models/user';
import { OrderService } from 'src/app/services/order.service';
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
    private orderService: OrderService,
    private router: Router) { }

  isChecked: boolean = true;

  ngOnInit(): void {
    console.dir('startMainProfile')
    this.userService.getUser().subscribe(result => {
      if (result) {
        this.isChecked = true;

        this.user = result;

        console.dir('asdfsda')
      
        console.dir(result);


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

      console.dir('fvwbdwbdhwbdhbwdhbwdbwhdbhwbdhbwdbwhdbwhbdhwbdwbhd')
      this.cart = result.cart

      console.dir(this.cart)
    });
    console.dir(this.isChecked)
  }
  
  getCart() {
    this.userService.getCart().subscribe(result => {
      if(result) {
        this.cart = result
      }
    })
    console.dir('this.cart')
    console.dir(this.cart.cartCars)
  }

  getOrders() {
    this.orderService.GerAllUserOrders();
  }

  updatePersonalInformation() {
    console.dir("wadwadawd")
    console.dir(this.user.name)
    this.userService.updateUser(this.user).subscribe(result => { },
      error => {
        console.log(error.message);
      })
      console.dir(this.user.name)
  }
}
