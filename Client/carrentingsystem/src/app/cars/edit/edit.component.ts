import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { CarModel } from '../car.model';
import { CarsService } from '../cars.service';
import { CategoryModel } from '../category.model';

@Component({
  selector: 'app-cars-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {
  form!: FormGroup;
  categories!: Array<CategoryModel>;
  id!: String | null;
  car!: CarModel;

  constructor(private carsService: CarsService, private formBuilder: FormBuilder, private activatedRoute: ActivatedRoute) {
    this.id = this.activatedRoute.snapshot.paramMap.get('id');
    this.categories = this.carsService.getAllCategories();
   }
   carUpdatedHandler(){}

   ngOnInit(): void {
       this.carsService.getCarById(this.id as String).subscribe(car => {
         this.car = car;
       });
       this.form = this.formBuilder.group({
        name: [this.car.name],
        model: [this.car.model],
        imageUrl: [this.car.imageUrl],
        manufacturerName: [this.car.manufacturer],
        pricePerDay: [this.car.pricePerDay],
        hasClimateControl: [this.car.hasClimateControl],
        hasAutomaticTransmission: [this.car.transmissionType],
        seatsCount: [this.car.numberOfSeats],
        categoryId: []
      })
   }
}
