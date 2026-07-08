import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ILeaveType } from '../interfaces/leave-type';


@Injectable({
  providedIn: 'root'
})
export class LeaveTypeService {

  private apiurl = "https://localhost:44354/api/LeaveType"
  constructor(private http: HttpClient) { }

  getAllLeaveTypes(): Observable<any> {
    return this.http.get(this.apiurl + '/GetAllLeaveTypes');
  }

  getLeaveTypeById(id: number): Observable<any> {
    return this.http.get(this.apiurl + '/GetLeaveTypeById?id=' + id);
  }
}
