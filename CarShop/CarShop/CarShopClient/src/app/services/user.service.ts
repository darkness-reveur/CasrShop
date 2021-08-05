import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Cart } from '../models/Cart';
import { User } from '../models/User';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private url = '/api/User/';

  constructor(public http: HttpClient) { }

  getUser(): Observable<User> {
    return this.http.get<User>(`${this.url}`);
  }

  updateUser(user: User): Observable<User> {
    return this.http.put<User>(`${this.url}`, user);
  }

  DeleteCarFromCart(cartId: number): Observable<Cart> {
    return this.http.put<Cart>(`${this.url}UpdateCart`, cartId)
  }

  getAll(): Observable<User[]> {
    return this.http.get<User[]>(`${this.url}GetAll`);
  }

  addCarToCart(carId: number): Observable<boolean> {
    return this.http.put<boolean>(`${this.url}AddCarToCart`, carId);
  }

  getCart(): Observable<Cart> {
    return this.http.get<Cart>(`${this.url}GetCart`)
  }
}
