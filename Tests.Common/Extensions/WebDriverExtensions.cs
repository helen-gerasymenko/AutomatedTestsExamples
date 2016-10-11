using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Tests.Common.Extensions
{
    public static class WebDriverExtensions
    {

        public static string FindElementValue(this IWebDriver driver, By by)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

            IWebElement element = null;
            wait.Until(d =>
            {
                try
                {
                    element = driver.FindElements(@by).FirstOrDefault(x => x.Displayed);
                }
                catch (StaleElementReferenceException)
                {
                    //there may be some page/control refreshes happening during this time
                    //so it's totally fine to ignore this specific exception if it happens
                }

                return element != null && element.Text != string.Empty;
            });

            return element.Text;
        }

        public static IWebElement FindElementWait(this IWebDriver driver, By by)
        {
            var elements = FindElementsWait(driver, @by);

            return elements.First();
        }

        public static IEnumerable<IWebElement> FindElementsWait(this IWebDriver driver, By by)
        {
            var elements = FindAnyElementsWait(driver, @by, x => x.Displayed && x.Enabled);

            return elements;
        }

        public static IEnumerable<IWebElement> FindAnyElementsWait(this IWebDriver driver, By by, Func<IWebElement, bool> predicate = null)
        {
           var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));

            IEnumerable<IWebElement> foundElements = null;

            const int maxAttemptCount = 5;
            var attemptCount = 0;
            while (true)
            {
                try
                {
                    wait.Until(d =>
                    {
                        foundElements = driver.FindElements(@by);

                        if (predicate != null)
                            foundElements = foundElements.Where(predicate);

                        return foundElements.Any();
                    });
                    break;
                }
                catch (StaleElementReferenceException)
                {
                    attemptCount++;
                    if (attemptCount < maxAttemptCount)
                    {
                        continue;
                    }
                    throw;
                }
            }

            return foundElements;
        }

        public static void FindElementClick(this IWebDriver driver, string element)
        {
            driver.FindElementWait(By.XPath(element)).Click();
        }
    }
}
