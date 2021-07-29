import { CarBrand } from "./CarBrand";
import { Cart } from "./Cart";
import { User } from "./user";

export interface Car{
    id : number

    releaseYear : number

    engineVolume : number

    price : number

    vehicleMileage : number

    description : string

    pictureLinks : string[]

    carModuleId : number

    carModule : CarBrand

    cartId : number

    cart : Cart

    userId : number

    user : User

    wheelDrive : WheelDrives

    engineType : EngineTypes
}

export enum EngineTypes{
    PetrolEngine,
    DieselEngine,
    ElectroEngine
}
export enum WheelDrives{
    FrontWheelDrive,
    RealWheelDrive,
    AllWheelDrive,
    PlugInAllWheelDrive
}