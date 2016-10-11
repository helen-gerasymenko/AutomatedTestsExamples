using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using UI.Tests.Extensions;

namespace UI.Tests.Pages.BackEnd
{
    public class ViewBrandForm : BackendPageBase
    {
        public ViewBrandForm(IWebDriver driver) : base(driver){}

        public string ConfirmationMessage
        {
            get
            {
                var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
                var message = _driver.FindElementWait(By.XPath("//div[@class[contains(., 'alert alert-success')]]"));
                wait.Until(x => message.Displayed);
                return message.Text;
            }
        }

        public string BrandNameValue => _driver.FindElementValue(By.XPath("//*[contains(@data-bind, 'brandName')]"));
    }
}