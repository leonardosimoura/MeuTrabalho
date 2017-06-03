import { Component, OnInit, AfterViewInit, ViewChildren, ElementRef, QueryList, ViewContainerRef } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl, FormControlName } from "@angular/forms";
import { Router } from "@angular/router";
import { Observable } from 'rxjs/Observable';

import { CustomValidators, CustomFormsModule } from 'ng2-validation';
import { GenericValidator } from "../../utils/generic-form-validator";

import 'rxjs/add/operator/debounceTime';
import 'rxjs/add/observable/fromEvent';
import 'rxjs/add/observable/merge';

import { RegistroUsuario } from "../../usuario/registroUsuario";
import { UsuarioService } from "../../usuario/usuarioService";
import { ToastsManager } from "ng2-toastr/ng2-toastr";
import { Toast } from "ng2-toastr/src/toast";

@Component({
  selector: 'app-registro-usuario',
  templateUrl: './registro-usuario.component.html',
  styleUrls: ['./registro-usuario.component.css']
})
export class RegistroUsuarioComponent implements OnInit, AfterViewInit {
@ViewChildren(FormControlName, { read: ElementRef }) formInputElements: QueryList<ElementRef>;

  public errors: any[] = [];
  public registroForm: FormGroup;
  public displayMessage: { [key: string]: string } = {};
  private validationMessages: { [key: string]: { [key: string]: string } };
  private genericValidator: GenericValidator;
  registroUsuario: RegistroUsuario;

  constructor(private fb: FormBuilder,
              private usuarioService: UsuarioService,
              private router: Router ,
              private toastr: ToastsManager, 
              vcr: ViewContainerRef) {
           this.toastr.setRootViewContainerRef(vcr);

    this.validationMessages = {
      nome: {
        required: 'O Nome é requerido.',
        minlength: 'O Nome precisa ter no mínimo 2 caracteres',
        maxlength: 'O Nome precisa ter no máximo 150 caracteres'
      },
      email: {
        required: 'Informe o e-mail',
        email: 'Email invalido'
      },
      senha: {
        required: 'Informe a senha',
        minlength: 'A senha deve possuir no mínimo 6 caracteres'
      },
      confirmaSenha: {
        required: 'Informe a senha novamente',
        minlength: 'A senha deve possuir no mínimo 6 caracteres',
        equalTo: 'As senhas não conferem'
      }
    };

    this.genericValidator = new GenericValidator(this.validationMessages);
    this.registroUsuario = new RegistroUsuario();
  }

  ngOnInit() {
    let senha = new FormControl('', [Validators.required, Validators.minLength(6)]);
    let confirmaSenha = new FormControl('', [Validators.required, Validators.minLength(6), CustomValidators.equalTo(senha)]);

    this.registroForm = this.fb.group({
      nome: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(150)]],
      email: ['', [Validators.required, CustomValidators.email]],
      senha: senha,
      confirmaSenha: confirmaSenha
    });
  }

  ngAfterViewInit(): void {

    console.log(this.formInputElements);

    let controlBlurs: Observable<any>[] = this.formInputElements
      .map((formControl: ElementRef) => Observable.fromEvent(formControl.nativeElement, 'blur'));

    Observable.merge(this.registroForm.valueChanges, ...controlBlurs).debounceTime(500).subscribe(value => {
      this.displayMessage = this.genericValidator.processMessages(this.registroForm);
    });
  }

   registrarUsuario():void{
    if (this.registroForm.dirty && this.registroForm.valid){
        let model = Object.assign({},this.registroUsuario,this.registroForm.value);
        
        this.usuarioService.registrarUsuario(model)
          .subscribe(
            result => {this.onSaveUsuarioComplete(result)},
            error => {this.errors = JSON.parse(error._body).errors}
          );
    }
  }

  onSaveUsuarioComplete(response: any):void{
      this.registroForm.reset();
      this.errors = [];
      this.toastr.success('Login efetuado com sucesso!','Bem vindo!', { dismiss: 'controlled' })
      .then((toast: Toast) => {
        setTimeout(() => 
        {
          this.toastr.dismissToast(toast);
          this.router.navigate(['/home']);
        },2500);
      });
      
      //localStorage.setItem('app.token',response.result.token);
      //localStorage.setItem('app.usuario',  JSON.stringify( response.result.usuario ));
      //this.router.navigate(['/proximos-eventos']);
  }
}
