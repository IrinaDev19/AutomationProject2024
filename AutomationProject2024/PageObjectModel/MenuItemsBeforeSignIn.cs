using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace AutomationProject2024.PageObjectModel
{
    public class MenuItemsBeforeSignIn : MenuItems
    {
        private IWebDriver driver;

        public MenuItemsBeforeSignIn(IWebDriver browser): base(browser)
        {
            driver = browser;
        }

        public IWebElement linkSign => driver.FindElement(By.LinkText("Sign In"));

        public IWebElement gearOption => driver.FindElement(By.XPath("//div[@id='store.menu']//span[text()='Gear']"));

        public IWebElement watchesPageOption => driver.FindElement(By.XPath("//ul[@role='menu']//span[text()='Watches']"));
     
        public WatchesPage GoToWatchesPage()
        {
            Thread.Sleep(2000);
            new Actions(driver).MoveToElement(gearOption).Perform();

            watchesPageOption.Click();

            return new WatchesPage(driver);
        }

        public LoginPage GoToLogin()
        {
            linkSign.Click();

            return new LoginPage(driver);
        }
    }
}
