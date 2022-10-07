import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FlexLayoutModule } from '@angular/flex-layout';

import { BuyerDdComponent } from './buyer-dd/buyer-dd.component';
import { DecentralandListComponent } from './decentraland-list/decentraland-list.component';
import { MetaHomeComponent } from './meta-home/meta-home.component';
import { SellerDdComponent } from './seller-dd/seller-dd.component';
import { DueDiligenceComponent } from './meta-home/due-diligence/due-diligence.component';
import { MetaMarketplaceComponent } from './meta-home/meta-marketplace/meta-marketplace.component';
import { MetaverseComponent } from './meta-home/metaverse/metaverse.component';
import { TweetComponent } from './meta-home/tweet/tweet.component';
import { HttpClientModule } from '@angular/common/http';
import { NavbarComponent } from './navbar/navbar.component';
import { EstateComponent } from './estate/estate.component';
import { EstateListComponent } from './decentraland-list/estate-list/estate-list.component';
import { MetadetectorApiService } from './metadetector-api.service';
import { MetaverseImageService } from './metaverse-image.service';
import { LoginComponent } from './navbar/login/login.component';
import { UserProfileComponent } from './user-profile/user-profile.component';

@NgModule({
  declarations: [
    AppComponent,
    BuyerDdComponent,
    DecentralandListComponent,
    MetaHomeComponent,
    SellerDdComponent,
    DueDiligenceComponent,
    MetaMarketplaceComponent,
    MetaverseComponent,
    TweetComponent,
    NavbarComponent,
    EstateComponent,
    EstateListComponent,
    LoginComponent,
    UserProfileComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FlexLayoutModule,
  ],
  providers: [
    MetadetectorApiService,
    MetaverseImageService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
