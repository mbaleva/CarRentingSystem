import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  form!: FormGroup;
  

  constructor(private authService: AuthService,
       private router: Router,
       private formBuilder: FormBuilder) {
    if(localStorage.getItem('authToken')) {
      this.router.navigateByUrl('');
    }
    this.form = this.formBuilder.group({
      email: ['yourEmail@email.bg', [Validators.required, Validators.email]],
      password: ['', Validators.required, Validators.minLength(5)]
    })
  }
  ngOnInit(): void {
      
  }
  loginHandler(): void {
    this.authService.login(this.form.value)
          .subscribe(res => {
              this.authService.setAuthToken(res['token']);
              this.authService.setUserId(res['userId']);
              localStorage.setItem('IsDealer', 'false');
              window.location.reload();
              this.router.navigateByUrl('/');
          });
  }
}