import { Car } from "./Car";
import { User } from "./user";


export interface Order {
    id: number

    totalAmount: number

    date: Date

    orderStatus: OrderStatuses

    userId: number

    user: User

    cars: Car[]
}

export enum OrderStatuses {
    InProgress,
    Paid
}