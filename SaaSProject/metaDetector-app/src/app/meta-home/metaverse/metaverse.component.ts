import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';


@Component({
  selector: 'app-metaverse',
  templateUrl: './metaverse.component.html',
  styleUrls: ['./metaverse.component.css']
})
export class MetaverseComponent implements OnInit {

  constructor(private router: Router) { }

  ngOnInit(): void {
  }

  redirect() {
    this.router.navigate(['decentraland']);
  }
}
