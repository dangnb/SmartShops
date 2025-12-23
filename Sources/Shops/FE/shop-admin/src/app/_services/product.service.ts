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

export interface IProductModel {
    id: number;
    code: string;
    name: string;
    barCode: string;
    categoryId?: string;
    isActive?: boolean;
}

@Injectable({
    providedIn: 'root'
})
export class ProductService {

    private apiUrl = `${environment.apiUrlV1}/products`;

    constructor(private http: HttpClient) { }

    getProducts(dataTablesParameters: any): Observable<any> {
        const url = `${this.apiUrl}/filter`;
        return this.http.post<any>(url, dataTablesParameters);
    }

    getProduct(id: string): Observable<IProductModel> {
        const url = `${this.apiUrl}/${id}`;
        return this.http.get<IProductModel>(url);
    }

    createProduct(user: IProductModel): Observable<IProductModel> {
        return this.http.post<IProductModel>(this.apiUrl, user);
    }

    updateProduct(id: string, user: IProductModel): Observable<IProductModel> {
        const url = `${this.apiUrl}/${id}`;
        return this.http.put<IProductModel>(url, user);
    }

    deleteProduct(id: string): Observable<void> {
        const url = `${this.apiUrl}/${id}`;
        return this.http.delete<void>(url);
    }
}