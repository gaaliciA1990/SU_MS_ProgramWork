import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserModel } from './_models/UserModel';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  //hostUrl : string = 'https://metadetector.azurewebsites.net/app/';
  //hostUrl : string = 'http://localhost:8080/';
  hostUrl: string = '/';

  constructor(private http:HttpClient) { }

  getUserInfo(){
    return this.http.get<UserModel>(this.hostUrl + 'auth/user/info');
  }

  addNewFavorite(id: Number){
    //doesn't need to obtain user ID because passport on the backend already keep track of that
    //Placeholder
    return this.http.put(this.hostUrl + 'app/user/favoritesList', {estateID: id});
  }

}
