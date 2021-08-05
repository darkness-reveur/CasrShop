import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CarBrand } from '../models/CarBrand';

@Injectable({
  providedIn: 'root'
})
export class BrandService {

  private url = '/api/Brand/';

  constructor(private http: HttpClient) { }

  GetAllBrands(): Observable<CarBrand[]> {
    return this.http.get<CarBrand[]>(`${this.url}`);
  }

  AddNewBrand(carBrand: CarBrand): Observable<CarBrand> {
    return this.http.post<CarBrand>(`${this.url}`, carBrand);
  }

  DeleteBrand(carBrandId: number): Observable<void> {
    return this.http.delete<void>(`${this.url}?carBrandId=${carBrandId}`);
  }
}
