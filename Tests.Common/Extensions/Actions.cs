using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using Tests.Common.Pages.BackEnd;

namespace Tests.Common.Extensions
{
    public static class Actions
    {
        public static DashboardPage DashboardPage;
        public static void Logout(this IWebDriver driver)
        {
            driver.Manage().Cookies.DeleteAllCookies();
            driver.Navigate().Refresh();
        }

        

        public static ViewUserForm CreateUser(this IWebDriver driver, TestDataGenerator.RoleData roleData, TestDataGenerator.AdminUserRegistrationData userData, string[] permissions)
        {
            // create a role
            var menu = new BackendMenuBar(driver);
            var roleManagerPage = menu.ClickRoleManagerMenuItem();
            var newRoleForm = roleManagerPage.OpenNewRoleForm();
            newRoleForm.SelectPermissions(permissions);
            var submittedForm = newRoleForm.FillInRequiredFieldsAndSubmit(roleData);

            // create a user
            var adminManagerPage = submittedForm.Menu.ClickAdminManagerMenuItem();
            var newUserForm = adminManagerPage.OpenNewUserForm();
            newUserForm.Submit(userData);

            var submittedUserForm = new ViewUserForm(driver);
            Assert.AreEqual("User has been successfully created", submittedUserForm.ConfirmationMessage);

            return submittedUserForm;
        }

        public static ViewUserForm CreateUserBasedOnPredefinedRole(this IWebDriver driver, TestDataGenerator.AdminUserRegistrationData userData)
        {
            var menu = new BackendMenuBar(driver);
            var adminManagerPage = menu.ClickAdminManagerMenuItem();
            var newUserForm = adminManagerPage.OpenNewUserForm();
            newUserForm.Submit(userData);
            return new ViewUserForm(driver);
        }
    }
}
