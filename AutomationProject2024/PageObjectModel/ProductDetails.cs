using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace AutomationProject2024.PageObjectModel
{
    public class ProductDetails
    {

        IWebDriver driver;

        public ProductDetails(IWebDriver browser)
        {
            driver = browser;
        }

        IWebElement pageTitle => driver.FindElement(By.XPath("//h1[@class='page-title']/span"));

        public string GetPageTitle()
        {
            return pageTitle.Text;
        }

    }
}
