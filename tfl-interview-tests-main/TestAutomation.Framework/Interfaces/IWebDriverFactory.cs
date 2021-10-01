using OpenQA.Selenium;

namespace TestAutomation.Framework.Interfaces
{
    public interface IWebDriverFactory
    {
        IWebDriver Create();
    }
}