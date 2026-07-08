import { Component,OnInit } from '@angular/core';
import { UserService } from 'src/app/services/user.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  email: string = "";
  password: string = "";
  role: string = "";
  constructor(private userService: UserService,
  private router:Router) { }

  ngOnInit() {

  }
  login() {
    this.userService.Login(this.email, this.password).subscribe(
      response => {
        console.log(response);
        if (response.success) {
          sessionStorage.setItem("userId", response.data.userId);
          sessionStorage.setItem("employeeId", response.data.employeeId);
          sessionStorage.setItem("email", response.data.email);
          sessionStorage.setItem("role", response.data.role);
          sessionStorage.setItem("name", response.data.name);

          this.role = response.data.role;
            this.router.navigate(['/dashboard'])
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
}
