using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SaucedemoTests.Steps
{
    [Binding]
    public class LoginAndAddToCartSteps
    {
        private IWebDriver _driver;
        private LoginPage _loginPage;
        private InventoryPage _inventoryPage;

        public void Setup()
        {
            // Запускаємо ChromeDriver у звичайному режимі
            _driver = new ChromeDriver();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            _driver.Manage().Window.Maximize(); // Максимізація вікна браузера
            _loginPage = new LoginPage(_driver);
            _inventoryPage = new InventoryPage(_driver);
        }

        public void Teardown()
        {
            _driver.Quit();
        }

        [Given(@"I am on the login page")]
        public void GivenIAmOnTheLoginPage()
        {
            _driver.Navigate().GoToUrl("https://www.saucedemo.com/");
        }

        [When(@"I login with username ""(.*)"" and password ""(.*)""")]
        public void WhenILoginWithUsernameAndPassword(string username, string password)
        {
            _loginPage.EnterUsername(username);
            _loginPage.EnterPassword(password);
            _loginPage.ClickLogin();
        }

        [When(@"I add the first product to the cart")]
        public void WhenIAddTheFirstProductToTheCart()
        {
            _inventoryPage.AddFirstItemToCart();
        }

        [Then(@"the cart badge should display ""(.*)""")]
        public void ThenTheCartBadgeShouldDisplay(string expectedBadgeText)
        {
            Assert.IsTrue(_inventoryPage.IsCartBadgeDisplayed(), "Cart badge is not displayed.");
            Assert.AreEqual(expectedBadgeText, _inventoryPage.GetCartBadgeText(), "Cart badge does not display correct quantity.");
        }
    }
}
