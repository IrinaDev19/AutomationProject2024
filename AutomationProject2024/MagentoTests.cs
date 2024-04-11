using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
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
        [TestMethod]
        public void Should_RedirectToAnAlreadyCreatedAccountPage_When_TheAccountWasCreatedBefore()
        {
            //access the browser
            ChromeDriver driver= new ChromeDriver();

            ChromeOptions options= new ChromeOptions();
         //   options.AddArgument("--start-maximized");
         //   options.AddArgument("--ignore-certificate-errors");
         //   options.AddArgument("--disable-popup-blocking");
            options.AddArgument("--incognito");

            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://magento.softwaretestingboard.com/");

            //Click on consent button of cookies pop-up
            driver.FindElement(By.XPath("//button[@aria-label='Consent']")).Click();

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

            //close Chrome
            driver.Quit();
        }


        //try to create the following test based on the one above

        [TestMethod]
        public void Should_Login_When_CredentialsAreValid()
        {
            
        }
    }
}


//NOTE: to see the test suite go to View-> Test Explorer->Click and then lock the new window