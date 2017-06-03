import { Component, OnInit } from '@angular/core';
import { UsuarioLogado } from "../../usuario/usuarioLogado";

@Component({
  selector: 'app-menu-login',
  templateUrl: './menu-login.component.html',
  styleUrls: ['./menu-login.component.css']
})
export class MenuLoginComponent implements OnInit {
public token;
public user;
public nome:string;
  constructor() {
        this.token = localStorage.getItem('app.token');
        this.user = JSON.parse(localStorage.getItem('app.user'));   
   }
    usuarioLogado(): boolean{
    return this.token !== null;
  }

   logout(){
    localStorage.removeItem('app.token');
    localStorage.removeItem('app.user');
   }

  ngOnInit() {    
    if  (this.user){
        this.nome = this.user.nome;
    }
  }
}
