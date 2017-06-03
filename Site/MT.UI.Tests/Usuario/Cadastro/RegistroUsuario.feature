Funcionalidade: 1 - Registro novo Usuario
	Um Usuario fará seu cadastro pelo site
	preenchendo os campos necessários
	ao terminar receberá uma notificacao 
	de sucesso ou de falha

@TesteAceitacaoCadastroUsuario
Cenário: 1_CadastroUsuarioSucesso
	Dado que o usuario está no site, na pagina inicial
	E clica no link de registro
	E preenche os campos com os valores de cadastro
		| Campo       | Valor                       |
		| Nome        | Leonardo					|
		| Email       | leonardosimoura@gmail.com	|
		| Senha       | Teste@123                   |
		| ConfirmaSenha| Teste@123                  |
	Quando clicar no botao registrar
	Entao Recebe uma mensagem com sucesso

