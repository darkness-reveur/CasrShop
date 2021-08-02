import { Car } from "./Car";
import { User } from "./user";

export interface Cart{
    id : number

    cars : Car[]

    user : User
}