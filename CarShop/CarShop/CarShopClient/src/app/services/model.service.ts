import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CarModel } from '../models/CarModel';

@Injectable({
  providedIn: 'root'
})
export class ModelService {

  private url = '/api/Model';
  
  constructor(private http: HttpClient) { }

  GetAllModels(): Observable<CarModel[]> {
    return this.http.get<CarModel[]>(`${this.url}`);
  }

  AddNewModel(carModel: CarModel): Observable<CarModel> {
    return this.http.post<CarModel>(`${this.url}`, carModel);
  }

  DeleteModel(carModelId): Observable<void> {
    return this.http.delete<void>(`${this.url}?carModelId=${carModelId}`);
  }
}
