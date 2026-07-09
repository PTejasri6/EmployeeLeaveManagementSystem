import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { LoginComponent } from './components/login/login.component';
import { AdminLayoutComponent } from './components/admin-layout/admin-layout.component';
import { EmployeeLayoutComponent } from './components/employee-layout/employee-layout.component';


import { ApplyLeaveComponent } from './components/apply-leave/apply-leave.component';
import { EmployeesComponent } from './components/employees/employees.component';
import { LeaveTypesComponent } from './components/leave-types/leave-types.component';

import { ManageLeavesComponent } from './components/manage-leaves/manage-leaves.component';
import { MyLeavesComponent } from './components/my-leaves/my-leaves.component';
import { DepartmentsComponent } from './components/departments/departments.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { AddEmployeeComponent } from './components/add-employee/add-employee.component';

const routes: Routes = [
  { path: "", component: LoginComponent },
  { path: 'admin', component: AdminLayoutComponent },
  { path: 'employee', component: EmployeeLayoutComponent },
  { path: 'applyleave', component: ApplyLeaveComponent },
  { path: 'employees', component: EmployeesComponent },
  { path: 'leaveType', component: LeaveTypesComponent },
  { path: 'manageleaves', component: ManageLeavesComponent },
  { path: 'myleaves', component: MyLeavesComponent },
  { path: 'departments', component: DepartmentsComponent },
  { path: 'leavetypes',component:LeaveTypesComponent},
  { path: 'dashboard', component: DashboardComponent },
  { path: 'addemployee', component: AddEmployeeComponent }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
