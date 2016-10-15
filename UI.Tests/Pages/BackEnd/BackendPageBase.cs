using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace UI.Tests.Pages.BackEnd
{
    public abstract class BackendPageBase
    {
        protected IWebDriver _driver;
        protected string AdminWebsiteUrl { get; private set; }

        protected BackendPageBase(IWebDriver driver)
        {
            _driver = driver;
        }

        public BackendMenuBar Menu => new BackendMenuBar(_driver);

        public virtual void NavigateToAdminWebsite()
        {
            _driver.Manage().Cookies.DeleteAllCookies();
            _driver.Navigate().GoToUrl(AdminWebsiteUrl);
            _driver.Manage().Cookies.DeleteAllCookies();
            _driver.Navigate().Refresh();
            Initialize();
        }

        public virtual void Initialize()
        {
            PageFactory.InitElements(_driver, this);
        }

        
    }
}
