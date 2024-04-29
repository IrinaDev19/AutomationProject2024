using System.Threading;
using OpenQA.Selenium;

namespace AutomationProject2024.PageObjectModel
{
    public class ProductDetailsPage
    {

        IWebDriver driver;

        public ProductDetailsPage(IWebDriver browser)
        {
            driver = browser;
        }

        IWebElement pageTitle => driver.FindElement(By.XPath("//h1[@class='page-title']/span"));

        public string GetPageTitle()
        {
            return pageTitle.Text;
        }

        public IWebElement BtnAddToCart => driver.FindElement(By.Id("product-addtocart-button"));

        public ProductDetailsPage AddProductToCart()
        {
            BtnAddToCart.Click();

            return this;
        }

        public IWebElement ShoppingCartLink => driver.FindElement(By.LinkText("shopping cart"));

        public ShoppingCartPage GoToShoppingCart()
        {
            Thread.Sleep(2000);
            ShoppingCartLink.Click();

            return new ShoppingCartPage(driver);
        }

    }
}
