import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Car } from 'src/app/models/Car';
import { User } from 'src/app/models/User';
import { CarService } from 'src/app/services/car.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  products: Car[]

  user: User

  newProduct: Car

  constructor(
    public dialog: MatDialog,
    private carService: CarService
  ) { }

  ngOnInit(): void {
  }

}
