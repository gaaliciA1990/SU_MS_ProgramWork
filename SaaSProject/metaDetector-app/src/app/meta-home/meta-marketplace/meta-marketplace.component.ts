import { Component, OnInit } from '@angular/core';
import {MarketplaceModel} from 'src/app/_models/MarketplaceModel';
import { MetadetectorApiService } from 'src/app/metadetector-api.service';

@Component({
  selector: 'app-meta-marketplace',
  templateUrl: './meta-marketplace.component.html',
  styleUrls: ['./meta-marketplace.component.css']
})
export class MetaMarketplaceComponent implements OnInit {

  results : Array<MarketplaceModel>= [];

  constructor(private MetaService: MetadetectorApiService) { }

  ngOnInit(): void {
    this.MetaService.getMarketplaceModel().subscribe( (result: MarketplaceModel[]) => {
      this.results = result;
      console.log('result: ' + JSON.stringify(this.results));
    });
  }

}
