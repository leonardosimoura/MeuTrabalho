import { Component, OnInit, AfterViewInit, ViewChildren, ElementRef, QueryList, ViewContainerRef } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl, FormControlName } from "@angular/forms";
import { Router } from "@angular/router";
import { Observable } from 'rxjs/Observable';

import { CustomValidators, CustomFormsModule } from 'ng2-validation';
import { GenericValidator } from "../../utils/generic-form-validator";

import 'rxjs/add/operator/debounceTime';
import 'rxjs/add/observable/fromEvent';
import 'rxjs/add/observable/merge';

import { UsuarioService } from "../../usuario/usuarioService";
import { LoginUsuario } from "../../usuario/loginUsuario";


import { ToastsManager, Toast } from 'ng2-toastr/ng2-toastr';

@Component({
  selector: 'app-login-usuario',
  templateUrl: './login-usuario.component.html',
  styleUrls: ['./login-usuario.component.css']
})
export class LoginUsuarioComponent implements OnInit, AfterViewInit {
@ViewChildren(FormControlName, { read: ElementRef }) formInputElements: QueryList<ElementRef>;

  public errors: any[] = [];
  public loginForm: FormGroup;
  public displayMessage: { [key: string]: string } = {};
  private validationMessages: { [key: string]: { [key: string]: string } };
  private genericValidator: GenericValidator;
  loginUsuario: LoginUsuario;

  constructor(private fb: FormBuilder,
              private usuarioService: UsuarioService,
              private router: Router,
              private toastr: ToastsManager, 
              vcr: ViewContainerRef) {
           this.toastr.setRootViewContainerRef(vcr);

    this.validationMessages = {
      email: {
        required: 'Informe o e-mail',
        email: 'Email invalido'
      },
      senha: {
        required: 'Informe a senha',
        minlength: 'A senha deve possuir no mínimo 6 caracteres'
      }
    };

    this.genericValidator = new GenericValidator(this.validationMessages);
    this.loginUsuario = new LoginUsuario();
  }

  ngOnInit() {
    let senha = new FormControl('', [Validators.required, Validators.minLength(6)]);

    this.loginForm = this.fb.group({
      email: ['', [Validators.required, CustomValidators.email]],
      senha: senha
    });
  }

  ngAfterViewInit(): void {

    let controlBlurs: Observable<any>[] = this.formInputElements
      .map((formControl: ElementRef) => Observable.fromEvent(formControl.nativeElement, 'blur'));

    Observable.merge(this.loginForm.valueChanges, ...controlBlurs).debounceTime(500).subscribe(value => {
      this.displayMessage = this.genericValidator.processMessages(this.loginForm);
    });
  }

   logarUsuario():void{
    if (this.loginForm.dirty && this.loginForm.valid){
        let model = Object.assign({},this.loginUsuario,this.loginForm.value);
        
          this.usuarioService.logarUsuario(model)
            .subscribe(
              result => {this.onLogarUsuarioComplete(result)},
              error => {this.errors = JSON.parse(error._body).errors}
            );
    }
  }


onObterAccessTokenComplete(response: any):void{
      this.loginForm.reset();
      this.errors = [];
      
      localStorage.setItem('app.token', response.access_token);      

      this.toastr.success('Login efetuado com sucesso!','Bem vindo!', { dismiss: 'controlled' })
      .then((toast: Toast) => {
        setTimeout(() => 
        {
          this.toastr.dismissToast(toast);
          this.router.navigate(['/home']);
        },2500);
      });
    }

  onLogarUsuarioComplete(response: any):void{
      //this.loginForm.reset();
      this.errors = [];

      localStorage.setItem('app.user', JSON.stringify(response.usuario));
      let model = Object.assign({},this.loginUsuario,this.loginForm.value);
      this.usuarioService.obterAccessToken(model)
          .subscribe(
            result => {this.onObterAccessTokenComplete(result)},
            error => {
              this.errors = [ JSON.parse(error._body).error_description];
              this.toastr.error(JSON.parse(error._body).error_description,'Error!');
           }
          );

      
    }
}
