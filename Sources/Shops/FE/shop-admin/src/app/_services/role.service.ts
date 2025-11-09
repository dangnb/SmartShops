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

export interface IRoleModel {
    id:string;
    name:string;
    description:string;
    permissionCodes:string[];
}

@Injectable({
    providedIn: 'root'
})
export class RoleService {

    private apiUrl = `${environment.apiUrlV1}/roles`;

    constructor(private http: HttpClient) { }

    getRoles(): Observable<any> {
        const url = `${this.apiUrl}/get-list`;
        return this.http.get<any>(url);
    }

    getRole(id: string): Observable<any> {
        const url = `${this.apiUrl}/${id}`;
        return this.http.get<IRoleModel>(url);
    }

    createRole(user: IRoleModel): Observable<IRoleModel> {
        return this.http.post<IRoleModel>(this.apiUrl, user);
    }

    updateRole(id: string, user: IRoleModel): Observable<IRoleModel> {
        const url = `${this.apiUrl}/${id}`;
        return this.http.put<IRoleModel>(url, user);
    }

    deleteRole(id: string): Observable<void> {
        const url = `${this.apiUrl}/${id}`;
        return this.http.delete<void>(url);
    }
}