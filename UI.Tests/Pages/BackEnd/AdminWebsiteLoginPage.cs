using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace UI.Tests.Pages.BackEnd
{
    public class AdminWebsiteLoginPage : BackendPageBase
    {
        public AdminWebsiteLoginPage(IWebDriver driver) : base(driver){}

        public DashboardPage Login(string username, string password)
        {
            ClearFields();
            _usernameField.SendKeys(username);
            _passwordField.SendKeys(password);
            _loginButton.Click();

            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(45));
            var welcomeTitle = By.XPath("//small[text()='Welcome,']");
            wait.Until(ExpectedConditions.ElementIsVisible(welcomeTitle));
            var page = new DashboardPage(_driver);
            page.Initialize();
            return page;
        }

        public void ClearFields()
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(45));
            wait.Until(d => _usernameField.Displayed);
            _usernameField.Clear();
            _passwordField.Clear();
        }

#pragma warning disable 649
        [FindsBy(How = How.Id, Using = "username")]
        private IWebElement _usernameField;

        [FindsBy(How = How.Id, Using = "password")]
        private IWebElement _passwordField;

        [FindsBy(How = How.XPath, Using = "//button[contains(., 'Login')]")]
        private IWebElement _loginButton;
#pragma warning restore 649  
    }
}