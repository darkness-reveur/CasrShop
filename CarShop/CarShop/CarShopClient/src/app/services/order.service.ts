import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Cart } from '../models/Cart';
import { Order } from '../models/Order';


@Injectable({
  providedIn: 'root'
})
export class OrderService {

  private url = '/api/User/';

  constructor(public Http: HttpClient) { }

  GerAllUserOrders(): Observable<Order[]> {
    return this.Http.get<Order[]>(`${this.url}GetAllUserOrders`);
  }

  CreateOrder(cart: Cart): Observable<boolean> {
    return this.Http.post<boolean>(`${this.url}CreateOrder`,cart);
  }
}
