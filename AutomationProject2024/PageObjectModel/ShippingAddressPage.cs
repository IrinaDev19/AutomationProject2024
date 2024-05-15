using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AutomationProject2024.ModelsBO;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AutomationProject2024.PageObjectModel
{
    public class ShippingAddressPage
    {
        private IWebDriver driver;

        public ShippingAddressPage(IWebDriver browser)
        {
            driver = browser;
        }

        public IWebElement EmailAddressInput => driver.FindElement(By.Id("customer-email"));
        public IWebElement FirstNameInput => driver.FindElement(By.XPath("//input[@name = 'firstname']"));
        public IWebElement LastNameInput => driver.FindElement(By.CssSelector("input[name = 'lastname']"));
        public IWebElement StreetAddressInput => driver.FindElement(By.CssSelector("input[name = 'street[0]']"));
        public IWebElement CityInput => driver.FindElement(By.CssSelector("input[name = 'city']"));
        public IWebElement StateDropdown => driver.FindElement(By.CssSelector("select[name = 'region_id']"));
        public IWebElement ZipCodeInput => driver.FindElement(By.CssSelector("input[name = 'postcode']"));
        public IWebElement TelephoneInput => driver.FindElement(By.CssSelector("input[name = 'telephone']"));
        public IList<IWebElement> ShippingMethodsOptions => driver.FindElements(By.CssSelector("input[type= 'radio']"));
        public IWebElement BtnNext => driver.FindElement(By.CssSelector("button[class= 'button action continue primary']"));

        IWebElement ShippmentPageTitle => driver.FindElement(By.XPath("//li[contains(@class, 'opc-progress-bar-item _active')]/span[text()='Shipping']"));
        // IWebElement ShippmentPageTitle => driver.FindElement(By.XPath("//div[contains(@data-bind,'Address')]"));
        public string getShippingPageTitle()
        {
            return ShippmentPageTitle.Text;
        }

        public PaymentMethodPage FillInShippingAddress(string email, string firstName, string lastName, string streetAddress, string city, string state, string zipcode, string telephone)
        {
            EmailAddressInput.SendKeys(email);
            Thread.Sleep(1000);
            FirstNameInput.SendKeys(firstName);
            LastNameInput.SendKeys(lastName);
            StreetAddressInput.SendKeys(streetAddress);
            CityInput.SendKeys(city);

            var selectState = new SelectElement(StateDropdown);
            selectState.SelectByText(state);

            ZipCodeInput.SendKeys(zipcode);
            TelephoneInput.SendKeys(telephone);

            ShippingMethodsOptions.FirstOrDefault().Click();

            BtnNext.Click();

            return new PaymentMethodPage(driver);
        }

        public PaymentMethodPage FillInShippingAddressSecondMethod(ShippingAddressModel shippingAddress)
        {
            EmailAddressInput.SendKeys(shippingAddress.Email);
            Thread.Sleep(4000);
            FirstNameInput.SendKeys(shippingAddress.FirstName);
            LastNameInput.SendKeys(shippingAddress.LastName);
            StreetAddressInput.SendKeys(shippingAddress.Street);
            CityInput.SendKeys(shippingAddress.City);

            var selectState = new SelectElement(StateDropdown);
            selectState.SelectByText(shippingAddress.State);

            ZipCodeInput.SendKeys(shippingAddress.PostalCode);
            TelephoneInput.SendKeys(shippingAddress.Phone);

            ShippingMethodsOptions.ElementAt(shippingAddress.ShippingMethod).Click();

            BtnNext.Click();

            return new PaymentMethodPage(driver);
        }
    }
}
    