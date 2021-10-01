using OpenQA.Selenium;
using TestAutomation.Framework.BasePages;
using TestAutomation.Framework.Interfaces;

namespace TestAutomation.PageObjects.Pages
{
    // This page and all other page classes should inherit from BasePage<T>
    public class LoginPage : BasePage<LoginPage>
    {
        internal LoginPage(IWebDriverManager webDriverManager) : base(webDriverManager)
        {
        }

        private IWebElement LoginButton => WebDriver.FindElement(By.ClassName("fa-sign-in"));
        private IWebElement UsernameField => WebDriver.FindElement(By.Id("username"));
        private IWebElement PasswordField => WebDriver.FindElement(By.Id("password"));
        private IWebElement Banner => WebDriver.FindElement(By.Id("flash"));

        public string BannerText => Banner.Text;
        public bool PageLoaded => LoginButton.Displayed;

        public LoginPage Navigate()
        {
            WebDriverManager.WebDriver.Navigate().GoToUrl(WebDriverManager.RootUrl);
            return this;
        }

        public SecureAreaPage LoginWithValidUsernameAndPassword(string username, string password)
        {
            Login(username, password);
            return new SecureAreaPage(WebDriverManager).WaitForPageLoad();
        }

        public LoginPage LoginWithInvalidUsernameAndPassword(string username, string password)
        {
            Login(username, password);
            return this;
        }

        public override LoginPage WaitForPageElements()
        {
            WebDriverWait.Until(driver => PageLoaded);
            return this;
        }

        private void Login(string username, string password)
        {
            UsernameField.SendKeys(username);
            PasswordField.SendKeys(password);
            LoginButton.Click();
        }
    }
}