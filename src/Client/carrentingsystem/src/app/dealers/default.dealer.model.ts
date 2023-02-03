import { DealerInfo } from "../cars/dealer.model";

export class DefaultDealerModel implements DealerInfo {
    constructor() {
        this.id = '';
        this.name = '';
        this.totalCars = 0;
        this.phoneNumber = '';
    }
    id: string;
    name: string;
    totalCars: number;
    phoneNumber: string;

}