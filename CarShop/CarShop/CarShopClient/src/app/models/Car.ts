import { CarModel } from "./CarModel";
import { CartCar } from "./CartCar";
import { EngineTypes } from "./enums/EngineTypes";
import { WheelDrives } from "./enums/WheelDrives";
import { User } from "./user";

export interface Car {
    id: number

    releaseYear: number

    engineVolume: number

    price: number

    vehicleMileage: number

    description: string

    pictureLinks: string

    carModelId: number

    carModel: CarModel

    cartsCar: CartCar[]

    wheelDrive: WheelDrives

    engineType: EngineTypes
}


