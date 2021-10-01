using TestAutomation.Framework.Interfaces;
using TestAutomation.PageObjects.Pages;

namespace TestAutomation.PageObjects.Factories
{
    public class PageObjectFactory : IPageObjectFactory
    {
        private readonly IWebDriverManager _webDriverManager;

        public PageObjectFactory(IWebDriverManager webDriverManager)
        {
            _webDriverManager = webDriverManager;
        }

        public LoginPage CreateLoginPage()
        {
            return new LoginPage(_webDriverManager);
        }
    }
}