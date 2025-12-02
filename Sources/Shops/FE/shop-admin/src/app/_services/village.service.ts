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

export interface IVillageModel {
    cityId: string;
    wardId: string;
    districtId: 0 | number;
    id: 0 | number;
    code?: string;
    name: string;
    username: string
}

@Injectable({
    providedIn: 'root'
})
export class VillageService {

    private apiUrl = `${environment.apiUrlV1}/Villages`;

    constructor(private http: HttpClient) { }

    getVillages(dataTablesParameters: any): Observable<any> {
        const url = `${this.apiUrl}/filter`;
        return this.http.post<any>(url, dataTablesParameters);
    }

    getByWard(wardId: number): Observable<any> {
        const url = `${this.apiUrl}/getbywardid/${wardId}`;
        return this.http.get<any>(url);
    }


    getVillage(id: number): Observable<IVillageModel> {
        const url = `${this.apiUrl}/${id}`;
        return this.http.get<IVillageModel>(url);
    }

    createVillage(user: IVillageModel): Observable<IVillageModel> {
        return this.http.post<IVillageModel>(this.apiUrl, user);
    }

    updateVillage(id: number, user: IVillageModel): Observable<IVillageModel> {
        const url = `${this.apiUrl}/${id}`;
        return this.http.put<IVillageModel>(url, user);
    }

    deleteVillage(id: number): Observable<void> {
        const url = `${this.apiUrl}/${id}`;
        return this.http.delete<void>(url);
    }
}