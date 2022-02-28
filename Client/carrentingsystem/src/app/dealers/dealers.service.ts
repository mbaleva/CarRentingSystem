import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { DealerInfo } from '../cars/dealer.model';
import { CarInListModel } from '../cars/car.list.model';

@Injectable({
    providedIn: 'root'
})

export class DealersService {
    dealersPath = environment.dealersUrl + '/api/dealers';
    carsPath = environment.dealersUrl + '/cars';
    constructor(private httpClient: HttpClient) { }

    createDealer(data: any): Observable<any> {
        return this.httpClient.post(this.dealersPath + '/add', data);
    }
    getById(userId: String, dealerId: String): Observable<DealerInfo> {
        return this.httpClient.get<DealerInfo>(this.dealersPath + `/GetDealerById?dealerId=${dealerId}&userId=${userId}`);
    }
    getMyCars(userId: String, dealerId: String): Observable<Array<CarInListModel>> {
        return this.httpClient.get<Array<CarInListModel>>(this.carsPath + `/GetCarsByDealerId?dealerId=${dealerId}&userId=${userId}`);
    }
    deleteCarById(carId: String, dealerId: String) {
        return this.httpClient.get(this.carsPath + `/Delete?dealerId=${dealerId}&carId=${carId}`)
            .subscribe(x => {
                console.log(x);
            });
    }
    edit(data: FormData, id: String): Observable<any> {
        return this.httpClient.post(this.dealersPath + `/edit?id=${id}`, data);
    }
    delete(userId: String, dealerId: String): Observable<any> {
        return this.httpClient.get<DealerInfo>(this.dealersPath + `/Edit?dealerId=${dealerId}&userId=${userId}`);
    }
}