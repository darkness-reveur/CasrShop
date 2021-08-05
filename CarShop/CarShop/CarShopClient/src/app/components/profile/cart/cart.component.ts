import { Component, Input, OnInit } from '@angular/core';
import { Cart } from 'src/app/models/Cart';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit {

  @Input() cart: Cart

  constructor(
    private userService: UserService
  ) { }

  ngOnInit(): void {
    console.dir('CartOnInitStart');
    /* this.userService.getCart().subscribe(result => {
      if(result){
        this.cart = result;
        console.dir(this.cart);
      }else{
        console.dir('Can\'t get cart');
      }
    }) */
    console.dir('CartOnInitEnd');
  }

  getCarsInCard() {
    this.cart.cartCars;
  }

  confirmOrder() {

  }
}
