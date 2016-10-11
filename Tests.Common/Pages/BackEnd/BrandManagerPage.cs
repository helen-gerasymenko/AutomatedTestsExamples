using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Tests.Common.Extensions;

namespace Tests.Common.Pages.BackEnd
{
    public class BrandManagerPage : BackendPageBase
    {
        public BrandManagerPage (IWebDriver driver) : base(driver) { }

#region button xpaths

        public static By NewButton = By.XPath("//div[@data-view='brand/brand-manager/list']//span[text()='New']");
        public static By EditButton = By.XPath("//div[@data-view='brand/brand-manager/list']//span[text()='Edit']");
        public static By ActivateButton = By.XPath("//div[@data-view='brand/brand-manager/list']//span[text()='Activate']");

#endregion

        public IWebElement FindButton(By button)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            IWebElement element = null;
            wait.Until(d =>
            {
                element = _driver.FindElements(button).FirstOrDefault();
                return element != null;
            });
            return element;
        }

        public NewBrandForm OpenNewBrandForm()
        {
            var newButton = _driver.FindElementWait(By.XPath("//a[text()='new']"));
            newButton.Click();
            var newForm = new NewBrandForm(_driver);
            return newForm;
        }

        public ViewBrandForm OpenViewBrandForm(string brandName)
        {
            var viewButton = _driver.FindElementWait(By.XPath("//a[text()='view']"));
            viewButton.Click();
            var viewForm = new ViewBrandForm(_driver);
            return viewForm;
        }

 
        public EditBrandForm OpenEditBrandForm(string brand)
        {
            var editButton = _driver.FindElementWait(By.XPath("//a[text()='edit']"));
            editButton.Click();
            var editForm = new EditBrandForm(_driver);
            return editForm;
        }
    }
}
