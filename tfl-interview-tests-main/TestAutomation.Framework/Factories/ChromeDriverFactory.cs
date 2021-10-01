using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TestAutomation.Framework.Interfaces;

namespace TestAutomation.Framework.Factories
{
    public class ChromeDriverFactory : IWebDriverFactory
    {
        public string ChromeDriverPath { get; set; } = @".\\";
        public TimeSpan WebDriverTimeout { get; set; } = TimeSpan.FromSeconds(5);
        public bool Incognito { get; set; } = true;
        public bool Headless { get; set; } = false;
        public bool DisableGpu { get; set; } = false;
        public bool NoSandBox { get; set; } = true;
        public bool StartMaximised { get; set; } = true;
        public IEnumerable<string> AdditionalArguments { get; set; } = new List<string>();
        public IDictionary<string, string> AdditionalUserProfilePreferences { get; set; } = new Dictionary<string, string>();

        public string DownloadsFolderPath { get; set; } = null;

        public IWebDriver Create()
        {
            var options = new ChromeOptions();
            var arguments = new List<string>();

            if (!string.IsNullOrEmpty(DownloadsFolderPath))
            {
                options.AddUserProfilePreference("download.default_directory", DownloadsFolderPath);
            }

            if (Incognito)
            {
                arguments.Add("incognito");
            }

            if (StartMaximised)
            {
                arguments.Add("start-maximized");
            }

            if (Headless)
            {
                arguments.Add("headless");
            }

            if (DisableGpu)
            {
                arguments.Add("disable-gpu");
            }

            if (NoSandBox)
            {
                arguments.Add("no-sandbox");
            }

            if (AdditionalArguments != null)
            {
                arguments.AddRange(AdditionalArguments);
            }

            options.AddArguments(arguments.Distinct());

            AdditionalUserProfilePreferences?.ToList().ForEach(profilePreference => options.AddUserProfilePreference(profilePreference.Key, profilePreference.Value));

            options.UnhandledPromptBehavior = UnhandledPromptBehavior.Accept;
            options.PageLoadStrategy = PageLoadStrategy.Normal;
            options.SetLoggingPreference(LogType.Browser, LogLevel.All);
            options.LeaveBrowserRunning = false;

            var chromeDriverService = ChromeDriverService.CreateDefaultService(ChromeDriverPath);
            chromeDriverService.HideCommandPromptWindow = true;
            return new ChromeDriver(chromeDriverService, options, WebDriverTimeout);
        }
    }
}