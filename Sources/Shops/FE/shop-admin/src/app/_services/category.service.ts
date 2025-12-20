import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, mergeMap, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ApiResponse } from '../_core/models/api-response';


export interface ICategoryTreeModel {
    id: string; // Guid is typically a string in JavaScript/TypeScript
    code: string;
    name: string;
    parentId?: string; // Guid? in C# maps to string | undefined in TypeScript
    sortOrder?: number; // int? in C# maps to number | undefined in TypeScript
    children: ICategoryTreeModel[]; // List<CategoryTreeResponse> in C# maps to CategoryTreeResponse[] in TypeScript
}

export interface ICreateCategoryModel {
    id: string;
    code: string;
    name: string;
    parentId: string | undefined;
    sortOrder: number;
    level: number;
    isActive: boolean;
}

@Injectable({
    providedIn: 'root'
})
export class CategoryService {

    private apiUrl = `${environment.apiUrlV1}/categories`;

    constructor(private http: HttpClient) { }

    getTree(): Observable<ApiResponse<ICategoryTreeModel[]>> {
        const url = `${this.apiUrl}/gettree`;
        return this.http.post<any>(url, {});
    }

    get(id: string): Observable<ICategoryTreeModel> {
        const url = `${this.apiUrl}/${id}`;
        return this.http.get<ICategoryTreeModel>(url);
    }

    post(model: ICreateCategoryModel): Observable<ICreateCategoryModel> {
        return this.http.post<ICreateCategoryModel>(this.apiUrl, model);
    }

    put(id: string, model: ICreateCategoryModel): Observable<ICreateCategoryModel> {
        const url = `${this.apiUrl}/${id}`;
        return this.http.put<ICreateCategoryModel>(url, model);
    }

    postUpload(formData: FormData): Observable<any> {
        const url = `${this.apiUrl}/upload`;
        return this.http.post<ICategoryTreeModel>(url, formData);
    }

    deleteCustomer(id: number): Observable<void> {
        const url = `${this.apiUrl}/${id}`;
        return this.http.delete<void>(url);
    }
}