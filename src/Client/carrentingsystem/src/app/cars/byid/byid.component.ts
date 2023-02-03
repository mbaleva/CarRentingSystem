import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { map, take } from 'rxjs/operators';
import { CarModel } from '../car.model';
import { CarsService } from '../cars.service';

@Component({
    selector: 'app-cars-byid',
    templateUrl: './byid.component.html',
    styleUrls: ['./byid.component.css']
})

export class ByIdComponent implements OnInit {
    id!: string;
    car!: CarModel;

    constructor(private carsService: CarsService, private route: ActivatedRoute){
        this.id = this.route.snapshot.paramMap.get('id') as string;
    }

    ngOnInit(): void {
        this.carsService.getCarById(this.id).pipe(
            take(1),
            map(res => this.car = res)
        ).subscribe();
    }
}