import { CarModel } from "./CarModel";
import { CartCar } from "./CartCar";
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

export enum EngineTypes {
    PetrolEngine,
    DieselEngine,
    ElectroEngine
}
export enum WheelDrives {
    FrontWheelDrive,
    RealWheelDrive,
    AllWheelDrive,
    PlugInAllWheelDrive
}