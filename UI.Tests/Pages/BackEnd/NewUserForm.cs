using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Ui.Tests;
using UI.Tests.Extensions;

namespace UI.Tests.Pages.BackEnd
{
    public class NewUserForm : BackendPageBase
    {
        public NewUserForm(IWebDriver driver) : base(driver)
        {
        }

        public ViewUserForm Submit(TestDataGenerator.AdminUserRegistrationData data)
        {
            var userName = _driver.FindElementWait(By.XPath("//input[contains(@data-bind, 'value: Model.username')]"));
            userName.SendKeys(data.UserName);

            var password = _driver.FindElementWait(By.XPath("//input[@data-bind='value: Model.password']"));
            password.SendKeys(data.Password);

            var retypePassword = _driver.FindElementWait(By.XPath("//input[@data-bind='value: Model.passwordConfirmation']"));
            retypePassword.SendKeys(data.Password);

            var rolesList = _driver.FindElementWait(By.XPath("//select[contains(@data-bind, 'options: Model.roles')]"));
            var roleField = new SelectElement(rolesList);
            roleField.SelectByText(data.Role);

            var submitButton = _driver.FindElementWait(By.XPath("//button[text()='Save']"));
            submitButton.Click();
            var form = new ViewUserForm(_driver);

            return form;
        }
    }
}