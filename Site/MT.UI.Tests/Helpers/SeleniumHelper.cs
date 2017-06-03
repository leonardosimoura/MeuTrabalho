using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.IO;

namespace MT.UITests.Helpers
{
    public class SeleniumHelper
    {
        public static IWebDriver CD;
        public WebDriverWait Wait;

        private static SeleniumHelper _instance;
        public static SeleniumHelper Instance()
        {
            return _instance ?? (_instance = new SeleniumHelper());
        }

        public SeleniumHelper()
        {
            CD = new ChromeDriver(ConfigurationHelper.ChromeDrive);
            Wait = new WebDriverWait(CD, TimeSpan.FromSeconds(30));
        }

        public void AguardarEmSegundos(int segundos)
        {
            CD.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(segundos);
        }

        public string ObterUrl()
        {
            return CD.Url;
        }

        public bool ValidarConteudoUrl(string conteudo)
        {
            return Wait.Until(ExpectedConditions.UrlContains(conteudo));
        }

        public string NavegarParaSite(string url)
        {
            CD.Navigate().GoToUrl(url);
            return ObterUrl();
        }

        public void ClicarLinkSite(string linkText)
        {
            var link = Wait.Until(ExpectedConditions.ElementIsVisible(By.LinkText(linkText)));
            link.Click();
        }

        public void PreencherTextBox(string idCampo, string valorCampo)
        {
            var campo = Wait.Until(ExpectedConditions.ElementIsVisible(By.Id(idCampo)));
            campo.SendKeys(valorCampo);
        }

        public void ClicarBotaoSite(string botaoId)
        {
            Wait.Until(ExpectedConditions.ElementIsVisible(By.Id(botaoId))).Click();
        }

        public string ObterTextoElementoPorId(string id)
        {
            return Wait.Until(ExpectedConditions.ElementIsVisible(By.Id(id))).Text;
        }

        public string ObterTextoElementoPorClasse(string className)
        {
            return Wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName(className))).Text;
        }

        public IEnumerable<IWebElement> ObterListaPorClasse(string className)
        {
            return Wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.ClassName(className)));
        }

        public void ObterScreenShot(string nome)
        {
            var screenshot = ((ITakesScreenshot)CD).GetScreenshot();
            SalvarScreenShot(screenshot, string.Format("{0}_" + nome + ".png", DateTime.Now.ToFileTime()));
        }

        private static void SalvarScreenShot(Screenshot screenshot, string fileName)
        {
            screenshot.SaveAsFile(string.Format("{0}{1}", ConfigurationHelper.FolderPicture, fileName), ScreenshotImageFormat.Png);
        }

        public void Fechar()
        {
            CD.Close();
            CD.Quit();
        }
    }
}
