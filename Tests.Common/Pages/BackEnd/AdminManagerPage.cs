using OpenQA.Selenium;
using Tests.Common.Extensions;

namespace Tests.Common.Pages.BackEnd
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
    }
}