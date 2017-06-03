import { Component, OnInit } from '@angular/core';
import { SeoModel, SeoService } from "app/services/seo.service";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(seoService: SeoService) { 
    let seoModel: SeoModel = <SeoModel>{
      title: 'Bem vindo!',
      description: 'Home page',
      robots: 'Index, Follow',
      keywords: 'home'
    };

    seoService.setSeoData(seoModel);
  }

  ngOnInit() {
  }

}