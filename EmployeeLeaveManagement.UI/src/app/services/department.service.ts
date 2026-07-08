import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IDepartment } from '../interfaces/department';


@Injectable({
  providedIn: 'root'
})
export class DepartmentService {

  private apiurl = "https://localhost:44354/api/Department"
  constructor(private http: HttpClient) { }

  getAllDepartments(): Observable<any> {
    return this.http.get(this.apiurl + '/GetAllDepartments');
  }
  getDepartmentById(id: number): Observable<any> {
    return this.http.get(this.apiurl + '/GetDepartmentById?id='+id);
  }
}
