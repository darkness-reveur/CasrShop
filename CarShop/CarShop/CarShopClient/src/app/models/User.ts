import { Car } from "./Car";
import { Cart } from "./Cart";
import { Order } from "./Order";

export interface User{
    id : number

    name : string 

    age : number

    email : string

    userRole: UserRoles

    phoneNumber: string

    car : Car

    orders : Order[]

    cart : Cart
}

export enum UserRoles{
    Admin,
    AdminAssistant,
    AuthorizedUser,
    NotAuthorizedUser
}