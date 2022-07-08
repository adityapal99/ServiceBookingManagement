export interface UserRequest {
    id:number;
    productid:number;
    userid:number;
    requestDate:Date;
    problem:string;
    description:string;
    status:string;
}

export interface ResponseObj
{
    status:number;
    msg:string;
    payload:any;
}

export interface Sample
{
    productid:number;
    userid:number;
    requestDate:Date;
    problem:string;
    description:string;
    status:string;
}
