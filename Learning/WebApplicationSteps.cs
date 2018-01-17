using System;
using System.IO;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;

namespace Learning
{
    [Binding]
    public class WebApplicationSteps
    {
        private IWebDriver _driver;

        private By DeliveryOrCarryOut(string orderType)
        {
            return By.CssSelector($".form__input--icon.Delivery.js-delivery.c-delivery");
        }

        private By AddressType => By.Id("Address_Type_Select");
        private By StreetAddress => By.Id("Street");
        private By AptDetails => By.Id("Address_Line_2");
        private By City => By.Id("City");
        private By State => By.Id("State");
        private By ZipCode => By.Id("Postal_Code");
        private By ContinueButton=> By.CssSelector("button[type='submit']");
        private By ApartmentName => By.Id("Location_Name");



        [Given(@"I am on the dominos home screen")]
        public void GivenIAmOnTheDominosHomeScreen()
        {
            _driver = new FirefoxDriver();
            _driver.Manage().Window.Maximize();
            _driver.Manage().Cookies.DeleteAllCookies();
            
            _driver.Navigate().GoToUrl("https://www.dominos.com/en/");
        }
        
        [Given(@"I select a Pizza from Menu")]
        public void GivenISelectAPizzaFromMenu()
        {
            IWebElement menuElement = _driver.FindElement(By.CssSelector(".qa-Cl_Menu.c-site-nav-main-link-1"));
            menuElement.Click();
        }
        
        private IWebElement WaitForElementDisplayed(By element)
        {
            WebDriverWait wait1 = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            wait1.Until((d) =>
                d.FindElement(element).Displayed);
            IWebElement webElement = _driver.FindElement(element);
            return webElement;
        }

        [Given(@"I select ""(.*)""")]
        public void GivenISelect(string p0)
        {
            string selector = string.Empty;
            switch (p0)
            {
                case "Menu":
                   selector = ".qa-Cl_Menu.c-site-nav-main-link-1";
                    break;
                case "Order Online":
                    selector = "data-quid=['main-nav-link-0']";
                    break;

            }

            IWebElement menuElement = WaitForElementDisplayed(By.CssSelector(selector));
            menuElement.Click();
        }

        [Given(@"I select a ""(.*)"" from Menu")]
        public void GivenISelectAfromMenu(string p0)
        {
            string menuElement = $"[data-quid = 'entree-title-{p0}']";
            IWebElement menuIWebElement = WaitForElementDisplayed(By.CssSelector(menuElement));
            menuIWebElement.Click();
        }

        [Given(@"I Select item ""(.*)"" pizza")]
        public void GivenISelectItem(string p0)
        {
            string pizzaElement = $"//a[contains(text(),'{p0}')]/../preceding-sibling::a";
            IWebElement pizzaWebElement = WaitForElementDisplayed(By.XPath(pizzaElement));
            pizzaWebElement.Click();
        }

        [When(@"I click on ""(.*)""")]
        public void WhenIClickOn(string p0)
        {
            WaitForElementDisplayed(DeliveryOrCarryOut(p0)).Click();
        }

        [Then(@"I should see the AddressType")]
        public void ThenIShouldSeeTheAddressType()
        {
            WaitForElementDisplayed(AddressType);
            Assert.IsTrue(_driver.FindElement(AddressType).Displayed);
        }

        [Then(@"Select apartment type as ""(.*)""")]
        public void ThenSelectApartmentTypeAs(string p0)
        {
            IWebElement apartmentTypElement = WaitForElementDisplayed(AddressType);
            var options = new SelectElement(apartmentTypElement);
            options.SelectByText(p0);
        }

        [Then(@"enter street address as ""(.*)""")]
        public void ThenEnterStreetAddressAs(string p0)
        {
            WaitForElementDisplayed(StreetAddress).SendKeys(p0);
            //Assert.AreEqual(p0, WaitForElementDisplayed(StreetAddress).Text);
        }

        [Then(@"enter suite number as ""(.*)""")]
        public void ThenEnterSuiteNumberAs(string p0)
        {
           IWebElement appWebElement = WaitForElementDisplayed(AptDetails);
            appWebElement.SendKeys(p0);
            appWebElement.SendKeys(Keys.Tab);
            Thread.Sleep(100);
            Assert.AreEqual(p0, appWebElement.Text);
        }

        [Then(@"enter city as ""(.*)""")]
        public void ThenEnterCityAs(string p0)
        {
            WaitForElementDisplayed(City).SendKeys(p0);
            //Assert.AreEqual(p0, WaitForElementDisplayed(City).Text);
        }

        #region 
         
        #endregion



        [AfterScenario]
        public void DisposeWebDriver()
        {
            const string folderPath = @"C:\kalyan\Udemy\Pluralsight\Specflow\TestResults\";
            if (ScenarioContext.Current.TestError != null)
            {
                ITakesScreenshot screenShot;
                using ((screenShot = _driver as ITakesScreenshot) as IDisposable)
                {
                    if (screenShot != null)
                    {
                        string fileName = DateTime.Now.ToString("MM-dd-yyyy-hhmmss");
                        var testScreenShot = screenShot.GetScreenshot();
                        testScreenShot.SaveAsFile(folderPath+fileName, ScreenshotImageFormat.Bmp);
                    }
                }
            }
            _driver?.Quit();
        }
    }
}
