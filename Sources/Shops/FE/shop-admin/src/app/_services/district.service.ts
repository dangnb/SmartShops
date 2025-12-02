import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, mergeMap, of } from 'rxjs';
import { environment } from 'src/environments/environment';

export interface DataTablesResponse {
    value: ValueModel;
    isSuccess: number;
    isFailure: number;
    error: any;
}

export interface ValueModel {
    items: any;
    pageIndex: number;
    pageSize: number;
    totalCount: number;
    hasNextPage: boolean;
    hasPreviousPage: boolean;
}

export interface IDistrictModel {
    id: 0 | number;
    code: string;
    name: string;
    cityId: number;
}

@Injectable({
    providedIn: 'root'
})
export class DistrictService {

    private apiUrl = `${environment.apiUrlV1}/districts`;

    constructor(private http: HttpClient) { }

    getDistricts(dataTablesParameters: any): Observable<DataTablesResponse> {
        const url = `${this.apiUrl}/filter`;
        return this.http.post<any>(url, dataTablesParameters);
    }

    getByCity(cityId: string): Observable<any> {
        const url = `${this.apiUrl}/getbycity/${cityId}`;
        return this.http.get<any>(url);
    }

    getDistrict(id: number): Observable<IDistrictModel> {
        const url = `${this.apiUrl}/${id}`;
        return this.http.get<IDistrictModel>(url);
    }

    createDistrict(user: IDistrictModel): Observable<IDistrictModel> {
        return this.http.post<IDistrictModel>(this.apiUrl, user);
    }

    updateDistrict(id: number, user: IDistrictModel): Observable<IDistrictModel> {
        const url = `${this.apiUrl}/${id}`;
        return this.http.put<IDistrictModel>(url, user);
    }

    deleteDistrict(id: number): Observable<void> {
        const url = `${this.apiUrl}/${id}`;
        return this.http.delete<void>(url);
    }
}