import { Role } from "../authentication/auth.model";

export interface User {
    id: number;
    name: string;
    email: string;
    mobile: string;
    role: Role;
    createdDate: Date;
}


export interface UserResponse {
    message: string;
    payload: User;
    status: boolean;
}

export interface UserListResponse {
    message: string;
    payload: User[];
    status: boolean;
}

export interface Response {
    message: string;
    status: boolean;
}
