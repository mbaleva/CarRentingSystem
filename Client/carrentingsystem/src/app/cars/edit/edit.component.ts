import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
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

  constructor(private carsService: CarsService, 
    private formBuilder: FormBuilder,
    private activatedRoute: ActivatedRoute,
    private router: Router) {
    this.id = this.activatedRoute.snapshot.paramMap.get('id');
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
    })
   }
   carUpdatedHandler(){
     this.carsService.updateCar(this.form.value, this.id as String, localStorage.getItem('DealerId') as String)
        .subscribe(res => {
          this.router.navigate(['']);
        });
   }

   async ngOnInit(): Promise<void> {
    await this.getCategories();
    await this.getCar();
   }
   getCategories() {
     this.categories = this.carsService.getAllCategories();
   }
   getCar() {
     this.carsService.getCarById(this.id as String).subscribe(response => {
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
     });
   }
}
