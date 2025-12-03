import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, mergeMap, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ApiResponse } from '../_core/models/api-response';

export interface DataTablesResponse {
    draw?: number;
    recordsTotal: number;
    recordsFiltered: number;
    data: any[];
}

export interface ISupplierModel {
    id: string
    code: string
    name: string
    shortName: string
    taxCode: string
    phone: string
    email: string
    website: string
    contactName: string
    contactPhone: string
    contactEmail: string
    provinceId: string
    wardId: string
    addressLine: string
    fullAddress: string
    bankName: string
    bankAccountNo: string
    bankAccountName: string
    paymentTermDays: number
    note: string
    isActive: boolean
}

@Injectable({
    providedIn: 'root'
})
export class SupplierService {

    private apiUrl = `${environment.apiUrlV1}/suppliers`;

    constructor(private http: HttpClient) { }

    gets(dataTablesParameters: any): Observable<any> {
        const url = `${this.apiUrl}/filter`;
        return this.http.post<any>(url, dataTablesParameters);
    }

    get(id: string): Observable<ISupplierModel> {
        const url = `${this.apiUrl}/${id}`;
        return this.http.get<ISupplierModel>(url);
    }

    create(user: ISupplierModel): Observable<ApiResponse<any>> {
        return this.http.post<ApiResponse<any>>(this.apiUrl, user);
    }

    update(id: string, user: ISupplierModel): Observable<ApiResponse<any>> {
        const url = `${this.apiUrl}/${id}`;
        return this.http.put<ApiResponse<any>>(url, user);
    }

    upload(formData: FormData): Observable<any> {
        const url = `${this.apiUrl}/upload`;
        return this.http.post<ISupplierModel>(url, formData);
    }

    delete(id: string): Observable<void> {
        const url = `${this.apiUrl}/${id}`;
        return this.http.delete<void>(url);
    }
}