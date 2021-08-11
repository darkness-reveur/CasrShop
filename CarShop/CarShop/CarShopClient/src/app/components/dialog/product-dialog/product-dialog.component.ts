import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Car } from 'src/app/models/Car';
import { BrandService } from 'src/app/services/brand.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-product-dialog',
  templateUrl: './product-dialog.component.html',
  styleUrls: ['./product-dialog.component.scss']
})
export class ProductDialogComponent implements OnInit {

  constructor(
    private router: Router,
    private userService: UserService,
    private brandService: BrandService ) { }

  ngOnInit(): void {
    this.userService.getUser().subscribe(result => {
      if (result) {
        this.router.navigateByUrl('');
      }
    },
      error => {
      })
  }
  giveAllBrands

}
