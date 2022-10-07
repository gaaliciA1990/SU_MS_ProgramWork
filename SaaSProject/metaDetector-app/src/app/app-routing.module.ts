import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MetaHomeComponent } from './meta-home/meta-home.component';
import { DecentralandListComponent } from './decentraland-list/decentraland-list.component';
import { SellerDdComponent } from './seller-dd/seller-dd.component';
import { BuyerDdComponent } from './buyer-dd/buyer-dd.component';
import { EstateComponent } from './estate/estate.component';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { LoginComponent } from './navbar/login/login.component';
import { TweetResolverService } from './tweet-resolver.service';

const routes: Routes = [
  {path: '', component: MetaHomeComponent, resolve: {tweets : TweetResolverService} }, 
  {path: 'decentraland', component: DecentralandListComponent}, 
  {path: 'decentraland/estate/:estateId', component: EstateComponent}, 
  {path: 'sellerDD', component: SellerDdComponent}, 
  {path: 'buyerDD', component: BuyerDdComponent},
  {path: 'login', component: LoginComponent},
  {path: 'profile', component: UserProfileComponent} //todo: add a guard here
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {useHash: true})],
  exports: [RouterModule]
})
export class AppRoutingModule { }
