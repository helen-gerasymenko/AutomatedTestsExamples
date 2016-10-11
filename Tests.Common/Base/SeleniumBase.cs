using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Configuration;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Tests.Common.Pages.BackEnd;

namespace Tests.Common.Base
{
    [TestFixture]
    public abstract class SeleniumBase
    {
        protected IWebDriver    _driver;

        [OneTimeSetUp]
        public void BeforeAllSetUp()
        {
            try
            {
                BeforeAll();
            }
            catch (Exception ex)
            {
                BeforeAllFailed(ex);
                throw;
            }
        }
        public virtual void BeforeAll()
        {
            _driver = CreateFireFoxWebDriver();
            _driver.Url = GetWebsiteUrl();
            _driver.Manage().Cookies.DeleteAllCookies();
            _driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            _driver.Manage().Window.Size = new Size(1500, 900);
        }
        protected abstract string GetWebsiteUrl();
        
        public virtual void BeforeEach()
        {
        }
        private void BeforeAllFailed(Exception exception)
        {
            try
            {
               SaveScreenshot(_driver);
            }
            finally
            {
                QuitWebDriver();
            }
        }

        static IWebDriver CreateFireFoxWebDriver()
        {
            return new FirefoxDriver();
        }
        protected void QuitWebDriver()
        {
            if (_driver == null) return;

            //try to quit WebDriver upto 3 times
            var exceptionThrown = false;
            var retries = 0;
            do
            {
                try
                {
                    retries++;
                    _driver.Quit();
                }
                catch (Exception)
                {
                    exceptionThrown = true;
                    SaveScreenshot(_driver);
                    Thread.Sleep(TimeSpan.FromSeconds(3));
                }
            } while (exceptionThrown && retries <= 3);
        }
        public static void SaveScreenshot(IWebDriver driver)
        {
            if (driver == null)
                return;

            var path = WebConfigurationManager.AppSettings["ScreenshotsPath"];
            Directory.CreateDirectory(path);
            var testName = TestContext.CurrentContext.Test.Name;
    
            //generate screenshot name
            var fileName = $"{DateTime.Now:yyyy-MM-dd_hh-mm}-{testName}.{"png"}";
            var fullPath = Path.Combine(path, fileName);
           
            //make a screenshot and save
            Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            Thread.Sleep(500);
            screenshot.SaveAsFile(fullPath, ImageFormat.Png);
            Thread.Sleep(500);
        }

        public DashboardPage LoginToAdminWebsiteAsSuperAdmin(string username, string password)
        {
            var loginPage = new AdminWebsiteLoginPage(_driver);
            loginPage.NavigateToAdminWebsite();
            Logout();

            var page = loginPage.Login(username, password);
            return page;
        }

        public void Logout()
        {
            _driver.Manage().Cookies.DeleteAllCookies();
            _driver.Navigate().Refresh();
        }


        public class CategorySmoke : CategoryAttribute
        {
            public CategorySmoke() : base("Smoke") { }
        }

        
    }
}
