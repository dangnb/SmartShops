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

export interface IPermissionModel {
    id:string;
    description:string;
    code:string;
    groupCode:string;
    groupName:string;
}

@Injectable({
    providedIn: 'root'
})
export class PermisionService {

    private apiUrl = `${environment.apiUrlV1}/permissions`;

    constructor(private http: HttpClient) { }

    getPermissions(dataTablesParameters: any): Observable<any> {
        const url = `${this.apiUrl}/filter`;
        return this.http.post<any>(url, dataTablesParameters);
    }

    getList(): Observable<any> {
        const url = `${this.apiUrl}/get-list`;
        return this.http.get<any>(url);
    }

    getPermission(id: string): Observable<IPermissionModel> {
        const url = `${this.apiUrl}/${id}`;
        return this.http.get<IPermissionModel>(url);
    }

    createPermission(user: IPermissionModel): Observable<IPermissionModel> {
        return this.http.post<IPermissionModel>(this.apiUrl, user);
    }

    updatePermission(id: string, user: IPermissionModel): Observable<IPermissionModel> {
        const url = `${this.apiUrl}/${id}`;
        return this.http.put<IPermissionModel>(url, user);
    }

    deletePermission(id: string): Observable<void> {
        const url = `${this.apiUrl}/${id}`;
        return this.http.delete<void>(url);
    }
}