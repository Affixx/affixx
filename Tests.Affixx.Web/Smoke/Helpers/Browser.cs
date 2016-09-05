using System;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using NUnit.Framework;
using System.Windows.Forms;
using OpenQA.Selenium.Support.UI;

namespace Tests.Affixx.Web.Smoke.Helpers
{
    public class Browser : IDisposable
    {
        private static IWebDriver driver = null;

        public Browser()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
        }

        internal void NavigateTo(string url)
        {
            driver.Navigate().GoToUrl(url);
        }

        internal bool SwitchTab(string tabUrl)
        {
            var baseWindowHandle = driver.CurrentWindowHandle;
            
            foreach (var handle in driver.WindowHandles) {
                var tab = driver.SwitchTo().Window(handle);
                if (tab.Url.ToLower().Contains(tabUrl.ToLower())) {
                    return true;
                }
            }

            return false;
        }

        internal void TypeIn(string cssSelector, string text)
        {
            var element = driver.FindElement(By.CssSelector(cssSelector));
            element.SendKeys(text);
        }

        internal void ClickOn(string cssSelector, bool scroll = false)
        {
            try {
                var javascript = (IJavaScriptExecutor)driver;
                if (scroll) {
                    //todo: investigate, for some reason Actions work only in Debug mode
                    //var actions = new Actions(driver);
                    //var element = driver.FindElement(By.CssSelector(cssSelector));
                    //actions
                    //    .MoveToElement(element)
                    //    .Build()
                    //    .Perform();


                    var hiddenElement = driver.FindElement(By.CssSelector(cssSelector));
                    javascript.ExecuteScript("arguments[0].scrollIntoView(true);", hiddenElement);
                    this.Sleep(1);
                }

                var element = driver.FindElement(By.CssSelector(cssSelector));
                element.Click();
            }
            catch (Exception ex) {
                throw new Exception($"Failed to click on {cssSelector}", ex);
            }
        }

        internal void SelectDropdown(string cssSelector, int index)
        {
            var element = driver.FindElement(By.CssSelector(cssSelector));
            var dropdown = new SelectElement(element);
            dropdown.SelectByIndex(index);
        }

        internal void SelectFile(string fileName)
        {
            var fullPath = System.IO.Path.Combine(TestContext.CurrentContext.TestDirectory, fileName);

            //for more details, see https://msdn.microsoft.com/en-us/library/system.windows.forms.sendkeys.sendwait.aspx#Anchor_1
            Sleep(1);
            SendKeys.SendWait(fullPath);
            Sleep(1);
            SendKeys.SendWait(@"{Enter}");
            Sleep(1);
        }

        public IWebElement GetElement(string cssSelector)
        {
            try {
                return driver.FindElement(By.CssSelector(cssSelector));
            }
            catch (Exception) {
                return null;
            }
        }

        public IWebElement GetElementByXpath(string xpath)
        {
            try {
                return driver.FindElement(By.XPath(xpath));
            }
            catch (Exception) {
                return null;
            }
        }

        internal void Sleep(int seconds, string reason = null)
        {
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(seconds));
        }

        /// <summary>
        /// Small helper method to show on the UI which test we're currently running.
        /// </summary>
        internal void SetTestTitle(string title)
        {
            var javascript = (IJavaScriptExecutor)driver;
            javascript.ExecuteScript($"$('span.logo').css('color', 'red').css('font-size', 'larger').text('{title}');");
        }

        public string Url
        {
            get
            {
                return driver.Url;
            }
        }

        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
