using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Tests.Common.Extensions;

namespace Tests.Common.Pages.BackEnd
{
    public class BackendMenuBar
    {
        protected readonly IWebDriver _driver;
        public BackendMenuBar(IWebDriver driver)
        {
            _driver = driver;
        }

        public static readonly By BrandManager = By.XPath("//span[text()='Brand Manager']");

#region find menu sections
        public IWebElement GetAdminMenu => GetMenuItem("Admin");
        public IWebElement GetBrandMenu => GetMenuItem("Brand");

#endregion

 #region actions
        private IWebElement GetMenuItem(string title)
        {
            string xpath = $"//div[@id='sidebar']//span[@class='menu-text' and text()='{title}']";
            return _driver.FindElementWait(By.XPath(xpath));
        }

        public bool CheckIfMenuItemDisplayed(By menuItem)
        {
            var element = _driver.FindElements(menuItem).FirstOrDefault();
            return element != null && element.Displayed;
        }

        public BrandManagerPage ClickBrandManagerItem()
        {
            var menuItem = By.XPath("//span[text()='Brand Manager']");
            if (_driver.FindElements(menuItem).Count(x => x.Displayed && x.Enabled) == 0)
            {
                GetBrandMenu.Click();
            }
            var submenu = _driver.FindElementWait(menuItem);
            submenu.Click();
            var page = new BrandManagerPage(_driver);
            return page;
        }

        public RoleManagerPage ClickRoleManagerMenuItem()
        {
            var menuItem = By.XPath("//div[@id='sidebar']//span[text()='Role Manager']");
            if (_driver.FindElements(menuItem).Count(x => x.Displayed && x.Enabled) == 0)
            {
                GetAdminMenu.Click();
            }
            var submenu = _driver.FindElementWait(menuItem);
            submenu.Click();
            var page = new RoleManagerPage(_driver);
            return page;
        }

        #endregion

        public AdminManagerPage ClickAdminManagerMenuItem()
        {
            var menuItem = By.XPath("//div[@id='sidebar']//span[text()='Admin Manager']");
            if (_driver.FindElements(menuItem).Count(x => x.Displayed && x.Enabled) == 0)
            {
                GetAdminMenu.Click();
            }
            var submenu = _driver.FindElementWait(menuItem);
            submenu.Click();
            var page = new AdminManagerPage(_driver);
            return page;
        }
    }
}
