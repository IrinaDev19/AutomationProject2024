using AutomationProject2024.PageObjectModel;
using AutomationProject2024.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;
/*  to have these namespaces you need to add in solution 
from ManageNuGet Packages the following:
Selenium.Webdriver
Selenium.Webdriver.ChromeDriver
Selenium.Support*/

namespace AutomationProject2024
{
    [TestClass]
    public class MagentoTests
    {
        private ChromeDriver driver = new ChromeDriver();
        private CookieConsent cookieConsent;
        private MenuItemsBeforeSignIn menuItemsBeforeSignIn;
        private LoginPage loginPage;
        private HomePage homePage;

        [TestInitialize]
        public void Setup()
        {
            ChromeOptions options = new ChromeOptions();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://magento.softwaretestingboard.com/");
            loginPage = new LoginPage(driver);
            menuItemsBeforeSignIn = new MenuItemsBeforeSignIn(driver);
            cookieConsent = new CookieConsent(driver);
            cookieConsent.GoToMenuAfterCookieAccept();
        }

        [TestMethod]
        public void LoginValidAccount()
        {
            menuItemsBeforeSignIn.GoToLogin();

            loginPage.SignInAccount(Resources.email, Resources.password);

            // Console.WriteLine("You are logged in on:" + driver.Url);
            homePage = new HomePage(driver);

            Thread.Sleep(2000);

            Assert.IsTrue(homePage.GetWelcomeText().Contains(Resources.welcomeMessage), ValidationText.UnknownText);
        }

        //This test should be refactorized
        [TestMethod]
        public void Should_RedirectToAnAlreadyCreatedAccountPage_When_TheAccountWasCreatedBefore()
        {
            //Locate Create an Account link and click to go to the page 
            driver.FindElement(By.LinkText("Create an Account")).Click();

            //fill Create an Account form 
            driver.FindElement(By.Id("firstname")).SendKeys("Name");
            driver.FindElement(By.Id("lastname")).SendKeys("LastName");
            driver.FindElement(By.Id("email_address")).SendKeys("YourCredential@yahoo.com");
            driver.FindElement(By.Id("password")).SendKeys("YourPassword");
            driver.FindElement(By.Id("password-confirmation")).SendKeys("YourPassword");

            //click on create account button
            driver.FindElement(By.XPath("//button[@title='Create an Account']")).Click();

            //identify if title page is the right one
            var newCustomerpageTitle = driver.FindElement(By.XPath("//span[contains(text(),'Create New Customer Account')]")).Text;
            //create the assertion to check if page is the correct one
            Assert.AreEqual("Create New Customer Account", newCustomerpageTitle);

            //locate the validation message for an already created account 
            var validationMessage = driver.FindElement(By.XPath("//div[@role='alert']/div")).Text;
            var expectedMessage = "There is already an account with this email address.";
            //we use this kind of assert when we want to check only some part of the entire text
            Assert.IsTrue(validationMessage.Contains(expectedMessage));
        }


        [TestCleanup]
        public void CloseBrowser()
        {
            driver.Quit();
        }
    }
}