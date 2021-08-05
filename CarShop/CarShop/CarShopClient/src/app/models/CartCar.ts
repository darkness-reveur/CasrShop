import { Car } from "./Car";
import { Cart } from "./Cart";


export interface CartCar {
    id: number

    carId: number

    car: Car

    cartId: number

    cart: Cart
}