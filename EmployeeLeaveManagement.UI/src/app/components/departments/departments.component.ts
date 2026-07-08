import { Component,OnInit } from '@angular/core';
import { IDepartment } from 'src/app/interfaces/department';
import { DepartmentService } from 'src/app/services/department.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-departments',
  templateUrl: './departments.component.html',
  styleUrls: ['./departments.component.css']
})
export class DepartmentsComponent implements OnInit {

  departmentid = 2;
  departmentdata: IDepartment[] = [];
  constructor(private departmentservice: DepartmentService,private router:Router) { }
  ngOnInit() {
    if (sessionStorage.getItem("userId") == null) {
      this.router.navigate(['/']);
      return;
    }
    if (sessionStorage.getItem("role") != "Admin") {
      this.router.navigate(['/dashboard']);
      return;
    }
    this.getalldepartment();
  }

  getalldepartment() {
    this.departmentservice.getAllDepartments().subscribe(
      response => {
        if (response.success) {
          this.departmentdata = response.data;
        }
        else {
          alert(response.message);
        }
      },
      error => {
        console.log(error);
        alert("Server error")
      }
    );
  }

  getdepartmentbyid() {
    this.departmentservice.getDepartmentById(this.departmentid).subscribe(
      response => {
        if (response.success) {
          this.departmentdata = response.data;
        }
        else {
          alert(response.message);
        }
      },
      error => {
        console.log(error);
        alert("Server error")
      }
    );
  }

}

