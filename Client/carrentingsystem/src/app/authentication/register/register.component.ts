import {Component} from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service';

@Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.css']
})

export class RegisterComponent { 
    constructor(private authService: AuthService, private router: Router) { 
        if(localStorage.getItem('authToken')){
            this.router.navigateByUrl('');
        }
    }

    registerSubmitHandler(data: any){
        this.authService.register(data).subscribe(res => {
            this.router.navigateByUrl('/auth/login');
        });
    }
}