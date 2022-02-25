import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  formGroup!: FormGroup;
  

  constructor(private authService: AuthService,
       private router: Router,
       private formBuilder: FormBuilder) {
    if(localStorage.getItem('authToken')) {
      this.router.navigateByUrl('');
    }
    
    this.formGroup = this.formBuilder.group({
      email: ['myemail@email.bg', [Validators.required, Validators.email]],
      username: ['yourUsername', Validators.required],
      password: ['', Validators.required, Validators.minLength(5)]
    });
  }
  ngOnInit(): void {
  }
  registerHandler(): void {
    console.log(this.formGroup.value);
    this.authService.register(this.formGroup.value)
          .subscribe(res => {
              this.router.navigateByUrl('/auth/login');
          });
  }
}