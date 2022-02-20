import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})

export class AuthService {
    loginPath = 'https://localhost:5001/identity/login';
    registerPath = 'https://localhost:5001/identity/register';
    constructor(private httpClient: HttpClient) { }

    register(data: any): Observable<any> {
        return this.httpClient.post(this.registerPath, data);
    }
    login(data: any): Observable<any> {
        return this.httpClient.post(this.loginPath, data);
    }
    setAuthToken(authToken: string){
        localStorage.setItem('authToken', authToken);
    }
    setUserId(userId: string) {
        localStorage.setItem('userId', userId);
    }
}