import { Component,OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { EmployeeService } from 'src/app/services/employee.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  constructor(private router: Router, private employeeService: EmployeeService) { }

  role: string = "";
  leaveBalance: any[] = [];
  name = <string>sessionStorage.getItem("name");
  employeeId = <string>sessionStorage.getItem("employeeId");

  ngOnInit(): void {
    if (sessionStorage.getItem("userId") == null) {
      this.router.navigate(['/']);
      return;
    }

    this.role = sessionStorage.getItem("role") || "";
    if (this.role == "Employee") {
      this.loadLeaveBalance();
    }
  }
  loadLeaveBalance() {

    this.employeeService.getLeaveBalance(Number(this.employeeId)).subscribe(

      response => {

        if (response.success) {

          this.leaveBalance = response.data;

        }

        else {

          alert(response.message);

        }

      },

      error => {

        console.log(error);

        alert("Server Error");

      }

    );

  }

}
