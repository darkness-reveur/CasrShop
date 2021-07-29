import { Car } from "./Car";
import { User } from "./user";

export interface Cart{
    id : number

    cars : Car

    userId : number

    user : User
}