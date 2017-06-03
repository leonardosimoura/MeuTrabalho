
	Funcionalidade: 2 - Login Usuario
	Um Usuario fará login no site
	preenchendo os campos necessários
	ao terminar receberá uma notificacao 
	de sucesso ou de falha 

@TesteAceitacaoLoginUsuario
Cenário: 2_LoginUsuarioSucesso
	Dado que o usuario está no site, na pagina inicial
	E clica no link de login
	E preenche os campos com os valores de login
		| Campo       | Valor                       |
		| Email       | leonardosimoura@gmail.com	|
		| Senha       | Teste@123                   |
	Quando clicar no botao login
	Entao Recebe uma mensagem com sucesso 
	Entao e eh direcionado para tela inicial
	
	