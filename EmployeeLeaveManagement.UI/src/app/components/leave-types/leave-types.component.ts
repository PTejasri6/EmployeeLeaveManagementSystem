import { Component,OnInit } from '@angular/core';
import { ILeaveType } from 'src/app/interfaces/leave-type';
import { LeaveTypeService } from 'src/app/services/leave-type.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-leave-types',
  templateUrl: './leave-types.component.html',
  styleUrls: ['./leave-types.component.css']
})
export class LeaveTypesComponent implements OnInit {
  constructor(private leavetypeService: LeaveTypeService,
    private router: Router) { }

  leaveTypes: ILeaveType[] = [];

  ngOnInit() {
    if (sessionStorage.getItem("userId") == null) {
      this.router.navigate(['/']);
      return;
    }
    if (sessionStorage.getItem("role") != "Admin") {
      this.router.navigate(['/dashboard']);
      return;
    }
    this.loadleavetypes()
  }
  loadleavetypes() {
    this.leavetypeService.getAllLeaveTypes().subscribe(
      response => {
        if (response.success) {
          this.leaveTypes = response.data;
        }
        else {
          alert(response.message);
        }
      },
      error => {
        console.log(error);
        alert("server error");
      }
    )
  }
}
