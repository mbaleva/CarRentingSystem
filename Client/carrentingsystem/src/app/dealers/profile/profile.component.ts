import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CarInListModel } from 'src/app/cars/car.list.model';
import { DealerInfo } from 'src/app/cars/dealer.model';
import { DealersService } from '../dealers.service';

@Component({
  selector: 'app-dealers-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  dealerId!: String;
  userId!: String;
  dealer!: DealerInfo;
  cars!: Array<CarInListModel>;

  constructor(private dealersService: DealersService, private router: Router) { }

  ngOnInit(): void {
    this.getUserData();
    this.getDealer();
    this.getCars();
  }
  getUserData(): void {
    this.userId = localStorage.getItem('userId') as String;
    this.dealerId = localStorage.getItem('DealerId') as String;
  }
  getDealer() {
    this.dealersService.getById(this.userId, this.dealerId).subscribe(dealer => {
      this.dealer = dealer;
    });
  }
  getCars(){
    this.dealersService.getMyCars(this.userId, this.dealerId).subscribe(cars => {
      this.cars = cars;
    });
  }
  deleteCar(carId: String){
    this.dealersService.deleteCarById(carId, this.dealerId);
  }
  deleteProfile() {
    this.dealersService.delete(this.userId, this.dealerId)
      .subscribe(res => {
        this.router.navigateByUrl('/');
      });
  }
}
