import { Component,OnInit } from '@angular/core';
import { ILeaveRequest } from 'src/app/interfaces/leave-request';
import { LeaveRequestService } from 'src/app/services/leave-request.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-manage-leaves',
  templateUrl: './manage-leaves.component.html',
  styleUrls: ['./manage-leaves.component.css']
})
export class ManageLeavesComponent implements OnInit {
  leaveRequests: ILeaveRequest[] = [];

  constructor(private leaverequestService: LeaveRequestService,
    private router: Router) { }

  ngOnInit() {
    if (sessionStorage.getItem("userId") == null) {
      this.router.navigate(['/']);
      return;
    }
    if (sessionStorage.getItem("role") != "Admin") {
      this.router.navigate(['/dashboard']);
      return;
    }
    this.getAllLeaveRequests();
  }
  getAllLeaveRequests() {
    this.leaverequestService.getAllLeaveRequests().subscribe(
      response => {
        if (response.success) {
          this.leaveRequests = response.data;
        }
        else {
          alert(response.message);
        }
      },
      error => {
        console.log(error);
        alert("server error");
      }
    );
  }
  approveLeave( id: number) {


    this.leaverequestService.updateLeaveStatus(id,"Approved").subscribe(

      response => {
        if (response.result == 1) {
          alert(response.message);
          this.getAllLeaveRequests();        }
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
  rejectLeave(id:number) {
  

    this.leaverequestService.updateLeaveStatus(id, "Rejected").subscribe(
      response => {
        if (response.result == 1) {
          alert(response.message);
          this.getAllLeaveRequests();        }
        else {
          alert(response.message);
        }
      },
      error => {
        console.log(error);
        alert("server error");
      }
    );
  }
}




