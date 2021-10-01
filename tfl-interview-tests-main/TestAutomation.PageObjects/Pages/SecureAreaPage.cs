using OpenQA.Selenium;
using TestAutomation.Framework.BasePages;
using TestAutomation.Framework.Interfaces;

namespace TestAutomation.PageObjects.Pages
{
    public class SecureAreaPage : BasePage<SecureAreaPage>
    {
        public SecureAreaPage(IWebDriverManager webDriverManager) : base(webDriverManager)
        {
        }

        private IWebElement LogoutButton => WebDriver.FindElement(By.ClassName("icon-signout"));

        public bool PageLoaded => LogoutButton.Displayed;

        public LoginPage LogOut()
        {
            LogoutButton.Click();
            return new LoginPage(WebDriverManager).WaitForPageLoad();
        }

        public override SecureAreaPage WaitForPageElements()
        {
            WebDriverWait.Until(driver => PageLoaded);
            return this;
        }
    }
}