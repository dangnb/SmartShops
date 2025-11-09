import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, mergeMap, of } from 'rxjs';
import { environment } from 'src/environments/environment';

export interface DataTablesResponse {
    draw?: number;
    recordsTotal: number;
    recordsFiltered: number;
    data: any[];
}

export interface ICustomerModel 
{
    id:0|number;
    code:string;
    name:string;
    address: string;
    email?: string;
    phoneNumber: string;
    cityId:0|number;
    districtId:0|number;
    wardId:0|number;
    villageId:0|number;
    payments: PaymentHistory[]
}

export interface PaymentHistory 
{
   type:number
   quantity:number;
   price:number
}

@Injectable({
    providedIn: 'root'
})
export class CustomerService {

    private apiUrl = `${environment.apiUrlV1}/customers`;

    constructor(private http: HttpClient) { }

    getCustomers(dataTablesParameters: any): Observable<any> {
        const url = `${this.apiUrl}/filter`;
        return this.http.post<any>(url, dataTablesParameters);
    }

    getCustomer(id: string): Observable<ICustomerModel> {
        const url = `${this.apiUrl}/${id}`;
        return this.http.get<ICustomerModel>(url);
    }

    createCustomer(user: ICustomerModel): Observable<ICustomerModel> {
        return this.http.post<ICustomerModel>(this.apiUrl, user);
    }

    updateCustomer(id: number, user: ICustomerModel): Observable<ICustomerModel> {
        const url = `${this.apiUrl}/${id}`;
        return this.http.put<ICustomerModel>(url, user);
    }

    uploadCustomer(formData: FormData): Observable<any> {
        const url = `${this.apiUrl}/upload`;
        return this.http.post<ICustomerModel>(url, formData);
    }

    deleteCustomer(id: number): Observable<void> {
        const url = `${this.apiUrl}/${id}`;
        return this.http.delete<void>(url);
    }
}