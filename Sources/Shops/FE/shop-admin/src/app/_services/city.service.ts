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
    id: 0 | number;
    code?: string;
    name: string;
}

@Injectable({
    providedIn: 'root'
})
export class CityService {

    private apiUrl = `${environment.apiUrlV1}/provincies`;

    constructor(private http: HttpClient) { }

    getCities(dataTablesParameters: any): Observable<any> {
        const url = `${this.apiUrl}/filter`;
        return this.http.post<any>(url, dataTablesParameters);
    }

    getAll(): Observable<any> {
        const url = `${this.apiUrl}/getall`;
        return this.http.get<any>(url);
    }

    getCity(id: number): Observable<ICityModel> {
        const url = `${this.apiUrl}/${id}`;
        return this.http.get<ICityModel>(url);
    }

    createCity(user: ICityModel): Observable<ICityModel> {
        return this.http.post<ICityModel>(this.apiUrl, user);
    }

    updateCity(id: number, user: ICityModel): Observable<ICityModel> {
        const url = `${this.apiUrl}/${id}`;
        return this.http.put<ICityModel>(url, user);
    }

    deleteCity(id: number): Observable<void> {
        const url = `${this.apiUrl}/${id}`;
        return this.http.delete<void>(url);
    }
}