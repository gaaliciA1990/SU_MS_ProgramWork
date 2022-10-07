import { Injectable } from '@angular/core';
import {HttpClient, HttpRequest } from '@angular/common/http';
import { UserModel } from './_models/UserModel';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  //hostUrl : string = 'https://metadetector.azurewebsites.net/app/';
  //hostUrl : string = 'http://localhost:8080/';
  hostUrl: string = '/';

  constructor(private http:HttpClient) { }

  login(){
    window.location.href = this.hostUrl + 'auth/google';
  }

  logout(){
    //Make call to TokenService.signout()
    return this.http.delete(this.hostUrl + 'auth/user/logout');
  }

  getLoginStatus(): Observable<string>{
    return this.http.get<string>(this.hostUrl + 'auth/user/loggedIn');
  }
}
