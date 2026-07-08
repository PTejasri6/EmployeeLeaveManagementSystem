import { Component,OnInit } from '@angular/core';
import { ILeaveRequest } from 'src/app/interfaces/leave-request';
import { LeaveRequestService } from 'src/app/services/leave-request.service';
import { Router } from '@angular/router';
import { ILeaveType } from 'src/app/interfaces/leave-type';
import { LeaveTypeService } from 'src/app/services/leave-type.service';

import { NgForm } from '@angular/forms';


@Component({
  selector: 'app-apply-leave',
  templateUrl: './apply-leave.component.html',
  styleUrls: ['./apply-leave.component.css']
})
export class ApplyLeaveComponent implements OnInit {
  leaveTypes: ILeaveType[] = [];
  constructor(private leaveTypeService: LeaveTypeService,
  private leaverequestService: LeaveRequestService, private router: Router) { }
  ngOnInit() {
    if (sessionStorage.getItem("userId") == null) {
      this.router.navigate(['/']);
      return;
    }
    if (sessionStorage.getItem("role") != "Employee") {
      this.router.navigate(['/dashboard']);
      return;
    }

    this.loadleavetypes();
  }
  loadleavetypes() {
    this.leaveTypeService.getAllLeaveTypes().subscribe(
      response => {
        this.leaveTypes = response.data;
      }
    );
  }


  applyLeave(form: NgForm) {
    const leaveRequest: ILeaveRequest = {
      leaveRequestId: 0,
      employeeId: Number( < string > sessionStorage.getItem("employeeId")),
      leaveTypeId: form.value.leaveTypeId,
      fromDate: form.value.fromDate,
      toDate: form.value.toDate,
      reason: form.value.reason,
      appliedDate: new Date(),
      status: "Pending"
    };

    this.leaverequestService.applyLeave(leaveRequest).subscribe(

      response => {

        if (response.success) {

          alert(response.message);

          this.router.navigate(['/myleaves']);

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
