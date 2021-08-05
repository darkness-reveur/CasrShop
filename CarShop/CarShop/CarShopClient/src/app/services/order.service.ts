import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
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
}
