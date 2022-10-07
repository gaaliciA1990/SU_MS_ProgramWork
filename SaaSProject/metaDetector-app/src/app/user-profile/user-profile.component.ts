import { Component, OnInit } from '@angular/core';
import { UserModel } from '../_models/UserModel';
import { Router } from '@angular/router';
import { UserService } from '../user.service';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {
  user?: UserModel;
  favoritesList: Array<Number> = []//[1186, 4321];

  constructor(private userService: UserService, private router: Router) { }

  ngOnInit(): void {
    this.getUserInfo();
  }

  getUserInfo(){
    this.userService.getUserInfo().subscribe( (userData : UserModel) => {
      this.user = userData;
      console.log('ssoId: ' + this.user.ssoID);
      console.log('ssoId: ' + this.user.displayName);
      console.log(userData.favoritesList);
      this.favoritesList = userData.favoritesList;
    });
  }

}
