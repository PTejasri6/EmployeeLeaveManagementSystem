import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IEmployee } from '../interfaces/employee';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  private apiUrl = 'https://localhost:44354/api/Employee';

  constructor(private http: HttpClient) { }

  getAllEmployees(): Observable<any> {
    return this.http.get(this.apiUrl + '/GetAllEmployees');
  }

  getEmployeeById(id: number): Observable<any> {
    return this.http.get(this.apiUrl + '/GetEmployeeById?id=' + id);
  }

  addEmployee(employee: IEmployee): Observable<any> {
    return this.http.post(this.apiUrl + '/AddEmployee', employee);
  }

  updateEmployee(employee: IEmployee): Observable<any> {
    return this.http.put(this.apiUrl + '/UpdateEmployee', employee);
  }

  deleteEmployee(id: number): Observable<any> {
    return this.http.delete(this.apiUrl + '/DeleteEmployee?id=' + id);
  }


  getLeaveBalance(employeeId: number) {
    return this.http.get<any>(
      this.apiUrl + "/GetLeaveBalance?employeeId=" + employeeId
    );
  }
}
