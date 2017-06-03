import { BrowserModule, Title } from '@angular/platform-browser';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from'@angular/router';
import { rootRouterConfig } from'./app.routes';
import { ReactiveFormsModule} from '@angular/forms';

import { GridModule } from '@progress/kendo-angular-grid';
import { DropDownsModule } from '@progress/kendo-angular-dropdowns';
//bootstrap
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import {ToastModule, ToastOptions} from 'ng2-toastr/ng2-toastr';
import { CustomFormsModule } from 'ng2-validation';
//components
import { AppComponent } from './app.component';
import { ListaUsuarioComponent } from './usuario/lista-usuario/lista-usuario.component';
import { MainPrincipalComponent } from './shared/main-principal/main-principal.component';
import { FooterComponent } from './shared/footer/footer.component';
import { MenuSuperiorComponent } from './shared/menu-superior/menu-superior.component';
import { HomeComponent } from './home/home.component';
import { MenuLoginComponent } from './shared/menu-login/menu-login.component';
import { RegistroUsuarioComponent } from './usuario/registro-usuario/registro-usuario.component';
import { LoginUsuarioComponent } from './usuario/login-usuario/login-usuario.component';
// services
import { SeoService } from "./services/seo.service";
import { UsuarioService } from "./usuario/usuarioService";
import { AuthService } from "app/shared/auth-service";

@NgModule({
  declarations: [
    AppComponent,
    ListaUsuarioComponent,
    MainPrincipalComponent,
    FooterComponent,
    MenuSuperiorComponent,
    HomeComponent,
    MenuLoginComponent,
    RegistroUsuarioComponent,
    LoginUsuarioComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    CustomFormsModule,
    HttpModule,
    ReactiveFormsModule,
    RouterModule.forRoot(rootRouterConfig,{useHash: false}),
    CollapseModule.forRoot(),
    CarouselModule.forRoot(),
    ToastModule.forRoot(),
    GridModule,
    DropDownsModule
  ],
  providers: [
    Title,
    SeoService,
    UsuarioService,
    AuthService
    ],
  bootstrap: [AppComponent]
})
export class AppModule { }