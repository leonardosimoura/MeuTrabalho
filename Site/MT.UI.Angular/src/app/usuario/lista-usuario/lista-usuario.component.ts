import { Component, OnInit } from '@angular/core';
import { SeoService, SeoModel } from "app/services/seo.service";
@Component({
  selector: 'app-lista-usuario',
  templateUrl: './lista-usuario.component.html',
  styleUrls: ['./lista-usuario.component.css']
})
export class ListaUsuarioComponent implements OnInit {


ListaUsuarioViewModel: ListaUsuarioViewModel[]= [];


  constructor(seoService: SeoService) { 
    let seoModel: SeoModel = <SeoModel>{
      title: 'Usuários',
      description: 'Lista dos usuários',
      robots: 'Index, Follow',
      keywords: 'usuarios'
    };

    seoService.setSeoData(seoModel);




   for (var i = 1; i <= 20; i++) {
     this.ListaUsuarioViewModel.push({UsuarioId: i.toString(),Nome : "Nome"+i.toString(), Email : "email"+i.toString() + "@email.com"});
   }


  }

  ngOnInit() {
  }

}


export class ListaUsuarioViewModel{

 UsuarioId:AAGUID;
 Nome:string;
 Email:string

}