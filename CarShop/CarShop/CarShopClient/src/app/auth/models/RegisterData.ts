import { User } from "src/app/models/User";

export interface RegisterData{
    user : User

    login : string

    password : string
}