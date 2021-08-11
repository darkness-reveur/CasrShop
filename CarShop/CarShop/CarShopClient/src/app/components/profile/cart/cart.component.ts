import { Component, Input, OnInit } from '@angular/core';
import { Car } from 'src/app/models/Car';
import { Cart } from 'src/app/models/Cart';
import { User } from 'src/app/models/user';
import { OrderService } from 'src/app/services/order.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit {

  @Input() cart: Cart

  @Input() user: User;

  supTotalPrice: number = 0

  constructor(
    private userService: UserService,
    private orderService: OrderService
  ) { }

  ngOnInit(): void {
    this.userService.getUser().subscribe(result => {
      if(result) {
        this.user = result

        this.cart = result.cart
      }
    })
    console.dir(this.cart)
    console.dir('CartOnInitStart');
    console.log(this.user)
    console.dir(this.supTotalPrice)
    console.dir('CartOnInitEnd');
  }

 /*  colculatePrice(cart: Cart) {
    let result: number = 0

    console.dir(cart)

    cart.cartCars.forEach(product => {
      result += product.car.price
    })

    return result
  } */

  clearCart() {
    this.cart.cartCars = null
  }

  addOrder() {
    this.orderService.CreateOrder(this.cart);

    this.clearCart()
  }

  deleteCarFromCard(car: Car) {

  }
}
