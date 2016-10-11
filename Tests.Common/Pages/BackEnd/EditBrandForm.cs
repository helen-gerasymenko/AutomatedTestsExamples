using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Tests.Common.Extensions;

namespace Tests.Common.Pages.BackEnd
{
    public class EditBrandForm : BackendPageBase
    {
        public EditBrandForm(IWebDriver driver) : base(driver)
        {
        }

        public ViewBrandForm EditOnlyRequiredData(string brandType, string brandName, string brandCode, string remarks = "remarks")
        {
            var brandNameField = _driver.FindElementWait(By.XPath("//label[contains(., 'Brand Name')]/following-sibling::div/input"));
            brandNameField.Clear();
            brandNameField.SendKeys(brandName);

            var brandTypesField =
                _driver.FindElementWait(By.XPath("//label[contains(., 'Brand Type')]/following-sibling::div/select"));
            var brandTypesList = new SelectElement(brandTypesField);
            brandTypesList.SelectByText(brandType);

            var saveButton = _driver.FindElementWait(By.XPath("//button[text()='Save']"));
            saveButton.Click();
            var form = new ViewBrandForm(_driver);
            return form;
        }
    }
}