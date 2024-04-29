### **Session 1 - 03.04.2024** 
We have discussed about automation:

What it is
Why it is important
Types of automation testing
The presentation was sent via email after the session.

### **Session 2 - 10.04.2024 - Let's write our first UI automation test**
**Scope**: This session scope was to create a unit test project, install all the dependencies and write a simple test.

How to create a unit test project:

1. Open Visual studio
2. Click File > New > Project
3. Search for unit test: Unit Test Project(.NET Framework)
4. Add project Name
5. Click "Create" button
At this moment, we have created a unit test project and should have a default test class: UnitTest1. The class has the following particularities:

1. It has [TestClass] annotation that identifies the class to be a test one. Without this annotation, the test under this class cannot be recognized and there for cannot run the tests within it.
2. The class contains a test method that has a [TestMethod] annotation. This help the method to be recognized as an test method and run it accordingly.
Find more info on: https://learn.microsoft.com/en-us/visualstudio/test/walkthrough-creating-and-running-unit-tests-for-managed-code?view=vs-2022

Let's import the needed packages in order to open a chrome browser using our test. Do a right click on the created project and click Manage Nuget Packages. On the opened window, select Browse tab and search for:

* **Selenium.Webdriver** 
* **Selenium.Webdriver.ChromeDriver**
* **Selenium.Support**
and install the latest version of each package in your project.

Be aware that if the local installed Chrome is not the same with the installed package, it will trigger a compatibility error when running the test.

**REMEMBER: THE TEST DOES WHAT YOU TELL IT TO DO.**
 
 That's why, selenium manipulates the elements in DOM as a human would do. For now, we will use click and sendkeys.

Our first automated test case will try to login into application. For this, we will need to write code for the next steps:

1. Open the browser
2. Maximize the browser window
3. Navigate to the application URL
4. Click Create an Account from header
5. Fill fields for create account
6. Click Create an account button
7. Assert that the redirection to page was successful and validation for existing user is displayed

   ```csharp  
    var driver = new ChromeDriver(); //open chrome browser
    driver.Manage().Window.Maximize(); //maximize browser window
    driver.Navigate().GoToUrl("OUR URL"); //access the SUT(System Under Test) url. In our case https://magento.softwaretestingboard.com/
    ```
Until now, we have covered the first 3 steps of our test. Let's complete our test:

 ```csharp
     //Click on consent button of cookies pop-up
            driver.FindElement(By.XPath("//button[@aria-label='Consent']")).Click();


            //Locate Create an Account link and click to go to the page 
            driver.FindElement(By.LinkText("Create an Account")).Click();

            //fill Create an Account form 
            driver.FindElement(By.Id("firstname")).SendKeys("Name");
            driver.FindElement(By.Id("lastname")).SendKeys("LastName");
            driver.FindElement(By.Id("email_address")).SendKeys("YourCredential@yahoo.com");
            driver.FindElement(By.Id("password")).SendKeys("YourPassword");
            driver.FindElement(By.Id("password-confirmation")).SendKeys("YourPassword");

            //click on create account button
            driver.FindElement(By.XPath("//button[@title='Create an Account']")).Click();

            //identify if title page is the right one
            var newCustomerpageTitle = driver.FindElement(By.XPath("//span[contains(text(),'Create New Customer Account')]")).Text;
    
            //create the assertion to check if page is the correct one
            Assert.AreEqual("Create New Customer Account", newCustomerpageTitle);

            //locate the validation message for an already created account 
            var validationMessage = driver.FindElement(By.XPath("//div[@role='alert']/div")).Text;
            var expectedMessage = "There is already an account with this email address.";
           
            //we use this kind of assert when we want to check only some part of the entire text
            Assert.IsTrue(validationMessage.Contains(expectedMessage));

            //close Chrome
            driver.Quit();
 ```
Clarification :)

