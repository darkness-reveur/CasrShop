import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './auth/components/login/login.component';
import { RegisterComponent } from './auth/components/register/register.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { CarService } from './services/car.service';
import { ModelService } from './services/model.service';
import { BrandService } from './services/brand.service';
import { UserService } from './services/user.service';
import { MaterialModule } from './material.module';
import { NavBarComponent } from './components/nav-bar/nav-bar/nav-bar.component';
import { ProfileComponent } from './components/profile/profile/profile.component';
import { HomeComponent } from './components/home/home.component';
import { CartComponent } from './components/profile/cart/cart.component';
import { OrderComponent } from './components/profile/order/order.component';
import { ProductDialogComponent } from './components/dialog/product-dialog/product-dialog.component';
import { ProfileMainComponent } from './components/profile/profile-main.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    NavBarComponent,
    ProfileComponent,
    HomeComponent,
    ProductDialogComponent,
    CartComponent,
    OrderComponent,
    ProfileMainComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    MaterialModule
  ],
  exports: [
    MatSnackBarModule
  ],
  providers: [
    CarService,
    ModelService,
    BrandService,
    UserService,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
