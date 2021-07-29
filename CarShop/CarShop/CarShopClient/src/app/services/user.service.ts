import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
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

  updateUser(user: User) {
    return this.http.put<void>(`${this.url}`, user);
  }

  getAll(): Observable<User[]> {
    return this.http.get<User[]>(`${this.url}GetAll`);
  }
}
