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

export interface IWarehouseModel {
    id: string;
    code?: string;
    name: string;
    address: string;
    isActive: boolean;
}

@Injectable({
    providedIn: 'root'
})
export class WarehouseService {

    private apiUrl = `${environment.apiUrlV1}/warehouses`;

    constructor(private http: HttpClient) { }

    getWarehouses(dataTablesParameters: any): Observable<any> {
        const url = `${this.apiUrl}/filter`;
        return this.http.post<any>(url, dataTablesParameters);
    }

    getAll(): Observable<ApiResponse<IWarehouseModel[]>> {
        const url = `${this.apiUrl}/getall`;
        return this.http.get<ApiResponse<IWarehouseModel[]>>(url);
    }

    getWarehouse(id: string): Observable<IWarehouseModel> {
        const url = `${this.apiUrl}/${id}`;
        return this.http.get<IWarehouseModel>(url);
    }

    createWarehouse(user: IWarehouseModel): Observable<IWarehouseModel> {
        return this.http.post<IWarehouseModel>(this.apiUrl, user);
    }

    updateWarehouse(id: string, user: IWarehouseModel): Observable<IWarehouseModel> {
        const url = `${this.apiUrl}/${id}`;
        return this.http.put<IWarehouseModel>(url, user);
    }

    deleteWarehouse(id: string): Observable<void> {
        const url = `${this.apiUrl}/${id}`;
        return this.http.delete<void>(url);
    }
}