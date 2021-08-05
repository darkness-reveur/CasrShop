import { Car } from "./Car";
import { CartCar } from "./CartCar";
import { User } from "./user";

export interface Cart {
    id: number

    cartCars: CartCar[]
}