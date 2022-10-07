import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/auth-service.service';
import { UserModel } from 'src/app/_models/UserModel';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  private user?: UserModel;
  isLoggedIn: Boolean = false;
  //loginFailed: Boolean = false;
  constructor(private authService: AuthService, private router: Router) {

  }

  ngOnInit(): void {
    this.checkLoginStatus();
    
  }

  login(){
    this.authService.login();
  }

  logout(){
    this.authService.logout().subscribe((response: any) => {
      console.log('got the response back');
       this.isLoggedIn = false;
       this.router.navigateByUrl('/#/');
    })
  }

  checkLoginStatus(){
    this.authService.getLoginStatus().subscribe( (result: any ) => {
      this.isLoggedIn = result;
      console.log('value of logged in:' +this.isLoggedIn);
    }
  )}

}
