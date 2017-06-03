import { Injectable } from "@angular/core";
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from "@angular/router";

@Injectable()
export class AuthService implements CanActivate {
    public token: string;
    public user;

    constructor(private router: Router){
        //this.token = localStorage.getItem("app.token");
        //this.user = JSON.parse(localStorage.getItem('app.user'));
    }


    canActivate(routeAc: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {

        this.token = localStorage.getItem("app.token");
        this.user = JSON.parse(localStorage.getItem('app.user'));

        if(!this.token)
        {
            this.router.navigate(['/entrar']);
            return false;
        }

        let papeisData = routeAc.data as Array<any>;

        if  (papeisData){

        let papel = papeisData[0]['papel'];

                if(papel){
                    let papeis = this.user.papeis.some(x => x == papel);
                    if(!papeis){
                        this.router.navigate(['/acesso-negado']);
                        return false;
                    }
                }
        }
        
        

        return true;
    }
}