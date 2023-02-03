import { DealerInfo } from "./dealer.model";

export interface CarModel {
    id: string;
    name: string;
    manufacturer: string;
    model: string;
    imageUrl: string;
    category: string;
    pricePerDay: number;
    hasClimateControl: boolean;
    numberOfSeats: number;
    transmissionType: string;
    dealer: DealerInfo;
}