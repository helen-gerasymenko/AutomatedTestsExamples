using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using Tests.Common.Extensions;

namespace Tests.Common.Pages.BackEnd
{
    public class NewRoleForm : BackendPageBase
    {
        public NewRoleForm(IWebDriver driver) : base(driver)
        {
        }

        public const string BrandManagerCreate = "root-brandmanager-create";

        public void SelectPermissions(string[] permissions)
        {

            _driver.FindElementWait(By.XPath("//button[contains(@data-bind, 'click: expandGrid')]"));

            foreach (var permission in permissions)
            {
                var elementXPath = $"//tr[td/@title='{permission}']//input";
                _driver.FindElementClick(elementXPath);
            }
        }

        public SubmittedRoleForm FillInRequiredFieldsAndSubmit(TestDataGenerator.RoleData data)
        {
            var roleCode = _driver.FindElementWait(By.XPath("//input[contains(@data-bind, 'value: Model.code')]"));
            roleCode.SendKeys(data.RoleCode);
            var roleName =
                _driver.FindElementWait(By.XPath("//div[@id='add-role-home']//input[contains(@data-bind, 'value: Model.name')]"));
            roleName.SendKeys(data.RoleName);
            var submitButton = _driver.FindElementWait(By.XPath("//div[@id='add-role-home']//button[text()='Save']"));
            submitButton.Click();
            var form = new SubmittedRoleForm(_driver);
            return form;
        }
    }
}
