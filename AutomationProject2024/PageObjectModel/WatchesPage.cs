using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace AutomationProject2024.PageObjectModel
{
    public class WatchesPage
    {
        IWebDriver driver;

        public WatchesPage(IWebDriver browser)
        {
            driver = browser;
        }


        IWebElement pageTitle => driver.FindElement(By.Id("page-title-heading"));

        IList<IWebElement> productsList => driver.FindElements(By.XPath("//li[contains(@class, 'product-item')]"));

        public string GetPageTitle()
        {
            return pageTitle.Text;
        }

        public ProductDetails GoToProductDetails(int indexProduct)
        {
            productsList.ElementAt(indexProduct).Click();
         
          //  productsList.First().Click();

          //  productsList.Last().Click();
            return new ProductDetails(driver);
        }

        public string GetProductName(int index)
        {
           return productsList.ElementAt(index).FindElement(By.ClassName("product-item-link")).Text;
        }
    }
}
