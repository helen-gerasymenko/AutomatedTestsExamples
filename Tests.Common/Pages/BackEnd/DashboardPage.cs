using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using Tests.Common.Extensions;

namespace Tests.Common.Pages.BackEnd
{
    public class DashboardPage : BackendPageBase
    {
        public DashboardPage(IWebDriver driver) : base(driver) { }
        public string Username => _driver.FindElementValue(By.XPath("//span[@data-bind='text: security.userName']"));
    }
}
