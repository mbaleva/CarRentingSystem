import { Component, OnInit } from '@angular/core';
import { CarInListModel } from '../car.list.model';
import { CarsService } from '../cars.service';

@Component({
  selector: 'app-all',
  templateUrl: './all.component.html',
  styleUrls: ['./all.component.css']
})
export class AllComponent implements OnInit {
  cars!: Array<CarInListModel>;
  constructor(private carsService: CarsService) { 

  }

  ngOnInit(): void {
    this.assignCars();
  }
  assignCars(): void {
    this.cars = this.carsService.getCars();
  }
}
