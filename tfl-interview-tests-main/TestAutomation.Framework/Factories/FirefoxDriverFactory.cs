using System;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using TestAutomation.Framework.Interfaces;

namespace TestAutomation.Framework.Factories
{
    public class FireFoxDriverFactory : IWebDriverFactory
    {
        public string GeckoDriverPath { get; set; } = @".\\";
        public TimeSpan WebDriverTimeout { get; set; } = TimeSpan.FromSeconds(5);
        public bool Headless { get; set; } = false;
        public IWebDriver Create()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var firefoxOptions = new FirefoxOptions
            {
                Profile = new FirefoxProfile
                {
                    DeleteAfterUse = true
                }
            };

            if (Headless)
            {
                firefoxOptions.AddArguments("--headless");
            }

            return new FirefoxDriver(FirefoxDriverService.CreateDefaultService(GeckoDriverPath), firefoxOptions, WebDriverTimeout);
        }
    }
}