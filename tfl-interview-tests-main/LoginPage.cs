using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TestAutomation.Framework.BasePages;
using TestAutomation.Framework.Helpers;
using TestAutomation.Framework.Interfaces;

namespace TestAutomation.PageObjects.Pages
{
    // This page and all other page classes should inherit from BasePage<T>

       public class LoginPage : BasePage<LoginPage>
    {
      
        internal LoginPage(IWebDriverManager webDriverManager) : base(webDriverManager)
        {
        }

        private IWebElement LoginButton => WebDriver.FindElement(By.ClassName("fa-sign-in"));
        private IWebElement UsernameField => WebDriver.FindElement(By.Id("username"));
        private IWebElement PasswordField => WebDriver.FindElement(By.Id("password"));
        private IWebElement Banner => WebDriver.FindElement(By.Id("flash"));

        private IWebElement JourneyFrom3n => WebDriver.FindElement(By.Id("InputFrom"));

        private IWebElement JourneyTo => WebDriver.FindElement(By.Id("InputTo"));

        private IWebElement PlanmyJourney => WebDriver.FindElement(By.Id("plan-journey-button"));

        private IWebElement PlanmyJourneyerror => WebDriver.FindElement(By.Id("InputTo-error"));

        private IWebElement UpdateJourney => WebDriver.FindElement(By.Id("plan-journey-button"));

        
        private IWebElement PlanJourneyConfirmation => WebDriver.FindElement(By.ClassName("journey-form"));

        private IWebElement destinationedit => WebDriver.FindElement(By.ClassName("jpTo tt-input"));

        
        private IWebElement PlanJourneyerror_Invalidinput => WebDriver.FindElement(By.XPath("//li[contains(text(),'Sorry, we can't find a journey matching your crite')]"));


        private IWebElement EditJourney => WebDriver.FindElement(By.XPath(("//a[contains(.,'javascript:void(0);')]")));

        private IWebElement ELemntFastestroute => WebDriver.FindElement(By.ClassName("visually-hidden"));

        private IWebElement Webllist => WebDriver.FindElement(By.ClassName("disambiguation-link clearfix"));
        
         [FindsBy(How = How.XPath, Using = "//div[contains(@class, 'full-map-container')]")]
        private IList<IWebElement> ELemntFastestroute1;

       
        public string BannerText => Banner.Text;
        public bool PageLoaded => LoginButton.Displayed;

        public bool PageJourneyLoaded => PlanmyJourney.Enabled;

        public LoginPage Navigate()
        {
            WebDriverManager.WebDriver.Navigate().GoToUrl(WebDriverManager.RootUrl);
            return this;
        }

        public LoginPage NavigateBack()
        {
            WebDriverManager.WebDriver.Navigate().Back();
            return this;
        }

        public SecureAreaPage LoginWithValidUsernameAndPassword(string username, string password)
        {
            Login(username, password);
            return new SecureAreaPage(WebDriverManager).WaitForPageLoad();
        }

        public LoginPage LoginWithInvalidUsernameAndPassword(string username, string password)
        {
            Login(username, password);
            return this;
        }

        public override LoginPage WaitForPageElements()
        {
            WebDriverWait.Until(driver => PageLoaded);
            return this;
        }

        private void Login(string username, string password)
        {
            UsernameField.SendKeys(username);
            PasswordField.SendKeys(password);
            LoginButton.Click();
        }

        public void InputJourneydetail()
        {
            JourneyFrom3n.SendKeys("London Victoria Rail Station");
            JourneyTo.SendKeys("London Bridge");
            JourneyTo.SendKeys(Keys.ArrowDown);
            JourneyTo.SendKeys(Keys.Enter);
            Thread.Sleep(10);
            if (PlanmyJourney.Displayed)
            {
             PlanmyJourney.Click(); }
                      
        }

        public void IncorrectInputJourneydetail()
        {
            JourneyFrom3n.SendKeys("xcd");
            JourneyTo.SendKeys("xcd");
        }

        public void IncorrectInputJourneydetail_withoutdestiantion()
        {
            JourneyFrom3n.SendKeys("xcd");           
            PlanmyJourney.Click();
        }

        public void clickonPLanMyJourney()
        {
            Thread.Sleep(10);
            PlanmyJourney.Click();

        }

        public void FastestRoute()
        {
            Thread.Sleep(1000);
            Assert.IsTrue(ELemntFastestroute.Displayed, "Fastest route disaplyed");
        }

            public void RequestJourenyConfirmationpage()
        {    
            if (PlanJourneyConfirmation.Displayed)
            {
                var strText = PlanJourneyConfirmation.Text;
                Assert.IsTrue(strText.Contains("Journey"), "Details displayed based on From and To given successfully");
            }
            else
            {
                Webllist.Click();
            }
                      
            
        }

        public void RequestJourenyConfirmationpage_Eror()
        {
            var strText = PlanJourneyConfirmation.Text;
            Assert.IsFalse(strText.Contains("Sorry") , "Error message disaplyed on incorrect journey details");
        }

        public void EditJourneydetails()
        {
            NavigateBack();
            Thread.Sleep(10);
            JourneyFrom3n.Clear();
            JourneyFrom3n.SendKeys("London Victoria Rail Station");
            JourneyTo.Clear();
            JourneyTo.SendKeys("London Waterloo");
            JourneyTo.SendKeys(Keys.ArrowDown);
            JourneyTo.SendKeys(Keys.Enter);
            Thread.Sleep(10);
            PlanmyJourney.Click();            

        }

        public void withoutdestinationerror()
        {
            var strerror = PlanmyJourneyerror.Text;
            Assert.IsTrue(strerror.Contains("The To field is required"), "Error message on without providing destination");
        }

        public void IncorrectJourneynerror()
        {
            var strerror = PlanJourneyerror_Invalidinput.Text;
            Assert.IsTrue(strerror.Contains("Sorry"), "Error message on incorrect journey details");
        }
        
    }
}
