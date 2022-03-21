import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable, resolveForwardRef } from '@angular/core';
import { resetFakeAsyncZone } from '@angular/core/testing';
import { FormGroup } from '@angular/forms';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { CarInListModel } from './car.list.model';
import { CarModel } from './car.model';
import { CategoryModel } from './category.model';
import { ManufacturerModel } from './manufacturer.model';

@Injectable({
    providedIn: 'root'
})

export class CarsService {
    carsUrl = environment.dealersUrl + '/cars';
    categoriesUrl = environment.dealersUrl + '/categories/all';
    manufacturersUrl = environment.dealersUrl + '/manufacturers';
    rentingUrl = environment.rentingUrl + '/appointments';

    constructor(private httpClient: HttpClient){}

    addCar(data: FormData): Observable<any> {
        return this.httpClient.post(this.carsUrl + '/add', data);
    }
    getAllCategories() : Array<CategoryModel> {
        let data = new Array<CategoryModel>();
        

        this.httpClient.get(this.categoriesUrl).subscribe(res => {
            let resAsArray = (res as Array<CategoryModel>);
            for(let category of resAsArray){
                data.push(category as CategoryModel);
            }
            console.log(data);
            
        });
        console.log(data);
        return data;
    }
    getCars(): Array<CarInListModel> {
        let data = new Array<CarInListModel>();
        this.httpClient.get(this.carsUrl + '/all').subscribe(res => {
            console.log(res);
            let resAsArray = res as Array<CarInListModel>;
            for(let car of resAsArray){
                data.push(car as CarInListModel);
            }
        });
        return data;
    }
    getCarById(id: String): Observable<CarModel>  {
        return this.httpClient.get<CarModel>(this.carsUrl + '/byid?id=' + id);
    }
    updateCar(data: FormData, carId: String, dealerId: String): Observable<any> {
        return this.httpClient.post(this.carsUrl + `/edit?carId=${carId}&dealerId=${dealerId}`, data);
    }
    getManufacturers(): Observable<Array<ManufacturerModel>> {
        return this.httpClient.get<Array<ManufacturerModel>>(this.manufacturersUrl + '/getAll');
    }
    search(searchTerm: String, categoryId: Number, manufacturerId: Number): Observable<any> {
        return this.httpClient.get(this.carsUrl + `/Search?searchTerm=${searchTerm}&categoryId=${categoryId}&manufacturerId=${manufacturerId}`);
    }
    rentCar(data: FormData): Observable<any> {
        return this.httpClient.post(this.rentingUrl + '/create', data);
    }
}