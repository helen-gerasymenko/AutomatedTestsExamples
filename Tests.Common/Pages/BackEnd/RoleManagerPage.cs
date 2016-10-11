using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using Tests.Common.Extensions;

namespace Tests.Common.Pages.BackEnd
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
