using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using UI.Tests.Extensions;

namespace UI.Tests.Pages.BackEnd
{
    public class NewBrandForm : BackendPageBase
    {
        public NewBrandForm(IWebDriver driver) : base(driver){}

        public ViewBrandForm Submit(string name, string code, string playerPrefix, string type)
        {
            //enter brand details
            var brandNameField = _driver.FindElementWait(By.XPath("//input[contains(@id, 'brand-name') and contains(@data-bind, 'id: nameFieldId()')]"));
            brandNameField.SendKeys(name);

            var brandCodeField = _driver.FindElementWait(By.XPath("//input[contains(@id, 'brand-code') and contains(@data-bind, 'id: codeFieldId()')]"));
            brandCodeField.SendKeys(code);

            var brandTypeField = _driver.FindElementWait(By.XPath("//select[contains(@id, 'brand-type')]"));
            var brandTypeList = new SelectElement(brandTypeField);
            switch (type)
            {
                case "Deposit":
                    brandTypeList.SelectByText("Deposit");
                    break;
                case "Credit":
                    brandTypeList.SelectByText("Credit");
                    break;
                case "Integrated":
                    brandTypeList.SelectByText("Integrated");
                    break;
                default:
                    brandTypeList.SelectByText("Deposit");
                    break;
            }

            var playerPrefixField = _driver.FindElementWait(By.XPath("//input[contains(@id, 'brand-player-prefix') and contains(@data-bind, 'id: playerPrefixFieldId()')]"));
            playerPrefixField.SendKeys(playerPrefix);

           //save
            var saveButton = _driver.FindElementWait(By.XPath("//button[text()='Save' and not(contains(@class, 'inactive'))]"));
            saveButton.Click();
            var form = new ViewBrandForm(_driver);
            return form;
        }
    }
}