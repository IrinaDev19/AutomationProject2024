using OpenQA.Selenium;

namespace AutomationProject2024.PageObjectModel
{
    public class ShippingAddressPage
    {
        private IWebDriver driver;

        public ShippingAddressPage(IWebDriver browser)
        {
            driver = browser;
        }
    }
}
