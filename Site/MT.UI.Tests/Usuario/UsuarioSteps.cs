using MT.UITests.Helpers;
using System;
using TechTalk.SpecFlow;
using Xunit;

namespace MT.UI.Tests
{
    [Binding]

    public class UsuarioSteps
    {
        public SeleniumHelper Browser
        {
            get
            {
                return scenarioContext.Get<SeleniumHelper>("Browser");
            }
        }

        private readonly ScenarioContext scenarioContext;

        public UsuarioSteps(ScenarioContext scenarioContext)
        {
            if (scenarioContext == null) throw new ArgumentNullException("scenarioContext");
            this.scenarioContext = scenarioContext;
        }

        [TechTalk.SpecFlow.AfterScenario]
        public void AfterCenario()
        {
            Browser.Fechar();
        }
        [TechTalk.SpecFlow.BeforeScenario]
        public void BeforeCenario()
        {
            scenarioContext.Add("Browser", new SeleniumHelper());
            //scenarioContext.Add("Browser", SeleniumHelper.Instance());
            //Browser = SeleniumHelper.Instance();
        }

        [Given(@"que o usuario está no site, na pagina inicial")]
        public void DadoQueOUsuarioEstaNoSiteNaPaginaInicial()
        {
            var url = Browser.NavegarParaSite(ConfigurationHelper.SiteUrl);

            Assert.Equal(ConfigurationHelper.SiteUrl, url);
            Browser.ObterScreenShot("PaginaInicial");
        }

        [Given(@"clica no link de registro")]
        public void DadoClicaNoLinkDeRegistro()
        {
            Browser.ClicarLinkSite("Registre-se");
        }

        [Given(@"preenche os campos com os valores de cadastro")]
        public void DadoPreencheOsCamposComOsValoresDeCadastro(Table table)
        {
            Browser.PreencherTextBox("nome", table.Rows[0][1]);
            Browser.PreencherTextBox("email", table.Rows[1][1]);
            Browser.PreencherTextBox("senha", table.Rows[2][1]);
            Browser.PreencherTextBox("confirmaSenha", table.Rows[2][1]);
            Browser.ObterScreenShot("PreencheCamposCadastro");
        }

        [Given(@"preenche os campos com os valores de login")]
        public void DadoPreencheOsCamposComOsValoresDeLogin(Table table)
        {
            Browser.PreencherTextBox("email", table.Rows[0][1]);
            Browser.PreencherTextBox("senha", table.Rows[1][1]);
        }

        [When(@"clicar no botao registrar")]
        public void QuandoClicarNoBotaoRegistrar()
        {
            Browser.ClicarBotaoSite("btnregistrar");
        }

        [Then(@"Recebe uma mensagem com sucesso")]
        public void EntaoRecebeUmaMensagemComSucesso()
        {
            Browser.AguardarEmSegundos(1);
            Browser.ObterScreenShot("MensagemFinal");
            Assert.Contains("SUCESSO", Browser.ObterTextoElementoPorClasse("toast").ToUpper());
        }

        [Then(@"Fecha o navegador")]
        public void EntaoFechaONavegador()
        {
            Browser.Fechar();
        }

        [Then(@"e eh direcionado para tela inicial")]
        public void EntaoEEhDirecionadoParaTelaInicial()
        {
            var url = Browser.NavegarParaSite(ConfigurationHelper.SiteUrl);

            Assert.Equal(ConfigurationHelper.SiteUrl, url);
        }

        [Given(@"clica no link de login")]
        public void DadoClicaNoLinkDeLogin()
        {
            Browser.ClicarLinkSite("Entrar");
        }

        [When(@"clicar no botao login")]
        public void QuandoClicarNoBotaoLogin()
        {
            Browser.ClicarBotaoSite("btnlogin");
        }
        
    }
}
