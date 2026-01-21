import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, mergeMap, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ApiResponse, PageResponse } from 'src/app/_core/models/api-response';

export interface IGoodsReceiptModel {
    id: string;
    warehouseName: string;
    supplierName: string;
    receiptNo: string;
    status: number;
    subtotal: number;
    total: number;
    createdAt: string;
    createdBy: string;
}

@Injectable({
    providedIn: 'root'
})
export class GoodsReceiptService {

    private apiUrl = `${environment.apiUrlV1}/GoodsReceipts`;

    constructor(private http: HttpClient) { }
    filter(dataTablesParameters: any): Observable<ApiResponse<PageResponse<IGoodsReceiptModel[]>>> {
        const url = `${this.apiUrl}/filter`;
        return this.http.post<ApiResponse<PageResponse<IGoodsReceiptModel[]>>>(url, dataTablesParameters);
    }

    post(model: IGoodsReceiptModel): Observable<IGoodsReceiptModel> {
        return this.http.post<IGoodsReceiptModel>(this.apiUrl, model);
    }

    put(id: string, model: IGoodsReceiptModel): Observable<IGoodsReceiptModel> {
        const url = `${this.apiUrl}/${id}`;
        return this.http.put<IGoodsReceiptModel>(url, model);
    }

    delete(id: string): Observable<void> {
        const url = `${this.apiUrl}/${id}`;
        return this.http.delete<void>(url);
    }
}