

export interface Car {
    id?: number;
    manufacturer: string;
    model: string;
    category: number;
    imageUrl: string;
    pricePerDay: number;
    hasClimateControl: boolean;
    numberOfSeats: number;
    transmissionType: number;
    isAvailable?: boolean;
}