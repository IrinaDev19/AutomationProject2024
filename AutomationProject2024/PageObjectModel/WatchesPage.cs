using System.Collections.Generic;
using System.Linq;
using System.Threading;
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

        public ProductDetailsPage GoToProductDetails(int indexProduct)
        {
            productsList.ElementAt(indexProduct).Click();
         
            return new ProductDetailsPage(driver);
        }

        public string GetProductName(int index)
        {
            var list=productsList.Count();
           Thread.Sleep(2000);
           return productsList.ElementAt(index).FindElement(By.ClassName("product-item-link")).Text;
        }
    }
}
