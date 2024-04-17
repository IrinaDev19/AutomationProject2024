using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace AutomationProject2024.PageObjectModel
{
    public class HomePage
    {
        private IWebDriver driver;

        public HomePage(IWebDriver browser)
        {
            driver = browser;
        }

        IWebElement welcomeText => driver.FindElement(By.XPath("//li[@class='greet welcome']/span"));

        public string GetWelcomeText()
        {
            return welcomeText.Text;
        }
    }
}
