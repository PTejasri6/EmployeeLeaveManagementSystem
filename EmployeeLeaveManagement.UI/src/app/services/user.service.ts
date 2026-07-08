import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IUser } from '../interfaces/user';


@Injectable({
  providedIn: 'root'
})
export class UserService {

  private apiurl = "https://localhost:44354/api/User"
  constructor(private http: HttpClient) { }

  Login(email: string, password: string): Observable<any> {
    return this.http.get(this.apiurl + '/Login?email=' + email + '&password=' + password);

  }
}
