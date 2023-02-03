import { StatisticsModel } from "./stats.model";

export class DefaultStatisticsModel implements StatisticsModel{
    constructor(){
        this.totalCars = 0;
        this.totalRentedCars = 0;
        this.totalDealers = 0;
    }
    totalCars: Number;
    totalRentedCars: Number;
    totalDealers: Number;
}