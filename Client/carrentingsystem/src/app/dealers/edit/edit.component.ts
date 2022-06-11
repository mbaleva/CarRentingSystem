import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { finalize, map, take } from 'rxjs/operators';
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
  userId = localStorage.getItem('userId') as string;
  id!: string;
  form!: FormGroup;
  constructor(private router: Router,
     private route: ActivatedRoute, 
     private dealersService: DealersService,
     private fb: FormBuilder) {
    this.id = this.route.snapshot.paramMap.get('id') as string;
    this.form = this.fb.group({
      name: ['', Validators.required],
      phoneNumber: ['', Validators.required]
    })
  }
  ngOnInit(): void {
    this.dealersService.getById(this.userId, this.id).pipe(
      take(1),
      map(dealer => {
        this.dealer = dealer;
      }),
      finalize(() => {
        this.initializeForm();
      })
    ).subscribe();
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
