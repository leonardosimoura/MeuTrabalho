import { Http, Headers, Response, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Observable';

import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/switchMap';
import 'rxjs/add/observable/throw';
import { environment } from "environments/environment";

export abstract class ServiceBase {
    public Token: string = "";

    constructor() {
        this.Token = localStorage.getItem('app.token');

        if (environment.production) {
            this.UrlService = "http://meutrabalhoapp.database.windows.net/api/";
        }else{
            this.UrlService = "http://meutrabalhoapp.database.windows.net/api/";
        }
    }

    protected UrlService: string = "http://localhost:60202/api/";


    protected obterAuthHeader(): RequestOptions {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        headers.append('Authorization', `Bearer ${this.Token}`);
        let options = new RequestOptions({ headers: headers });
        return options;
    }

    protected extractData(response: Response) {
        let body = response.json();
        return body.data || {};
    }

    protected extractDataToken(response: Response) {
        let body = response.json();
        return body || {};
    }

    protected serviceErrorToken(error: Response | any) {
        let errMsg: string;
        if (error instanceof Response) {
            const body = error.json() || '';
            const err = body.errors || JSON.stringify(body);
            errMsg = body.error_description
        } else {
            errMsg = "Ocorreu um erro!";
        }
        console.error(error);
        return Observable.throw(error);
    }

    protected serviceError(error: Response | any) {
        let errMsg: string;
        if (error instanceof Response) {
            const body = error.json() || '';
            const err = body.errors || JSON.stringify(body);
            errMsg = `${error.status} - ${error.statusText || ''} ${err}`;
        } else {
            errMsg = error.message ? error.message : error.toString();
        }
        console.error(error);
        return Observable.throw(error);
    }
}