WebElement represents a DOM element. WebElements can be found by searching from the document root using a WebDriver instance. WebDriver API provides built-in methods to find the WebElements which are based on different properties like ID, Name, Class, XPath, CSS Selectors, link Text, etc.
There are some ways of optimizing our selectors used to identify the elements in page. a. For the CssSelector, the simplest way is to use the following format: tagname[attribute='attributeValue']. b. For the XPath, the simplest way is to use the following format: //tagname[@attribute='attributeValue'].
Let's take for example the Email input:

    <input name="login[username]" value="" autocomplete="off" id="email" type="email" class="input-text" title="Email" data-validate="{required:true, 'validate-email':true}" aria-required="true" style="">

<input(this is the tagname)

type(this is the attribute)="email"(this is the value) → The CssSelector would be: input[type='email'] Or xpath → //input[@type= 'email']
name(this is the attribute)=" login[username]"(this is the value) → The XPath would be: //input[@name= 'login[username]'] or css selector → input[name= 'login[username]']
title (this is the attribute)=’Email’ → The CSS selector would be: input[title= 'Email'] or the xpath → //input[@title= 'Email']
class(this is the attribute)=" input-text "(this is the value) → The XPath would be: //input[@class='input-text'] or the css selector → input[class='input-text']
Session 3 - 10.04.2023 - Locators. TestInitialize. TestCleanup. Page Object Model
Scope: This session scope was to build reliable web element locators and to use MSTest methods to initialize/clean up our test data and to get rid of our duplicate code.

Xpath - Most flexible in order to build reliable web element locators.

Absolute XPath (direct way, select the element from the root node) /

Relative XPath (anywhere at the webpage) //

//input[@id="email"]

//*[@name="login[username]"]

//input[starts-with(@name, 'login')]

//input[contains(@name, 'login')]

If we don't find anything unique we can use:

AND

//input[contains(@name, 'login') and @type='email']
OR

//input[contains(@name, 'login') or @type='email']

Using Xpath we can also make use of the family of an element:
For example this element class='field email required ' has a big family:

Following - all following elements of the current node

//div[@class='field email required']//following::div
Child - all children elements of the current node

//div[@class='field email required']//child::div
Preceding - all nodes that come before the current node

//div[@class='field email required']//preceding::fieldset
Following-sibling - following siblings of the context node

//div[@class='field email required']//following-sibling::label
Parent - parent of the current node

//div[@class='field email required']//parent::fieldset
Descendant - descendants of the current node

//div[@class='field email required']//descendant::div
Try to use these elements in order if possible in order to consistently have good tests which will reduce brittleness and increase maintainability..

### **Session 3 - 17.04.2024**
One of a test case component is the prerequisite: conditions that must be met before the test case can be run. Our code is testing login scenarios and we need to see what are the prerequisites. We have identified the following steps that need to be execute before running the test:

    var driver = new ChromeDriver(); //open chrome browser
    driver.Manage().Window.Maximize(); //maximize browser window
    driver.Navigate().GoToUrl("OUR URL"); //access the SUT(System Under Test) url. In our case https://magento.softwaretestingboard.com/

Also, after the test has finished running, we need to clean up the operations that we made in our test in order to not impact further test. Remember, each test is independent and should not influence the result of other tests. In our test, the clean up would mean to close the browser.

    driver.Quit();
MSTest provides a way to declare methods to be called by the test runner before or after running a test.

    [TestInitialize]
    public void TestInitialize()
    {
    }

    [TestCleanup]
    public void TestCleanup()
    {
    }
The method decorated by [TestInitialize] is called before running each test of the class. The method decorated by [TestCleanup] is called after running each test of the class.

First, we need to remove the init/clean up steps and to move it the according method. Then, we have to organize the elements in such way, if the login page layout needs to be changed, also the maintenance of the tests should not be very time consuming.

A better approach to script maintenance is to create a separate class file which would find web elements, fill them or verify them. This class can be reused in all the scripts using that element. In future, if there is a change in the web element, we need to make the change in just 1 class file and not 10 different scripts.

This approach is called Page Object Model (POM). It helps make the code more readable, maintainable, and reusable.

