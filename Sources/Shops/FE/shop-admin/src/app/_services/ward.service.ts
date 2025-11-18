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

export interface IWardModel {
    provinceId: string;
    id: string;
    code?: string;
    name: string;
}

@Injectable({
    providedIn: 'root'
})
export class WardService {

    private apiUrl = `${environment.apiUrlV1}/wards`;

    constructor(private http: HttpClient) { }

    getWards(dataTablesParameters: any): Observable<any> {
        const url = `${this.apiUrl}/filter`;
        return this.http.post<any>(url, dataTablesParameters);
    }

    getByProvince(provinceId: any): Observable<any> {
        const url = `${this.apiUrl}/province/${provinceId}`;
        return this.http.get<any>(url);
    }

    getWard(id: any): Observable<IWardModel> {
        const url = `${this.apiUrl}/${id}`;
        return this.http.get<IWardModel>(url);
    }

    createWard(user: IWardModel): Observable<IWardModel> {
        return this.http.post<IWardModel>(this.apiUrl, user);
    }

    updateWard(id: any, user: IWardModel): Observable<IWardModel> {
        const url = `${this.apiUrl}/${id}`;
        return this.http.put<IWardModel>(url, user);
    }

    deleteWard(id: any): Observable<void> {
        const url = `${this.apiUrl}/${id}`;
        return this.http.delete<void>(url);
    }
}