import { Component, OnInit } from '@angular/core';
import { Car } from 'src/app/models/Car';
import { CarBrand } from 'src/app/models/CarBrand';
import { CarModel } from 'src/app/models/CarModel';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {

  model: CarModel;
  
  brand: CarBrand;

  cars: Car[]

  isValideData: boolean;

  constructor() { }

  ngOnInit(): void {

  }

}
