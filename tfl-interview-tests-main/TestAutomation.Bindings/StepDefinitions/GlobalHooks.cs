using BoDi;
using NUnit.Framework;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using TechTalk.SpecFlow;
using TestAutomation.Framework.Factories;
using TestAutomation.Framework.Helpers;
using TestAutomation.Framework.Interfaces;
using TestAutomation.PageObjects.Factories;

namespace TestAutomation.Bindings.StepDefinitions
{
    [Binding]
    public class GlobalHooks
    {
        private readonly ScenarioContext _scenarioContext;

        public GlobalHooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public void BeforeScenario(IObjectContainer objectContainer)
        {
            // Spins up the web browser. Browser type, WebDriverTimout and SiteUrl all specified in the test.runsettings file
            var driver = new WebDriverFactoryRegistry().GetWebDriver(TestContext.Parameters["Browser"], TestContext.Parameters["WebDriverTimeout"]).Create();
            var wait = new WebDriverWait(driver, TimeSpan.Parse(TestContext.Parameters["WebDriverTimeout"]));
            var webDriverManager = new WebDriverManager(driver, wait) { RootUrl = TestContext.Parameters["SiteUrl"] };

            // Here we register the IWebDriverManager Interface using Specflow's context injection (IObjectContainer)
            objectContainer.RegisterInstanceAs<IWebDriverManager>(webDriverManager);
            objectContainer.RegisterTypeAs<PageObjectFactory, IPageObjectFactory>();
        }

        [AfterScenario]
        public void AfterScenario(IObjectContainer objectContainer)
        {
            var driver = objectContainer.Resolve<WebDriverManager>().WebDriver;
            try
            {
                if (_scenarioContext.TestError != null)
                {
                    var screenshot = driver?.TakeScreenshot();
                    if (screenshot != null)
                    {
                        var screenshotFile = $"{Directory.GetCurrentDirectory()}{DateTime.Now:dd.MM.yyyy-HH.mm.ss.ff}.png";
                        File.WriteAllBytes(screenshotFile, screenshot.AsByteArray);
                        TestContext.AddTestAttachment(screenshotFile);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            driver?.Dispose();
        }
    }
}