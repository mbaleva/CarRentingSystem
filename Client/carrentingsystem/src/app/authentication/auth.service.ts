import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: 'root'
})

export class AuthService {
    loginPath = environment.identityUrl + '/identity/login';;
    registerPath = environment.identityUrl + '/identity/register';
    constructor(private httpClient: HttpClient) { }

    register(data: any): Observable<any> {
        return this.httpClient.post(this.registerPath, data);
    }
    login(data: any): Observable<any> {
        return this.httpClient.post(this.loginPath, data);
    }
    getToken() : string | null {
        return localStorage.getItem('authToken');
    }
    setAuthToken(authToken: string){
        localStorage.setItem('authToken', authToken);
    }
    setUserId(userId: string) {
        localStorage.setItem('userId', userId);
    }
}