using OpenQA.Selenium;
using UI.Tests.Extensions;

namespace UI.Tests.Pages.BackEnd
{
    public class ViewUserForm : BackendPageBase
    {
        public ViewUserForm(IWebDriver driver) : base(driver)
        {
        }

        public string ConfirmationMessage => _driver.FindElementValue(By.XPath("//div[contains(@data-bind, 'visible: message')]"));
    }
}