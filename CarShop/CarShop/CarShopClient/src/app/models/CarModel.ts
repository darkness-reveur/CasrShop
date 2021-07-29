import { Car } from "./Car";
import { CarBrand } from "./CarBrand";


export interface CarModel{
    id : number

    name : string

    carBrandId : number 

    carBrand : CarBrand

    cars : Car
}