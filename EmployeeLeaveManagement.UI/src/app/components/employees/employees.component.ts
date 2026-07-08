import { Component,OnInit } from '@angular/core';
import { EmployeeService } from 'src/app/services/employee.service';
import { IEmployee } from 'src/app/interfaces/employee';
import { Router } from '@angular/router';


@Component({
  selector: 'app-employees',
  templateUrl: './employees.component.html',
  styleUrls: ['./employees.component.css']
})
export class EmployeesComponent implements OnInit {
  employees: IEmployee[] = [];

  constructor(private employeeservice: EmployeeService,private router:Router) { }
  ngOnInit(): void {
    if (sessionStorage.getItem("userId") == null) {
      this.router.navigate(['/']);
      return;
    }
    if (sessionStorage.getItem("role") != "Admin") {
      this.router.navigate(['/dashboard']);
      return;
    }
    this.getemployees();
  }

  getemployees() {
    this.employeeservice.getAllEmployees().subscribe(
      response => {
        if (response.success) {
          console.log(response.data);
          this.employees = response.data;
        }
        else {
          alert(response.message);
          console.log(response.message);

        }
      },
      error => {
        console.log(error);
        alert("server error");
      }
    );
  }

}
