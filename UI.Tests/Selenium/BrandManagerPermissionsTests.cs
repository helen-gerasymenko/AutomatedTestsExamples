using System.Linq;
using System.Threading;
using NUnit.Framework;
using Ui.Tests;
using UI.Tests.Base;
using UI.Tests.Extensions;
using UI.Tests.Pages.BackEnd;

namespace UI.Tests.Selenium
{
    public class BrandManagerPermissionsTests : SeleniumBase
    {
        private DashboardPage _dashboardPage;
        private const string DefaultLicensee = "XXX";
        private const string DefaultBrand = "YYY";

        public override void BeforeEach()
        {
            base.BeforeEach();
            Logout();
            _dashboardPage = LoginToAdminWebsiteAsSuperAdmin();
        }

        [Test, CategorySmoke]
        public void Can_manage_brand_with_permissions()
        {
            //create a brand
            var brandPage = _dashboardPage.Menu.ClickBrandManagerItem();
            var newBrandPage = brandPage.OpenNewBrandForm();
            var brandName = TestDataGenerator.GetRandomAlphabeticString(5);
            var brandCode = TestDataGenerator.GetRandomAlphabeticString(5);
            var playerPrefix = TestDataGenerator.GetRandomAlphabeticString(3);
            var submittedBrandForm = newBrandPage.Submit(brandName, brandCode, playerPrefix, type: "Credit");

            Assert.AreEqual("The brand has been successfully created.", submittedBrandForm.ConfirmationMessage);

            //create a user for the new brand
            var userData = TestDataGenerator.CreateValidAdminUserRegistrationData(
                role: "Licensee",
                licensee: DefaultLicensee,
                brand: brandName,
                status: "Active",
                currency: "ALL");

            var adminManagerPage = new AdminManagerPage(_driver);
            adminManagerPage.CreateUserBasedOnPredefinedRole(userData);

            //log in as the new user and verify management button states
            LoginToAdminWebsiteAs(userData.UserName, userData.Password);
            brandPage = _dashboardPage.Menu.ClickBrandManagerItem();
            var newButton = brandPage.FindButton(BrandManagerPage.NewButton);
            var editButton = brandPage.FindButton(BrandManagerPage.EditButton);
            var activateButton = brandPage.FindButton(BrandManagerPage.ActivateButton);

            Assert.That(newButton.Displayed);
            Assert.That(editButton.Displayed);
            Assert.That(activateButton.Displayed);

            //view brand
            LoginToAdminWebsiteAs(userData.UserName, userData.Password);
            brandPage = _dashboardPage.Menu.ClickBrandManagerItem();
            var viewBrandForm = brandPage.OpenViewBrandForm(brandName);

            Assert.AreEqual(brandName, viewBrandForm.BrandNameValue);

            //edit brand
            var editBrandPage = brandPage.OpenEditBrandForm(brandName);
            brandName += "edited";
            var submittedEditBrandForm = editBrandPage.EditOnlyRequiredData(
                brandType: "Deposit",
                brandName: brandName,
                brandCode: TestDataGenerator.GetRandomAlphabeticString(5));

            Assert.AreEqual("The brand has been successfully updated.", submittedEditBrandForm.ConfirmationMessage);
        }

        [Test]
        public void Cannot_view_brand_manager_without_permissions()
        {
            //create user/role data
            var roleData = TestDataGenerator.CreateValidRoleData(
                code: "XXX", 
                name: "YYY", 
                licensee: DefaultLicensee);

            var userData = TestDataGenerator.CreateValidAdminUserRegistrationData(
                roleData.RoleName, 
                roleData.Licensee, 
                DefaultBrand,
                status: "Active",
                currency: "ALL");

            var adminManagerPage = new AdminManagerPage(_driver);
            adminManagerPage.CreateUser(roleData, userData, new[] { NewRoleForm.BrandManagerCreate });

            //log in as the user
            _dashboardPage = LoginToAdminWebsiteAs(userData.UserName, userData.Password);
            var brandManagerMenuItemVisible = _dashboardPage.Menu.CheckIfMenuItemDisplayed(BackendMenuBar.BrandManager);

            Assert.IsFalse(brandManagerMenuItemVisible);
        }

        [Test]
        public void Can_view_brand_manager_with_permissions()
        {
            //create a user based on role with view permissions
            var brandName = TestDataGenerator.GetRandomString(7);
            var userData = TestDataGenerator.CreateValidAdminUserRegistrationData(
                role: "SuperAdmin", 
                licensee: DefaultLicensee, 
                brand: brandName,
                status: "Active",
                currency: "ALL");

            var adminManagerPage = new AdminManagerPage(_driver);
            adminManagerPage.CreateUserBasedOnPredefinedRole(userData);

            //log in as the user
            _dashboardPage = LoginToAdminWebsiteAs(userData.UserName, userData.Password);
            _dashboardPage.Menu.ClickBrandManagerItem();
            var brandManagerMenuItemVisible = _dashboardPage.Menu.CheckIfMenuItemDisplayed(BackendMenuBar.BrandManager); 

            Assert.IsTrue(brandManagerMenuItemVisible);
        }

       

        protected override string GetWebsiteUrl()
        {
            return "http://www.adminwebsite.com";
        }
    }
}