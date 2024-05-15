using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace AutomationProject2024.PageObjectModel
{
    public class PaymentMethodPage
    {
        private IWebDriver driver;


        public PaymentMethodPage(IWebDriver browser)
        {
            driver = browser;
        }
        public IWebElement BtnPlaceOrder => driver.FindElement(By.CssSelector("button[title='Place Order']"));

        IWebElement PaymentMethodPageTitle => driver.FindElement(By.XPath("//div[contains(@data-bind,'getGroupTitle')]"));
        public string getPaymentMethodPageTitle()
        {
            return PaymentMethodPageTitle.Text;
        }

        public PlaceOrderPage PlaceOrder()
        {
            Thread.Sleep(5000);
            BtnPlaceOrder.Click();

            return new PlaceOrderPage(driver);
        }
    }
}
