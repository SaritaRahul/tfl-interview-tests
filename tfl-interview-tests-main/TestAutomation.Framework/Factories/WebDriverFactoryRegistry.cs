using System;
using TestAutomation.Framework.Interfaces;

namespace TestAutomation.Framework.Factories
{
    public class WebDriverFactoryRegistry
    {
        public IWebDriverFactory GetWebDriver(string browser, string timeout)
        {
            var _webDriverTimeout = TimeSpan.Parse(timeout);

            return browser.ToLower() switch
            {
                "firefox" => new FireFoxDriverFactory { WebDriverTimeout = _webDriverTimeout },
                "chrome" => new ChromeDriverFactory { WebDriverTimeout = _webDriverTimeout },
                "ie" => new InternetExplorerDriverFactory(),
                _ => new ChromeDriverFactory { WebDriverTimeout = _webDriverTimeout }
            };
        }
    }
}