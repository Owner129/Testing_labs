using OpenQA.Selenium;

namespace SaucedemoTests.PageObjects
{
    public class InventoryPage
    {
        private readonly IWebDriver _driver;

        public InventoryPage(IWebDriver driver)
        {
            _driver = driver;
        }

        private IWebElement FirstAddToCartButton => _driver.FindElement(By.ClassName("btn_inventory"));
        private IWebElement CartBadge => _driver.FindElement(By.ClassName("shopping_cart_badge"));

        public void AddFirstItemToCart() => FirstAddToCartButton.Click();
        public bool IsCartBadgeDisplayed() => CartBadge.Displayed;
        public string GetCartBadgeText() => CartBadge.Text;
    }
}
