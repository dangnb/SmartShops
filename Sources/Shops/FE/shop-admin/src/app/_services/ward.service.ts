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

export interface IWardModel 
{
    cityId:0|number;
    districtId:0|number;
    id: 0 | number;
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

    getByDistrict(districtId:number): Observable<any> {
        const url = `${this.apiUrl}/getbydistrict/${districtId}`;
        return this.http.get<any>(url);
    }

    getWard(id: number): Observable<IWardModel> {
        const url = `${this.apiUrl}/${id}`;
        return this.http.get<IWardModel>(url);
    }

    createWard(user: IWardModel): Observable<IWardModel> {
        return this.http.post<IWardModel>(this.apiUrl, user);
    }

    updateWard(id: number, user: IWardModel): Observable<IWardModel> {
        const url = `${this.apiUrl}/${id}`;
        return this.http.put<IWardModel>(url, user);
    }

    deleteWard(id: number): Observable<void> {
        const url = `${this.apiUrl}/${id}`;
        return this.http.delete<void>(url);
    }
}