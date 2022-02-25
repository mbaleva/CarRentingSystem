import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CarsService } from '../cars.service';
import { CategoryModel } from '../category.model';

@Component({
    selector: 'app-cars-create',
    templateUrl: './create.component.html',
    styleUrls: ['./create.component.css']
})
export class CreateComponent {

    form!: FormGroup;
    categories: Array<CategoryModel>;
    constructor(private router: Router, private formBuilder: FormBuilder, private carsService: CarsService){
        
        this.form = this.formBuilder.group({
            name: ['', Validators.required],
            model: ['', Validators.required],
            imageUrl: ['', Validators.required],
            manufacturerName: ['', Validators.required],
            categoryId: ['', Validators.required],
            pricePerDay: ['', Validators.required],
            hasClimateControl: [],
            hasAutomaticTransmission: ['', Validators.required],
            seatsCount: []
        });
        this.categories = this.carsService.getAllCategories();
    }
    carCreatedHandler(){
        this.carsService.addCar(this.form.value).subscribe(res => {
            this.router.navigateByUrl(`/cars/byid/${res}`);
        });
    }
}