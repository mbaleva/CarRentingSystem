import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { DealerInfo } from 'src/app/cars/dealer.model';
import { DealersService } from '../dealers.service';
import { DefaultDealerModel } from '../default.dealer.model';

@Component({
  selector: 'app-dealers-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {
  dealer! : DealerInfo;
  userId = localStorage.getItem('userId');
  id: String;
  form!: FormGroup;
  constructor(private router: Router,
     private route: ActivatedRoute, 
     private dealersService: DealersService,
     private fb: FormBuilder) {
    this.id = this.route.snapshot.paramMap.get('id') as String;
    this.dealer = new DefaultDealerModel();
    this.form = this.fb.group({
      name: ['', Validators.required],
      phoneNumber: ['', Validators.required]
    })
  }
  ngOnInit(): void {
    this.dealersService.getById(this.userId as String, this.id)
      .subscribe(d => {
        this.dealer = d;
      });
    console.log(`After getting the dealer from the server => ` + this.dealer.name, this.dealer.phoneNumber, this.dealer.totalCars);
    this.initializeForm();
    console.log('After initializing form');
  }
  initializeForm() {
    this.form = this.fb.group({
      name: [this.dealer.name, Validators.required],
      phoneNumber: [this.dealer.phoneNumber, Validators.required]
    })
  }
  submitHandler() {
    console.log(this.form.value);
    this.dealersService.edit(this.form.value, this.id).subscribe(res => {
      this.router.navigateByUrl('/dealers/profile/' + this.id);
    });
  }
}
