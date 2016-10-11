using NUnit.Framework;
using OpenQA.Selenium;
using Ui.Tests;
using UI.Tests.Extensions;

namespace UI.Tests.Pages.BackEnd
{
    public class AdminManagerPage : BackendPageBase
    {
        public AdminManagerPage(IWebDriver driver) : base(driver){}

        public NewUserForm OpenNewUserForm()
        {
            var newUserButton = _driver.FindElementWait(By.XPath("//btn[text='newUser']"));
            newUserButton.Click();
            var form = new NewUserForm(_driver);
            return form;
        }

        public ViewUserForm CreateUser(TestDataGenerator.RoleData roleData, TestDataGenerator.AdminUserRegistrationData userData, string[] permissions)
        {
            // create a role
            var menu = new BackendMenuBar(_driver);
            var roleManagerPage = menu.ClickRoleManagerMenuItem();
            var newRoleForm = roleManagerPage.OpenNewRoleForm();
            newRoleForm.SelectPermissions(permissions);
            var submittedForm = newRoleForm.FillInRequiredFieldsAndSubmit(roleData);

            // create a user
            var adminManagerPage = submittedForm.Menu.ClickAdminManagerMenuItem();
            var newUserForm = adminManagerPage.OpenNewUserForm();
            newUserForm.Submit(userData);

            var submittedUserForm = new ViewUserForm(_driver);
            Assert.AreEqual("User has been successfully created", submittedUserForm.ConfirmationMessage);

            return submittedUserForm;
        }

        public ViewUserForm CreateUserBasedOnPredefinedRole(TestDataGenerator.AdminUserRegistrationData userData)
        {
            var menu = new BackendMenuBar(_driver);
            var adminManagerPage = menu.ClickAdminManagerMenuItem();
            var newUserForm = adminManagerPage.OpenNewUserForm();
            newUserForm.Submit(userData);
            return new ViewUserForm(_driver);
        }
    }
}