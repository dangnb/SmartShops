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

export interface IUserModel {
  id: string;
  userName: string;
  fullName: string;
  taxCode: string;
  lastName: string;
  dayOfBirth: string;
  email: string;
  phoneNumber: string;
  address: string;
  roleCodes:string[]
}

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private apiUrl = `${environment.apiUrlV1}/users`;

  constructor(private http: HttpClient) {}

  getUsers(dataTablesParameters: any): Observable<any> {
    const url = `${this.apiUrl}/filter`;
    return this.http.post<any>(url, dataTablesParameters);
  }

  getAll(): Observable<any> {
    const url = `${this.apiUrl}/getusers`;
    return this.http.get<any>(url);
  }

  getUser(id: string): Observable<IUserModel> {
    const url = `${this.apiUrl}/${id}`;
    return this.http.get<IUserModel>(url);
  }

  createUser(user: IUserModel): Observable<IUserModel> {
    return this.http.post<IUserModel>(this.apiUrl, user);
  }

  addDistrictForUser(id: string, districtCodes: string[]): Observable<IUserModel> {
    const url = `${this.apiUrl}/adddistrictforuser/${id}`;
    return this.http.put<IUserModel>(url, {districtCodes});
  }

  getDistrictCode(id: string): Observable<any> {
    const url = `${this.apiUrl}/getdistrictcode/${id}`;
    return this.http.get<any>(url);
  }

  updateUser(id: string, user: IUserModel): Observable<IUserModel> {
    const url = `${this.apiUrl}/${id}`;
    return this.http.put<IUserModel>(url, user);
  }

  deleteUser(id: string): Observable<void> {
    const url = `${this.apiUrl}/${id}`;
    return this.http.delete<void>(url);
  }
}
