using OpenQA.Selenium;

namespace TestAutomation.Framework.Extensions
{
    internal static class DriverExtensions
    {
        static Screenshot TakeScreenshot(this IWebDriver driver) => (driver as ITakesScreenshot)?.GetScreenshot();
    }
}
