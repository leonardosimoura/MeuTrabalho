import {Routes} from '@angular/router';

import { HomeComponent } from './home/home.component';
import { ListaUsuarioComponent } from './usuario/lista-usuario/lista-usuario.component';
import { RegistroUsuarioComponent } from './usuario/registro-usuario/registro-usuario.component';
import { LoginUsuarioComponent } from "./usuario/login-usuario/login-usuario.component";
import { AuthService } from "app/shared/auth-service";

export const rootRouterConfig: Routes =
[
    {path: '',redirectTo: 'home',pathMatch:'full'},
    {path: 'home', component: HomeComponent},
    {path: 'usuarios',canActivate:[AuthService], component: ListaUsuarioComponent, data: [{ papel: "Administrador"}] },
    {path: 'registrar', component: RegistroUsuarioComponent},
    {path: 'entrar', component: LoginUsuarioComponent }
    
]
