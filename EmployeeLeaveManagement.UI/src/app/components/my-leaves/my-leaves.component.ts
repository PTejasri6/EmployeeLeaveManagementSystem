import { Component, OnInit } from '@angular/core';
import { LeaveRequestService } from 'src/app/services/leave-request.service';
import { Router } from '@angular/router';
import { ILeaveRequest } from 'src/app/interfaces/leave-request';

@Component({
  selector: 'app-my-leaves',
  templateUrl: './my-leaves.component.html',
  styleUrls: ['./my-leaves.component.css']
})
export class MyLeavesComponent implements OnInit {
  leaves: ILeaveRequest[] = [];
  employeeid = <string>sessionStorage.getItem("employeeId");
  constructor(private leaverequestService: LeaveRequestService, private router: Router) { }


  ngOnInit(): void{
    if (sessionStorage.getItem("userId") == null) {
      this.router.navigate(['/']);
      return;
    }
    if (sessionStorage.getItem("role") != "Employee") {
      this.router.navigate(['/dashboard']);
      return;
    }
    this.loadleaves()
  }
  loadleaves() {
    this.leaverequestService.getLeavesByEmployeeId(Number(this.employeeid)).subscribe(
      response => {
        if (response.success) {
          console.log(response.data);
          this.leaves=response.data
        }
        else {
          console.log(response.message);
        }
      },
      error => {
        console.log(error);
        alert("server error");
      }
    );
  }



  cancelLeave(leaverequestid: any) {
    if (confirm("Are you sure you want to cancel this leave?")) {
      this.leaverequestService.cancelLeaveRequest(leaverequestid, Number(this.employeeid)).subscribe(
        response => {
          if (response.result == 1) {
            alert(response.message);
            this.router.navigate(['/myleaves'])
          }
          else {
            alert(response.message);
            this.router.navigate(['/myleaves'])
          }
        },
        error => {
          console.log(error);
          alert("server error");
        }
      );
    }
  }
}
