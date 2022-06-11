import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { map, take } from 'rxjs/operators';
import { CarModel } from '../car.model';
import { CarsService } from '../cars.service';
import { CategoryModel } from '../category.model';
import { ManufacturerModel } from '../manufacturer.model';

@Component({
  selector: 'app-cars-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {

  categories!: Array<CategoryModel>; 
  manufacturers!: Array<ManufacturerModel>;
  form!: FormGroup;
  @Output() carsEmitter = new EventEmitter<Array<CarModel>>();
  constructor(private carsService: CarsService, private router: Router, private fb: FormBuilder) { }

  ngOnInit() {
    this.getCategories();
    this.getManufacturers();
    this.initializeForm();
  }
  initializeForm() {
    this.form = this.fb.group({
      searchTerm: [''],
      manufacturerId: [],
      categoryId: []
    })
  }
  submitHandler(){
    let searchTerm = this.form.get('searchTerm')?.value as unknown as String;
    let categoryId = this.form.get('categoryId')?.value as unknown as Number;
    let manufacturerId = this.form.get('manufacturerId')?.value as unknown as Number;
    this.carsService.search(searchTerm, categoryId, manufacturerId)
      .subscribe(res => {
        this.carsEmitter.emit(res);
      });
  }
  getCategories() {
    this.carsService.getAllCategories().pipe(
      take(1),
      map(res => this.categories = res)
    ).subscribe();
  }
  getManufacturers() {
    this.carsService.getManufacturers().pipe(
      take(1),
      map(res => this.manufacturers = res)
    ).subscribe();
  }
}