Page Object model is an object design pattern in Selenium, where web pages are represented as classes, and the various elements on the page are defined as variables on the class. All possible user interactions can then be implemented as methods on the class.

Right click on the project > Add > Folder and name it PageObjects

Right click on the PageObjects folder > Add > New Item... > Add a class with name: LoginPage.cs

We need to add the objects that we use in our script in this class: email input, password input, sign in button and create a method to login the user.

Our LoginPage.cs will look like this in the end:

```csharp
public class LoginPage
    {
        IWebDriver driver;

        public LoginPage(IWebDriver browser) {
          driver = browser;
        }

        public IWebElement Email => driver.FindElement(By.Id("email"));

        public IWebElement Password => driver.FindElement(By.Id("pass"));

        public IWebElement SignInButton => driver.FindElement(By.Id("send2"));

        public IWebElement LoginPageTitle => driver.FindElement(By.CssSelector("h1.page-title"));

        public void SignInToAccount(string email, string password) {
            Email.SendKeys(email);
            Password.SendKeys(password);
            SignInButton.Click();
        }
    }
```

Now, we will continue working with the menu. It is present in all the app pages, and we need to create a single base class where the menu elements can be stored. This is a shared component and we need to call it in all of our page objects. Ww will create a class MenuItem.cs. This class will contain all menu elements.


 ```csharp
     public class MenuItem
     {
        private IWebDriver driver;

        public MenuItem(IWebDriver browser)
        {
            driver = browser;
        }
     }
 ```

The application has 2 contexts, but this menu cannot be used from both perspectives: logged out and logged in. Let's identify the elements used in these contexts:

logged out: Sign in, Create an Account
logged in: Sign out, My Account, My Wish List
For the moment we will create another class that will handle the context when a user is logged in: MenuItemBeforeSignIn that will inherit the MenuItem class.

```csharp
   public class MenuItemBeforeSignIn : MenuItem
    {
        IWebDriver driver;

        public MenuItemBeforeSignIn(IWebDriver browser): base(browser) 
        {
            driver = browser;
        }

        IWebElement SignInLink => driver.FindElement(By.LinkText("Sign In"));

            public LoginPage GoToSignInPage()
        {
            SignInLink.Click();
            return new LoginPage(driver);
        }
    }
```

We will add another PageObjectModel for ConsentCookie

```csharp
    public class ConsentCookiePage
    {
        IWebDriver driver;

        public ConsentCookiePage(IWebDriver browser)
        {
            driver = browser;
        }
        IWebElement ConsentButton => driver.FindElement(By.XPath("//button[@aria-label='Consent']"));
        
        public MenuItemBeforeSignIn ConsentCookies()
        {
            ConsentButton.Click();
            return new MenuItemBeforeSignIn(driver);
        }
      
    }
```

We will continue by making the changes in the LoginPage.cs and LoginTests.cs:

 ```csharp
    public class LoginPage
    {
        private IWebDriver driver;


        public LoginPage(IWebDriver browser)
        {
            driver = browser;
        }

        //...
        
        public string GetPageTitle() {
            return LoginPageTitle.Text;
        }
    }
```

The final look for TestClass will be:

```csharp
[TestClass]
    public class MagentoTests
    {
       ChromeDriver driver = new ChromeDriver();
        private ConsentCookiePage consentCookiePage;
        private MenuItemBeforeSignIn menuItemBeforeSignIn;
        private LoginPage loginPage;
       

        [TestInitialize]
        public void TestInitialize()
        {
            //access the browser
           
            ChromeOptions options = new ChromeOptions();
            //   options.AddArgument("--start-maximized");
            //   options.AddArgument("--ignore-certificate-errors");
            //   options.AddArgument("--disable-popup-blocking");
            options.AddArgument("--incognito");

            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://magento.softwaretestingboard.com/");
           
            consentCookiePage= new ConsentCookiePage(driver);
            consentCookiePage.ConsentCookies();
            menuItemBeforeSignIn = new MenuItemBeforeSignIn(driver);
        }

      [TestMethod]
        public void LoginValidAccount()
        {
            menuItemsBeforeSignIn.GoToLogin();

            loginPage.SignInAccount("email", "password");

            homePage = new HomePage(driver);

            //Wait for page to load
            Thread.Sleep(2000);

            Assert.IsTrue(homePage.GetWelcomeText().Contains("Welcome"));
        }


        [TestCleanup]
        public void TestCleanup()
        {
            driver.Quit();
        }
```
In the following we will create a resource page (Resource.resx) for used texts and for validation messages. To create this page go to AddNewItem and check for resources files type.
Add the following resources:
   Name  | Value
