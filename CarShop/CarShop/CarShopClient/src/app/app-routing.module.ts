import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './auth/components/login/login.component';
import { RegisterComponent } from './auth/components/register/register.component';
import { HomeComponent } from './components/home/home.component';
import { CartComponent } from './components/profile/cart/cart.component';
import { OrderComponent } from './components/profile/order/order.component';
import { ProfileMainComponent } from './components/profile/profile-main.component';

const routes: Routes = [
  {
    path: "",
    component: HomeComponent,
  },
  {
    path: "login",
    component: LoginComponent,
  },
  {
    path: "register",
    component: RegisterComponent
  },
  {
    path: "profile",
    component: ProfileMainComponent
  },
  {
    path: "cart",
    component: CartComponent
  },
  {
    path: "order",
    component: OrderComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
