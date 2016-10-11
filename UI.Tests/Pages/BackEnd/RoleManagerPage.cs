using OpenQA.Selenium;
using UI.Tests.Extensions;

namespace UI.Tests.Pages.BackEnd
{
    public class RoleManagerPage : BackendPageBase
    {
        public RoleManagerPage(IWebDriver driver) : base(driver){}

        public NewRoleForm OpenNewRoleForm()
        {
            var newRoleButton = _driver.FindElementWait(By.Id("newActionBtn"));
            newRoleButton.Click();
            var form = new NewRoleForm(_driver);
            return form;
        }
    }
}
