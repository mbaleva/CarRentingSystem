import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { DealersService } from '../dealers.service';

@Component({
  selector: 'app-dealers-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class CreateComponent  {
  form!: FormGroup;

  constructor(private dealersService: DealersService,
       private router: Router,
       private formBuilder: FormBuilder) {
    if(localStorage.getItem('IsDealer') == 'true') {
      this.router.navigateByUrl('');
    }
    this.form = this.formBuilder.group({
      name: ['', Validators.required],
      phoneNumber: ['', Validators.required],
    });
  }
  submitHandler(): void {
    this.dealersService.createDealer(this.form.value).subscribe(x => {
        localStorage.setItem('DealerId', x);
        localStorage.setItem('IsDealer', 'true');
        this.router.navigateByUrl('/');
      });
  }
}