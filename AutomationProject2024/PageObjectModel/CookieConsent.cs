using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace AutomationProject2024.PageObjectModel
{
    public class CookieConsent
    {
        private IWebDriver driver;

        public CookieConsent(IWebDriver browser)
        {
            driver = browser;
        }

        public IWebElement btnConsent => driver.FindElement(By.XPath("//button[@aria-label='Consent']"));

        public MenuItemsBeforeSignIn GoToMenuAfterCookieAccept()
        {
            btnConsent.Click();

            return new MenuItemsBeforeSignIn(driver);
        }
    }
}
