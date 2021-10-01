using FluentAssertions;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using TestAutomation.Bindings.Contexts;
using TestAutomation.PageObjects.Factories;

namespace TestAutomation.Bindings.StepDefinitions
{
    [Binding]
    // This class and all other binding classes should inherit from BaseSteps
    public class StepDefinitionExample : BaseSteps
    {
        public StepDefinitionExample(IPageObjectFactory pageObjectFactory, PageContext pageContext) : base(pageObjectFactory, pageContext)
        {
        }

        [Given(@"the Login page has been loaded")]
        public void GivenTheLoginPageHasBeenLoaded()
        {
            PageContext.LoginPage = PageObjectFactory.CreateLoginPage().Navigate();
        }

        [Given(@"I have successfully logged in to the Secure Area")]
        public void GivenIHaveSuccessfullyLoggedInToTheSecureArea()
        {
            var username = TestContext.Parameters["ValidUsername"];
            var password = TestContext.Parameters["ValidPassword"];

            LoginToSecureAreaPage(username, password);
        }

        [When(@"I log in with with the following details:")]
        public void WhenILogInWithWithTheFollowingDetails(Table table)
        {
            var loginDetails = table.CreateInstance<LoginDetails>();

            var username = loginDetails.Username;
            var password = loginDetails.Password;

            LoginToSecureAreaPage(username, password);
        }

        [When(@"I attempt to log in with '(.*)' and '(.*)'")]
        public void WhenIAttemptToLogInWithAnd(string username, string password)
        {
            TestContext.WriteLine($"Attempting log in in with invalid username: {username} and/or password: {password}");

            PageContext.LoginPage.LoginWithInvalidUsernameAndPassword(username, password);
        }

        [When(@"I Logout")]
        public void WhenILogout()
        {
            PageContext.LoginPage = PageContext.SecureAreaPage.LogOut();
        }

        [Then(@"I should be on a page titled '(.*)'")]
        public void ThenIShouldBeOnAPageTitled(string pageTitle)
        {
            PageContext.SecureAreaPage.Heading2Text.Should().Be(pageTitle);
        }

        [Then(@"I should remain on the Login Page")]
        [Then(@"I should return to the Login Page")]
        public void ThenIShouldRemainOnTheLoginPage()
        {
            PageContext.LoginPage.Heading2Text.Should().Be("Login Page");
        }

        [Then(@"the banner should read '(.*)'")]
        public void ThenTheBannerShouldRead(string errorMessage)
        {
            PageContext.LoginPage.BannerText.Should().Contain(errorMessage);
        }

        private void LoginToSecureAreaPage(string username, string password)
        {
            TestContext.WriteLine($"Logging in with username: {username} and password: {password}");

            PageContext.SecureAreaPage =
                PageContext.LoginPage.LoginWithValidUsernameAndPassword(username, password);
        }
    }
}
