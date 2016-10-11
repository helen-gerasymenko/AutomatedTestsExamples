using OpenQA.Selenium;
using UI.Tests.Extensions;

namespace UI.Tests.Pages.BackEnd
{
    public class DashboardPage : BackendPageBase
    {
        public DashboardPage(IWebDriver driver) : base(driver) { }
        public string Username => _driver.FindElementValue(By.XPath("//span[@data-bind='text: security.userName']"));
    }
}
