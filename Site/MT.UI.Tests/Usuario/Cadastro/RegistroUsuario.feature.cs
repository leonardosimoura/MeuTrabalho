﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:2.1.0.0
//      SpecFlow Generator Version:2.0.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace MT.UI.Tests.Usuario.Cadastro
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.1.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class _1_RegistroNovoUsuarioFeature : Xunit.IClassFixture<_1_RegistroNovoUsuarioFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "RegistroUsuario.feature"
#line hidden
        
        public _1_RegistroNovoUsuarioFeature()
        {
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("pt-BR"), "1 - Registro novo Usuario", "\tUm Usuario fará seu cadastro pelo site\r\n\tpreenchendo os campos necessários\r\n\tao " +
                    "terminar receberá uma notificacao \r\n\tde sucesso ou de falha", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public virtual void TestInitialize()
        {
        }
        
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        public virtual void SetFixture(_1_RegistroNovoUsuarioFeature.FixtureData fixtureData)
        {
        }
        
        void System.IDisposable.Dispose()
        {
            this.ScenarioTearDown();
        }
        
        [Xunit.FactAttribute(DisplayName="1_CadastroUsuarioSucesso")]
        [Xunit.TraitAttribute("FeatureTitle", "1 - Registro novo Usuario")]
        [Xunit.TraitAttribute("Description", "1_CadastroUsuarioSucesso")]
        [Xunit.TraitAttribute("Category", "TesteAceitacaoCadastroUsuario")]
        public virtual void _1_CadastroUsuarioSucesso()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("1_CadastroUsuarioSucesso", new string[] {
                        "TesteAceitacaoCadastroUsuario"});
#line 8
this.ScenarioSetup(scenarioInfo);
#line 9
 testRunner.Given("que o usuario está no site, na pagina inicial", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Dado ");
#line 10
 testRunner.And("clica no link de registro", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "Campo",
                        "Valor"});
            table1.AddRow(new string[] {
                        "Nome",
                        "Leonardo"});
            table1.AddRow(new string[] {
                        "Email",
                        "leonardosimoura@gmail.com"});
            table1.AddRow(new string[] {
                        "Senha",
                        "Teste@123"});
            table1.AddRow(new string[] {
                        "ConfirmaSenha",
                        "Teste@123"});
#line 11
 testRunner.And("preenche os campos com os valores de cadastro", ((string)(null)), table1, "E ");
#line 17
 testRunner.When("clicar no botao registrar", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line 18
 testRunner.Then("Recebe uma mensagem com sucesso", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Entao ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.1.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : System.IDisposable
        {
            
            public FixtureData()
            {
                _1_RegistroNovoUsuarioFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                _1_RegistroNovoUsuarioFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion
