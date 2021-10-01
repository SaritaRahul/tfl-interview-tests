using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TestAutomation.Framework.Interfaces
{
    public interface IWebDriverManager
    {
        IWebDriver WebDriver { get; set; }
        WebDriverWait WebDriverWait { get; set; }
        string RootUrl { get; set; }
    }
}