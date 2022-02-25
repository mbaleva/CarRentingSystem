import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CarModel } from '../car.model';
import { CarsService } from '../cars.service';

@Component({
    selector: 'app-cars-byid',
    templateUrl: './byid.component.html',
    styleUrls: ['./byid.component.css']
})

export class ByIdComponent implements OnInit {
    id: String | null;
    car!: CarModel;

    constructor(private carsService: CarsService, private route: ActivatedRoute){
        this.id = this.route.snapshot.paramMap.get('id');
    }

    ngOnInit(): void {
        this.carsService.getCarById(this.id as string).subscribe(res => {
            this.car = res;
        });
    }
}