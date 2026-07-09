import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { EmployeesComponent } from './components/employees/employees.component';
import { DepartmentsComponent } from './components/departments/departments.component';
import { LeaveTypesComponent } from './components/leave-types/leave-types.component';
import { ApplyLeaveComponent } from './components/apply-leave/apply-leave.component';
import { MyLeavesComponent } from './components/my-leaves/my-leaves.component';
import { ManageLeavesComponent } from './components/manage-leaves/manage-leaves.component';
import { HttpClientModule } from '@angular/common/http';
import { AdminLayoutComponent } from './components/admin-layout/admin-layout.component';
import { EmployeeLayoutComponent } from './components/employee-layout/employee-layout.component';
import { AddEmployeeComponent } from './components/add-employee/add-employee.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    DashboardComponent,
    EmployeesComponent,
    DepartmentsComponent,
    LeaveTypesComponent,
    ApplyLeaveComponent,
    MyLeavesComponent,
    ManageLeavesComponent,
    AdminLayoutComponent,
    EmployeeLayoutComponent,
    AddEmployeeComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule, HttpClientModule, FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
