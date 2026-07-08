import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ILeaveRequest } from '../interfaces/leave-request';


@Injectable({
  providedIn: 'root'
})
export class LeaveRequestService {


  private apiurl = "https://localhost:44354/api/LeaveRequest"
  constructor(private http: HttpClient) { }
  applyLeave(leaverequest: ILeaveRequest): Observable<any> {
    return this.http.post(this.apiurl + '/ApplyLeave', leaverequest);
  }

  updateLeaveStatus(leaveRequestId: number, status: string): Observable<any> {
    return this.http.put(
      this.apiurl + '/UpdateLeaveStatus?leaveRequestId=' + leaveRequestId + '&status=' + status,
      {}
    );
  }

  cancelLeaveRequest(leaveRequestId: number, employeeId: number): Observable<any> {
    return this.http.put(
      this.apiurl + '/CancelLeaveRequest?leaveRequestId=' + leaveRequestId +
      '&employeeId=' + employeeId,
      {}
    );
  }

  getLeavesByEmployeeId(id: number): Observable<any> {
    return this.http.get(this.apiurl + '/GetLeavesByEmployeeId?employeeId=' + id);
  }

  getAllLeaveRequests(): Observable<any> {
    return this.http.get(this.apiurl + '/GetAllLeaveRequests');
  }


}
