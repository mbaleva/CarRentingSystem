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
  async ngOnInit(): Promise<void> {
    await this.getDealer();
    console.log(`On Init method => ` + this.dealer);
    await this.initializeForm();
  }
  initializeForm() {
    this.form = this.fb.group({
      name: [this.dealer.name, Validators.required],
      phoneNumber: [this.dealer.phoneNumber, Validators.required]
    })
  }
  getDealer() {
    this.dealersService.getById(this.userId as String, this.id)
      .subscribe(dealer => {
        console.log(dealer);
        this.dealer = dealer;
      });
  }
  submitHandler() {
    this.dealersService.edit(this.form.value).subscribe(res => {
      this.router.navigateByUrl('/dealers/profile/' + this.id);
    });
  }
}
