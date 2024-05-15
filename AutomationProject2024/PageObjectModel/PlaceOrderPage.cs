using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace AutomationProject2024.PageObjectModel
{
    public class PlaceOrderPage
    {
        private IWebDriver driver;

        public PlaceOrderPage(IWebDriver browser)
        {
            driver = browser;
        }

        public IWebElement PageTitle => driver.FindElement(By.XPath("//h1[@class='page-title']/span"));
    
        public string getPageTitle()
        {
            return PageTitle.Text;
        }
    }
}