------------- | -------------
LoginPageTitle  |test.user@yahoo.com
Password	   | MagentoTests2024!  
StartPageTitle| Welcome
Username  |your username
WelcomeMessage| 	Welcome

Create a new resource file for validation (ValidationText.resx) containing:
  Name  | Value
------------- | -------------
UnknownText	| Text is not the same

Final test method will look like:

   ```csharp
       [TestMethod]
        public void LoginValidAccount()
        {
            menuItemsBeforeSignIn.GoToLogin();

            loginPage.SignInAccount(Resources.email, Resources.password);

            homePage = new HomePage(driver);

            //Wait for page to load
            Thread.Sleep(2000);

            Assert.IsTrue(homePage.GetWelcomeText().Contains(Resources.welcomeMessage), ValidationText.UnknownText);
        }
   ```

### **Session 4 - 27.03.2024**

Next we will move to the main scope of the session which is to write a test that will add a product to cart and place order while user is not logged in

In order to do this, we will need to write code for the next steps:

1. Open the browser
2. Maximize the window
3. Navigate to the application URL
4. Hover over the Watches option from menu and choose Gear option from the displayed list
5. Choose a product from the page 
6. Add the product to the cart
7. Go to the shopping cart
8. Choose 'Proceed to checkout'

In order to navigate through all the pages we will consider a base class HomePage.cs that will be the context of the user after he enters in the application:

```csharp
    public class HomePage
    {
        private IWebDriver driver;

        //reference the menu item control
        public MenuItemControl menuItemControl => new MenuItemControl(driver);

        public HomePage(IWebDriver browser)
        {
            driver = browser;
        }
    }
```

   After executing **Step 4** user is redirected to a page where all watches are displayed. For this we need to create another page object WatchesPage.cs:

```csharp
    public class WatchesPage
    {
        private IWebDriver driver;

        public WatchesPage(IWebDriver browser)
        {
            driver = browser;
        }
    }
```
Having the previous page object created we can update the MenuItemBeforeSingIn with the necessary elements declaration and method to hover over Gear option menu and click on Watches option from Step 4:
 
```csharp
    public class MenuItemBeforeSignIn : MenuItem
    {
        IWebDriver driver;

        //...

        IWebElement SignInLink => driver.FindElement(By.LinkText("Sign In"));

        public IWebElement MenuGearOption => driver.FindElement(By.XPath("//div[@id='store.menu']//span[text()='Gear']"));

        public IWebElement MenuWatchesOption => driver.FindElement(By.XPath("//div[@id='store.menu']//span[text()='Gear']/../following-sibling::ul[@role='menu']//span[text()='Watches']"));

        public WatchesPage NavigateToWatchesPage()
        {
            Thread.Sleep(2000);
            // hover on menu > gear element
            new Actions(driver).MoveToElement(MenuGearOption).Perform();
            MenuWatchesOption.Click();

            return new WatchesPage(driver);
        }

            public LoginPage GoToSignInPage()
          //  ...
    }
```

   After executing **Step 5** user is redirected to a page where details for the chosen watch are displayed. For this we need to create another page object WatchDetailsPage.cs:

```csharp
    public class ProductDetailsPage
    {
        private IWebDriver driver;

        public ProductDetailsPage(IWebDriver browser)
        {
            driver = browser;
        }
    }
```
Having the previous page object created we can update the WatchesPage.cs with the necessary elements declaration and method to click on the first watch from the list accordingly with Step 5:

