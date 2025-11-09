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

export interface ICityModel {
   id: string;
   code: string;
   customerId: string;
   customerName: string;
   customerAddress: string;
   customerCode: string;
   quantity:number;
   totalOfMonth:string;
   numberOfMonths:number;
   price:number;
   total:number;
   vatAmount:number;
   amount:number;
   type:number;
   status:number;
   createdDate: string;
   createdBy: string;
   modifiedDate: string;
   modifiedBy: string;
   note: string;
   isPrinted: boolean;      
}
@Injectable({
    providedIn: 'root'
})
export class PaymentService {

    private apiUrl = `${environment.apiUrlV1}/payments`;

    constructor(private http: HttpClient) { }

    getCities(dataTablesParameters: any): Observable<any> {
        const url = `${this.apiUrl}/filter`;
        return this.http.post<any>(url, dataTablesParameters);
    }
}