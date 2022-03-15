import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
    selector: 'app-cars-rent',
    templateUrl: './rent.component.html',
    styleUrls: ['./rent.component.css']
})
export class RentComponent {
    form!: FormGroup;
    constructor(private router: Router,
        private formBuilder: FormBuilder){
        
        this.form = this.formBuilder.group({
            startDate: ['', Validators.required],
            endDate: ['', Validators.required],
        });
    }
    rentHandler(): void {
        
    }

}