import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from '../authentication/auth.service';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {
    token: string | null;
    constructor(private authService: AuthService) {
        this.token = this.authService.getToken();
    }
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        if(this.token) {
            let reqHeaders = req.headers.append('Authorization', `Bearer ${this.token}`);
            req = req.clone({
                headers: reqHeaders,
            });
        }
        return next.handle(req);
    }
}