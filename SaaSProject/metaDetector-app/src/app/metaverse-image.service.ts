import { Injectable } from '@angular/core';
import { HttpClient, HttpRequest } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})

export class MetaverseImageService {
  hostUrl: string = 'https://api.decentraland.org/v1';

  constructor(private http: HttpClient) { }

  getEstateMap(estateId: number) {
    return `${this.hostUrl}/estates/${estateId}/map.png`;
  }

  getTileMap(xCor: number, yCor: number) {
    return `${this.hostUrl}/parcels/${xCor}/${yCor}/map.png`;
  }
  
}
