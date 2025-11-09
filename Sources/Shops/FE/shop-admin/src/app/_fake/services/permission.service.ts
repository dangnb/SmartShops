import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, mergeMap, of, tap } from 'rxjs';
import { environment } from 'src/environments/environment';

export interface DataTablesResponse {
  draw?: number;
  recordsTotal: number;
  recordsFiltered: number;
  data: any[];
}

export interface IPermissionModel {
  id: string;
  description: string;
  code: string;
  groupCode: string;
  groupName: string;
  created_at?: string;
  updated_at?: string;
}

@Injectable({
  providedIn: 'root'
})
export class PermissionService {
  private apiUrl = environment.apiUrlV1 +"/Permissions";

  constructor(private http: HttpClient) { }

  getPermissions(dataTablesParameters: any): Observable<any> {
    const url = `${this.apiUrl}/filter`;
    return this.http.post<any>(url, dataTablesParameters);
  }

  getPermission(id: number): Observable<IPermissionModel> {
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

  deletePermission(id: number): Observable<void> {
    const url = `${this.apiUrl}/${id}`;
    return this.http.delete<void>(url);
  }
  
}
