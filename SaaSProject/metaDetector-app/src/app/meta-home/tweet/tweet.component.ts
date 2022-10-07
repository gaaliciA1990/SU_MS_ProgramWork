import { Component, OnInit } from '@angular/core';
import { TweetModel } from 'src/app/_models/TweetModel';
import { MetadetectorApiService } from 'src/app/metadetector-api.service';
import { TweetResolverService } from 'src/app/tweet-resolver.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-tweet',
  templateUrl: './tweet.component.html',
  styleUrls: ['./tweet.component.css']
})
export class TweetComponent implements OnInit {

  results : Array<TweetModel>= [];
  counter(i: number){
    return new Array(i);
  }
  index: number = 0;

  getTweet():TweetModel{
    if(this.index >= this.results.length){
      this.index = 0;
    }
    let i = this.index;
    this.index++;
    return this.results[i];
  }

  constructor(
    private MetaService: MetadetectorApiService,
    private activatedRoute: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.activatedRoute.data.subscribe( ({tweets}) => {
      this.results = tweets;
      console.log('result: ' + JSON.stringify(this.results));
    });
  }

}
