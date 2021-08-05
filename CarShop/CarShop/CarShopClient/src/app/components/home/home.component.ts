import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { Car } from 'src/app/models/Car';
import { CarBrand } from 'src/app/models/CarBrand';
import { CarModel } from 'src/app/models/CarModel';
import { Cart } from 'src/app/models/Cart';
import { User } from 'src/app/models/User';
import { BrandService } from 'src/app/services/brand.service';
import { CarService } from 'src/app/services/car.service';
import { ModelService } from 'src/app/services/model.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  products: Car[]

  user: User

  cart: Cart

  newProduct: Car

  constructor(
    public dialog: MatDialog,
    private carService: CarService,
    private userService: UserService,
    private router: Router) { }

  ngOnInit(): void {
    console.dir('HomeOnInitStart')

    this.carService.GetAllProducts().subscribe(result => {
      this.products = result
    })

    this.userService.getUser().subscribe(result => {

      if (result) {
        this.user = result;
        console.dir('User is authorizade');
      }
      else {
        console.dir('User not authorizade');
      }
    })



    console.dir('HomeOnInitEnd')
  }

  addToCart(cartId: number) {
    if (!this.user) {
      this.router.navigateByUrl('/login');
    }
    console.dir('Car Added...');
    this.userService.addCarToCart(cartId).subscribe(result => {
      console.dir(result);
    })
  }
}
