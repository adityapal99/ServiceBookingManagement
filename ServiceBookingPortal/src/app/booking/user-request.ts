export interface UserRequest {
    id:number;
    productid:number;
    userid:number;
    requestDate:Date;
    problem:string;
    description:string;
    status:string;
}
export interface UserReport{
    "id":number;
    "reportDate": Date;
    "serviceType": string;
    "actionTaken": string;
    "diagnosisDetails": string;
    "isPaid": string;
    "visitFees": number;
    "repairDetails": string;
    "serviceRequestId": number;
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
export interface SampleReport
{
    "reportDate": Date;
    "serviceType": string;
    "actionTaken": string;
    "diagnosisDetails": string;
    "isPaid": string;
    "visitFees": number;
    "repairDetails": string;
    "serviceRequestId": number;

}
