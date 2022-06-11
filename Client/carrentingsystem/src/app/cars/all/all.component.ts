import { Component, OnInit } from '@angular/core';
import { map, take } from 'rxjs/operators';
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
  updateCarsForSearch(data: any) {
    console.log('I am here');
    console.log(data);
    this.cars = data as Array<CarInListModel>;
  }
  ngOnInit(): void {
    this.assignCars();
  }
  assignCars(): void {
    this.carsService.getCars().pipe(
      take(1),
      map(res => {
        this.cars = res;
      })
    ).subscribe();
  }
}
