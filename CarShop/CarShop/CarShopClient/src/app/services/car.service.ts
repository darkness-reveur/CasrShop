import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Car } from '../models/Car';

@Injectable({
  providedIn: 'root'
})
export class CarService {

  private url = '/api/Product/';

  constructor(private http: HttpClient) { }

  GetAllProducts(): Observable<Car[]> {
    return this.http.get<Car[]>(`${this.url}GetAllCars`)
  }

  GetProductById(id: number): Observable<Car> {
    return this.http.get<Car>(`${this.url}GetProduct/?id=${id}`)
  }

  AddNewProduct(product: Car): Observable<Car> {
    return this.http.post<Car>(`${this.url}`, product)
  }

  UpdateProduct(product: Car): Observable<Car> {
    return this.http.put<Car>(`${this.url}`, product)
  }

  DeleteProduct(productId: number): Observable<void> {
    return this.http.delete<void>(`${this.url}?productId=${productId}`)
  }
}
