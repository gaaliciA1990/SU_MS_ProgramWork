import { Injectable } from '@angular/core';
import {HttpClient, HttpRequest } from '@angular/common/http';
import { TweetModel } from './_models/TweetModel';
import { TileModel } from './_models/TileModel';
import { MarketplaceModel } from './_models/MarketplaceModel';


@Injectable({
  providedIn: 'root'
})
export class MetadetectorApiService {
   //hostUrl : string = 'https://metadetector.azurewebsites.net/app/';
  //hostUrl : string = 'http://localhost:8080/app/';
  hostUrl: string = '/app/';

  constructor(private http: HttpClient) { }

  getTweets(){
    return this.http.get<TweetModel[]>(this.hostUrl + 'tweets');
  }

  getTileByType(typeValue: string) {
    return this.http.get<TileModel[]>(this.hostUrl + 'tile/type/' + typeValue);
  }

  getAllTiles() {
    return this.http.get<TileModel[]>(this.hostUrl + 'allTiles');
  }

  getEstateByType(typeValue: string) {
    return this.http.get<number[]>(this.hostUrl + 'estates/type/' + typeValue);
  }

  getAllTilesInEstate(estateId: string) {
    return this.http.get<TileModel[]>(this.hostUrl + 'tile/estate/' + estateId);
  }

  getMarketplaceModel(){
    return this.http.get<MarketplaceModel[]>(this.hostUrl + 'marketplace/allSales');
  }
}
