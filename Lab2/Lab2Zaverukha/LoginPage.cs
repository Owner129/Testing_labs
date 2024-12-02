using OpenQA.Selenium;

namespace SaucedemoTests.PageObjects
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
        }

        private IWebElement UsernameField => _driver.FindElement(By.Id("user-name"));
        private IWebElement PasswordField => _driver.FindElement(By.Id("password"));
        private IWebElement LoginButton => _driver.FindElement(By.Id("login-button"));

        public void EnterUsername(string username) => UsernameField.SendKeys(username);
        public void EnterPassword(string password) => PasswordField.SendKeys(password);
        public void ClickLogin() => LoginButton.Click();
    }
}
