import { HttpClient } from '@angular/common/http';
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { LoginData } from '../models/loginData';
import { RegisterData } from '../models/registerData';


@Injectable({
    providedIn: 'root'
})
export class AuthService {
    constructor(public http: HttpClient) { }

    private url = '/api/auth/';

    Register(data: RegisterData): Observable<boolean> {
        return this.http.post<boolean>(`${this.url}`, data);
    }

    LogIn(data: LoginData): Observable<boolean> {
        return this.http.put<boolean>(`${this.url}`, data);
    }

    ChekUserLogin(login: string): Observable<boolean> {
        return this.http.get<boolean>(`${this.url}IsLoginFree/${login}`)
    }

    LogOut(): Observable<boolean> {
        return this.http.get<boolean>(`${this.url}LogOut`);
    }
}