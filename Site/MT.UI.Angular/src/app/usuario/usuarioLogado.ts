export class UsuarioLogado{
    nome:string;
    email:string; 
    usuarioId:string;
    papeis:Array<string>;
    access_token :string;
    expires_in: number;
}