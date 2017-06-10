import { Component, OnInit } from '@angular/core';
import { SeoService, SeoModel } from "app/services/seo.service";
import { UsuarioService } from "app/usuario/usuarioService";
@Component({
  selector: 'app-lista-usuario',
  templateUrl: './lista-usuario.component.html',
  styleUrls: ['./lista-usuario.component.css']
})
export class ListaUsuarioComponent implements OnInit {


usuarios: ListaUsuarioViewModel[]= [];
qtdeItens:number;
paginaAtual:number = 1;
  public errors: any[] = [];

  constructor(seoService: SeoService , private  usuarioService: UsuarioService) { 
    let seoModel: SeoModel = <SeoModel>{
      title: 'Usuários',
      description: 'Lista dos usuários',
      robots: 'Index, Follow',
      keywords: 'usuarios'
    };

    seoService.setSeoData(seoModel);

 
    this.carregarUsuarios();

  }

  ngOnInit() {
  }
  
  carregarUsuarios(){
      this.usuarioService.obterUsuarios(this.paginaAtual,2)
              .subscribe(
                result => {this.onObterUsuariosComplete(result)},
                error => {this.errors = JSON.parse(error._body).errors}
              );
  }
   onObterUsuariosComplete(response: any):void{
      this.errors = [];  

      response.itens.forEach(element => {
         this.usuarios.push(element);
      }); 

      this.qtdeItens = response.qtdeItens;
      this.paginaAtual++;
    }

}


export class ListaUsuarioViewModel{
 usuarioId:AAGUID;
 nome:string;
 email:string;
}