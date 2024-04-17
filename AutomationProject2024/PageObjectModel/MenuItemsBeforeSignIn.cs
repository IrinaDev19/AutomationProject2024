using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

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

        public LoginPage GoToLogin()
        {
            linkSign.Click();

            return new LoginPage(driver);
        }
    }
}
