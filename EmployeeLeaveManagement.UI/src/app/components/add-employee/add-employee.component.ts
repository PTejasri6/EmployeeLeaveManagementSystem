import { Component,OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { EmployeeService } from 'src/app/services/employee.service';
import { DepartmentService } from 'src/app/services/department.service';
import { IDepartment } from 'src/app/interfaces/department';

@Component({
  selector: 'app-add-employee',
  templateUrl: './add-employee.component.html',
  styleUrls: ['./add-employee.component.css']
})
export class AddEmployeeComponent implements OnInit {
  departments: IDepartment[] = [];
  constructor(private employeeservice: EmployeeService, private router: Router, private departmentservice: DepartmentService) { }

  ngOnInit() {

    if (sessionStorage.getItem("userId") == null) {
      this.router.navigate(['/']);
      return;
    }
    if (sessionStorage.getItem("role") != "Admin") {
      this.router.navigate(['/dashboard']);
      return;
    }
    this.loadDepartments();
  }
  loadDepartments() {
    this.departmentservice.getAllDepartments().subscribe(
      response => {
        if (response.success) {
          this.departments = response.data;
        }
        else {
          alert(response.message);
        }
      },
      error => {
        alert("Server Error");
      }

    );
  }



  addemployee(form: NgForm) {
    this.employeeservice.addEmployee(form.value).subscribe(
      response => {
        if (response.result == 1) {
          alert(response.message);
          this.router.navigate(['/employees']);
        }
        else {
          alert(response.message);
        }
      },
      error => {
        alert("Server Error");
      }
    );
  }


}
