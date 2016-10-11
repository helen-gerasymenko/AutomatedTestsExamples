using OpenQA.Selenium;
using Tests.Common.Extensions;
using Tests.Common.Pages.BackEnd;

namespace Tests.Common.Pages.BackEnd
{
    public class ViewUserForm : BackendPageBase
    {
        public ViewUserForm(IWebDriver driver) : base(driver)
        {
        }

        public string ConfirmationMessage => _driver.FindElementValue(By.XPath("//div[contains(@data-bind, 'visible: message')]"));
    }
}