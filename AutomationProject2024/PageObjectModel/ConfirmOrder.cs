using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace AutomationProject2024.PageObjectModel
{
    public class ConfirmOrder
    {
        public IWebDriver driver;

        public ConfirmOrder(IWebDriver browser)
        {
            driver = browser;
        }

        public IWebElement txtConfirmationOrder => driver.FindElement(By.XPath("//h1[@class='page-title']/span"));

        public string getConfirmOrderPageTitle()
        {
            return txtConfirmationOrder.Text;
        }
    }
}
