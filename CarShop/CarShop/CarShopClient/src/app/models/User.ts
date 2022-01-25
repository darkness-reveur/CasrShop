import { Car } from "./Car";
import { Cart } from "./Cart";
import { UserRoles } from "./enums/UserRoles";
import { Order } from "./Order";

export interface User {
    id: number

    name: string

    age: number

    email: string

    role: UserRoles

    mobilePhoneNumber: string

    car: Car

    carId?: number

    orders: Order[]

    cart: Cart
}

