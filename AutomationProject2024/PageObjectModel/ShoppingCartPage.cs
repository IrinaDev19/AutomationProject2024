using System.Threading;
using OpenQA.Selenium;

namespace AutomationProject2024.PageObjectModel
{
    public class ShoppingCartPage
    {
        private IWebDriver driver;

        public ShoppingCartPage(IWebDriver browser)
        {
            driver = browser;
        }

        public IWebElement BtnProceedToCheckout => driver.FindElement(By.XPath("//button[@title='Proceed to Checkout']"));

        public ShippingAddressPage ProceedToCheckoutPage()
        {
            Thread.Sleep(2000);
            BtnProceedToCheckout.Click();

            return new ShippingAddressPage(driver);
        }
    }
}
