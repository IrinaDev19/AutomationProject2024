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
        private ChromeDriver driver;
        private CookieConsent cookieConsent;
        private MenuItemsBeforeSignIn menuItemsBeforeSignIn;
        private LoginPage loginPage;
        private HomePage homePage;

        [TestInitialize]
        public void Setup()
        {
            ChromeOptions options = new ChromeOptions();
            driver= new ChromeDriver(options);
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://magento.softwaretestingboard.com/");
            
            cookieConsent = new CookieConsent(driver);
            loginPage = new LoginPage(driver);
            menuItemsBeforeSignIn = new MenuItemsBeforeSignIn(driver);
    
            cookieConsent.GoToMenuAfterCookieAccept();
        }

        [TestMethod]
        public void LoginValidAccount()
        {
            menuItemsBeforeSignIn.GoToLogin();

            loginPage.SignInAccount(Resources.email, Resources.password);

            homePage = new HomePage(driver);

            //Wait for page to load
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
            driver.FindElement(By.Id("firstname")).SendKeys("User");
            driver.FindElement(By.Id("lastname")).SendKeys("Test");
            driver.FindElement(By.Id("email_address")).SendKeys(Resources.email);
            driver.FindElement(By.Id("password")).SendKeys(Resources.password);
            driver.FindElement(By.Id("password-confirmation")).SendKeys(Resources.password);

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

        [TestMethod]
        public void ShouldGoToProductDetails()
        {
            menuItemsBeforeSignIn.GoToWatchesPage();

            WatchesPage watchesPage = new WatchesPage(driver);

            Assert.IsTrue(watchesPage.GetPageTitle().Equals(Resources.watchesPageTitle),ValidationText.UnknownText);
           
            var detailsPageTitle = watchesPage.GetProductName(0);
            watchesPage.GoToProductDetails(0);
            ProductDetailsPage productDetails = new ProductDetailsPage(driver);

            Assert.IsTrue(productDetails.GetPageTitle().Equals(detailsPageTitle), ValidationText.UnknownText);
        }


        [TestMethod]
        public void AddValidProductInCart()
        {
            menuItemsBeforeSignIn.GoToWatchesPage();
            WatchesPage watchesPage;
            watchesPage = new WatchesPage(driver);
            ProductDetailsPage watchDetailsPage = new ProductDetailsPage(driver);
            ShoppingCartPage shoppingCartPage;
            shoppingCartPage = new ShoppingCartPage(driver);

            var productName1 = watchesPage.GetProductName(1);
            watchesPage.GoToProductDetails(1);

            Assert.IsTrue(watchDetailsPage.GetPageTitle().Equals(productName1), "Is not equal");

            watchDetailsPage.AddProductToCart();

            watchDetailsPage.GoToShoppingCart();

            shoppingCartPage.ProceedToCheckoutPage();
        }

        [TestCleanup]
        public void CloseBrowser()
        {
            driver.Quit();
        }
    }
}