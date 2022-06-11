import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { map, take, tap } from 'rxjs/operators';
import { CarInListModel } from 'src/app/cars/car.list.model';
import { DealerInfo } from 'src/app/cars/dealer.model';
import { DealersService } from '../dealers.service';

@Component({
  selector: 'app-dealers-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  dealerId!: string;
  userId!: string;
  dealer!: DealerInfo;
  cars!: Array<CarInListModel>;

  constructor(private dealersService: DealersService, private router: Router) { }

  ngOnInit(): void {
    this.getUserData();
    this.getDealer();
    this.getCars();
  }
  getUserData(): void {
    this.userId = localStorage.getItem('userId') as string;
    this.dealerId = localStorage.getItem('DealerId') as string;
  }
  getDealer() {
    this.dealersService.getById(this.userId, this.dealerId).pipe(
      take(1),
      map(dealer => this.dealer = dealer)
    ).subscribe();
  }
  getCars(){
    this.dealersService.getMyCars(this.userId, this.dealerId).pipe(
      take(1),
      map(cars => this.cars= cars)
    ).subscribe();
  }
  deleteCar(carId: string){
    this.dealersService.deleteCarById(carId, this.dealerId).subscribe();
  }
  deleteProfile() {
    this.dealersService.delete(this.userId, this.dealerId).pipe(
      tap(x => this.router.navigateByUrl(''))
    ).subscribe();
  }
}
