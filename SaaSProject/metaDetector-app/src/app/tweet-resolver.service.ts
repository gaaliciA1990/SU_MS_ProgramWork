import { Injectable } from '@angular/core';
import { Resolve } from '@angular/router'
import { Observable } from 'rxjs';
import { MetadetectorApiService } from './metadetector-api.service';
import { TweetModel } from './_models/TweetModel';

@Injectable({
  providedIn: 'root'
})
export class TweetResolverService implements Resolve<TweetModel[]> {

  constructor(private metaApiService: MetadetectorApiService) { }

  resolve(): Observable<TweetModel[]>|Promise<TweetModel[]>|TweetModel[] {
    return this.metaApiService.getTweets();
  }
}
