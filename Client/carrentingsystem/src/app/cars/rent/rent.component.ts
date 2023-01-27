import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CarsService } from '../cars.service';

@Component({
    selector: 'app-cars-rent',
    templateUrl: 'rent.component.html',
    styleUrls: ['./rent.component.css']
})
export class RentComponent {
    form!: FormGroup;
    id: String | null;
    constructor(private router: Router,
        private formBuilder: FormBuilder,
        private route: ActivatedRoute,
        private carsService: CarsService) {
        const userId = localStorage.getItem('userId');
        this.id = this.route.snapshot.paramMap.get('id');
        this.form = this.formBuilder.group({
            carId: [this.id],
            userId: [userId],
            startDate: ['', Validators.required],
            endDate: ['', Validators.required],
        });
    }
    rentHandler(): void {
        this.carsService.rentCar(this.form.value).subscribe(x => {});
    }

}