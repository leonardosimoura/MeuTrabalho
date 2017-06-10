import {  Injectable } from "@angular/core";
import { Observable } from "rxjs/Observable";
import { Http, Response, Headers, RequestOptions } from "@angular/http";
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import {Subscription} from 'rxjs/Subscription';

import { RegistroUsuario } from "../usuario/registroUsuario";
import { ServiceBase } from "../shared/service-base";
import { LoginUsuario } from "../usuario/loginUsuario";
import { UsuarioLogado } from "app/usuario/usuarioLogado";


@Injectable()
export class UsuarioService extends ServiceBase {
    constructor(private http: Http){
        super();
    }

    registrarUsuario(usuario: RegistroUsuario ): Observable<RegistroUsuario>{
            let headers = new Headers({ 'Content-Type': 'application/json' });
            let options = new RequestOptions({ headers: headers });

            let response = this.http
                .post(this.UrlService + "usuario/registrar",usuario, options)
                .map(this.extractData)
                .catch(this.serviceError);

            return response;
    }

    logarUsuario(usuario: LoginUsuario ): Observable<UsuarioLogado>{
            let headers = new Headers({ 'Content-Type': 'application/json' });
            let options = new RequestOptions({ headers: headers });

            let response = this.http
                .post(this.UrlService + "usuario/login",usuario, options)
                .map(this.extractData)
                .catch(this.serviceError);

            return response;
    }

    obterAccessToken(usuario: LoginUsuario): Observable<UsuarioLogado>{
            let headers = new Headers({ 'Content-Type': 'application/x-www-form-urlencoded' });
            let options = new RequestOptions({ headers: headers });
            let urlSearchParams = new URLSearchParams();
            
            urlSearchParams.append('grant_type', "password");
            urlSearchParams.append('username', usuario.email);
            urlSearchParams.append('password', usuario.senha);
            let body = urlSearchParams.toString();

            let response = this.http            
                .post(this.UrlService + "usuario/token",body, options)
                .map(this.extractDataToken)
                .catch(this.serviceErrorToken);

            return response;
    }
    
}