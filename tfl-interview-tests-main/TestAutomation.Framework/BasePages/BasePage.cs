using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using TestAutomation.Framework.Interfaces;

namespace TestAutomation.Framework.BasePages
{
    public abstract class BasePage<T>
    {
        protected BasePage(IWebDriverManager webDriverManager)
        {
            WebDriverManager = webDriverManager;
            PageUrl = WebDriver.Url;
        }

        private const string ReadyStateJavascript = "return document.readyState";
        public string PageUrl { get; set; }
        protected IWebElement Heading2 => WebDriver.FindElement(By.TagName("h2"));
        public string Heading2Text => Heading2.Text;

        protected IWebDriverManager WebDriverManager { get; }
        protected IWebDriver WebDriver => WebDriverManager.WebDriver;
        protected WebDriverWait WebDriverWait => WebDriverManager.WebDriverWait;

        public bool IsPageInReadyState
        {
            get
            {
                try
                {
                    return WebDriver.ExecuteJavaScript<string>(ReadyStateJavascript).Equals("complete");
                }
                catch (Exception)
                {
                    return true;
                }
            }
        }

        public T WaitForPageLoad()
        {
            WebDriverWait.Until(x => IsPageInReadyState);
            return WaitForPageElements();
        }

        public abstract T WaitForPageElements();
    }
}