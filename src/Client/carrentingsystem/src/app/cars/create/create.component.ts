import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { map, take } from 'rxjs/operators';
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
        this.categories = this.getCategories();
    }
    getCategories(): CategoryModel[] {
        this.carsService.getAllCategories().pipe(
            take(1),
            map(res => this.categories = res)
        ).subscribe();
        return this.categories;
    }
    carCreatedHandler() {
        this.carsService.addCar(this.form.value).pipe(
            take(1),
            map(res => {
                this.router.navigateByUrl(`/cars/byid/${res}`);
            })
        ).subscribe();
    }
}