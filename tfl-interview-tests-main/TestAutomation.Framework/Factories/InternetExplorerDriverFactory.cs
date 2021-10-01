using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using TestAutomation.Framework.Interfaces;

namespace TestAutomation.Framework.Factories
{
    public class InternetExplorerDriverFactory : IWebDriverFactory
    {
        // ReSharper disable once NotAccessedField.Local
        private readonly string _webDriverTimeout;
        private const string DefaultWebDriverTimeout = "00:00:00:05.000";

        public InternetExplorerDriverFactory(string webDriverTimeout = DefaultWebDriverTimeout)
        {
            _webDriverTimeout = webDriverTimeout;
        }

        public IWebDriver Create()
        {
            var options = new InternetExplorerOptions { IgnoreZoomLevel = true };
            return new InternetExplorerDriver(options);
        }
    }
}