import { CarModel } from "./CarModel";


export interface CarBrand {
    id: number

    name: string

    carModels: CarModel[]
}