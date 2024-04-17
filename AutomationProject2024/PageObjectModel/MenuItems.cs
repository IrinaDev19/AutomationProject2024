using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace AutomationProject2024.PageObjectModel
{
    public class MenuItems
    {
       private IWebDriver driver;

        public MenuItems(IWebDriver browser) { 
            driver = browser;
        }

    }
}
