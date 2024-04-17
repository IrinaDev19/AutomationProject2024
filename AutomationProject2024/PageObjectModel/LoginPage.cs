using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProject2024.PageObjectModel
{
    public class LoginPage
    {
        private IWebDriver driver;

        public LoginPage(IWebDriver browser)
        {
            driver = browser;
        }

        
        public IWebElement txtEmail => driver.FindElement(By.Id("email"));
        public IWebElement txtPassword => driver.FindElement(By.Id("pass"));
        public IWebElement btnSignIn => driver.FindElement(By.Id("send2"));

        public void SignInAccount(string email, string password)
        {
            txtEmail.SendKeys(email);
            txtPassword.SendKeys(password);
            btnSignIn.Click();
        }

    }
}