```csharp
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
         
            return new ProductDetails(driver);
        }
    }
```
For checking if the correct details page is displayed we'll create a method in WhatchesPage to fetch product name

```csharp     
      public class WatchesPage
    {
        //...

        public ProductDetails GoToProductDetails(int indexProduct)
        {
        // ...
        }

        public string GetProductName(int index)
        {
            var list=productsList.Count();
           Thread.Sleep(2000);
           return productsList.ElementAt(index).FindElement(By.ClassName("product-item-link")).Text;
        }
    }
```
Now in the ProductDetails class we add a method to get Page title 

```csharp
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
```

 The test method will look like: 
    
```csharp
     [TestMethod]
        public void ShouldGoToProductDetails()
        {
            menuItemsBeforeSignIn.GoToWatchesPage();

            WatchesPage watchesPage = new WatchesPage(driver);

            Assert.IsTrue(watchesPage.GetPageTitle().Equals(Resources.watchesPageTitle),ValidationText.UnknownText); 

            var detailsPageTitle = watchesPage.GetProductName(0);//first product
            watchesPage.GoToProductDetails(0);

            ProductDetails productDetails = new ProductDetails(driver);

            Assert.IsTrue(productDetails.GetPageTitle().Equals(detailsPageTitle), ValidationText.UnknownText);
        }
```

Don't forget to add in  in resources a new string :
WatchesPageTitle	      Watches
  
Next, to complete **Step 7** we need to create the page object for the shopping cart page ShoppingCartPage.cs:

```csharp
    public class ShoppingCartPage
    {
        private IWebDriver driver;

        public ShoppingCartPage(IWebDriver browser)
        {
            driver = browser;
        }
    }
```
Having ShoppingCartPage.cs we can update ProductDetailsPage.cs with the button declaration and method to click on the shopping cart link:

```csharp

public class WatchDetailsPage
    {
        private IWebDriver driver;

        public WatchDetailsPage(IWebDriver browser)
        {
            driver = browser;
        }

        public IWebElement BtnAddToCart => driver.FindElement(By.Id("product-addtocart-button"));

        public WatchDetailsPage AddProductToCart()
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
```
  In order to complete **Step 8** we need to create the page object of the page we will be redirected after clicking on 'Proceed to checkout' button ShippingAddressPage.cs:

```csharp

public class ShippingAddressPage
     {
        private IWebDriver driver;

        public ShippingAddressPage(IWebDriver browser)
        {
            driver = browser;
        }

     }
```

Having the next page object we will be redirected after executing step 8, we can complete ShoppingCartPage.cs with the button declaration and method to click on the 'Proceed to checkout' element:

```csharp

  public class ShoppingCartPage
    {
        private IWebDriver driver;

        public ShoppingCartPage(IWebDriver browser)
        {
            driver = browser;
        }

        public IWebElement BtnProceedToCheckout => driver.FindElement(By.Id("top-cart-btn-checkout"));

        public ShippingAddressPage ProceedToCheckoutPage()
        {
         
            BtnProceedToCheckout.Click();

            return new ShippingAddressPage(driver);
        }
    }
```

The test method will look like:

```csharp

    [TestMethod]
        public void AddValidProductInCart()
        {
            menuItemsBeforeSignIn.GoToWatchesPage();
            WatchesPage watchesPage;
            watchesPage = new WatchesPage(driver);
            ProductDetailsPage watchDetailsPage = new ProductDetailsPage(driver);
            ShoppingCartPage shoppingCartPage;
            shoppingCartPage = new ShoppingCartPage(driver);

            var productName1 = watchesPage.GetProductName(1);
            watchesPage.GoToProductDetails(1);

            Assert.IsTrue(watchDetailsPage.GetPageTitle().Equals(productName1), "Is not equal");

            watchDetailsPage.AddProductToCart();

            watchDetailsPage.GoToShoppingCart();

            shoppingCartPage.ProceedToCheckoutPage();

            //remains to create the assert
        }
 ```
