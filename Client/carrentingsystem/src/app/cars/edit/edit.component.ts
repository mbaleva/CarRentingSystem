import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { map, take } from 'rxjs/operators';
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
  id!: string;
  car!: CarModel;

  constructor(private carsService: CarsService, 
    private formBuilder: FormBuilder,
    private activatedRoute: ActivatedRoute,
    private router: Router) {
    this.id = this.activatedRoute.snapshot.paramMap.get('id') as string;
    this.form = this.formBuilder.group({
      manufacturerName: ['', Validators.required],
      model: ['', Validators.required],
      categoryId: [0, Validators.required],
      name: ['', Validators.required],
      imageUrl: ['', Validators.required],
      pricePerDay: [0, Validators.required],
      hasClimateControl: [false, Validators.required],
      seatsCount: [0, Validators.required],
      transmissionType: [0, Validators.required],
    });
    this.categories = [];
   }
   carUpdatedHandler(){
     this.carsService.updateCar(this.form.value, this.id as String, localStorage.getItem('DealerId') as String)
        .subscribe(res => {
          this.router.navigate(['']);
        });
   }

  ngOnInit() {
    this.getCategories();
    this.getCar();
   }
   getCategories() {
     this.carsService.getAllCategories().pipe(
       take(1),
       map(res => this.categories = res)
     ).subscribe();
   }
   getCar() {
     this.carsService.getCarById(this.id).pipe(
       take(1),
       map(response => {
        this.form = this.formBuilder.group({
                manufacturerName: [response.manufacturer, Validators.required],
                model: [response.model, Validators.required],
                categoryId: [0, Validators.required],
                name: [response.name, Validators.required],
                imageUrl: [response.imageUrl, Validators.required],
                pricePerDay: [response.pricePerDay, Validators.required],
                hasClimateControl: [response.hasClimateControl, Validators.required],
                seatsCount: [response.numberOfSeats, Validators.required],
                transmissionType: [response.transmissionType, Validators.required],
             });
        this.mapCategory(response.category);
       })
     ).subscribe();
   }
  mapCategory(category: string) {
    let selectedCategory = this.categories.filter(x => x.name == category)[0];
    this.form.patchValue({categoryId: selectedCategory.id})
  }
}
