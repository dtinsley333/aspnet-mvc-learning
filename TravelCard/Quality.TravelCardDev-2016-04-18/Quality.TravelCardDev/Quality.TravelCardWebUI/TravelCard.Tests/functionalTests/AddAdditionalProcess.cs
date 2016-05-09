using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace SeleniumTests
{
    [TestFixture]
    public class Firsttest
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;

        [SetUp]
        public void SetupTest()
        {
            driver = new FirefoxDriver();
            baseURL = "http://tn-sqldevel:9016/";
            verificationErrors = new StringBuilder();
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }

        [Test]
        public void TheFirstTest()
        {
            driver.Navigate().GoToUrl("/");
            driver.FindElement(By.Id("ItemID")).Clear();
            driver.FindElement(By.Id("ItemID")).SendKeys("4125052");
            driver.FindElement(By.Name("SubmitConfirmation")).Click();
            driver.FindElement(By.Id("UserSettingsButton")).Click();
            driver.FindElement(By.Id("ItemID")).Clear();
            driver.FindElement(By.Id("ItemID")).SendKeys("4125052");
            driver.FindElement(By.Name("SubmitConfirmation")).Click();
            driver.FindElement(By.LinkText("Add New Additional Process")).Click();
            driver.FindElement(By.Id("AdditionalProcess_Description")).Clear();
            driver.FindElement(By.Id("AdditionalProcess_Description")).SendKeys("test");
            driver.FindElement(By.Id("AdditionalProcess_DescriptionES")).Clear();
            driver.FindElement(By.Id("AdditionalProcess_DescriptionES")).SendKeys("test");
            driver.FindElement(By.Id("AdditionalProcess_DescriptionCN")).Clear();
            driver.FindElement(By.Id("AdditionalProcess_DescriptionCN")).SendKeys("test");
            // ERROR: Caught exception [ERROR: Unsupported command [select]]
            driver.FindElement(By.Id("AdditionalProcess_Notes")).Click();
            driver.FindElement(By.Id("AdditionalProcess_Notes")).Clear();
            driver.FindElement(By.Id("AdditionalProcess_Notes")).SendKeys("stes");
            driver.FindElement(By.CssSelector("input.style2")).Click();
            driver.FindElement(By.CssSelector("span.notification-close")).Click();
            driver.FindElement(By.XPath("//table[@id='ProcessesToSort']/tbody/tr[6]/td/span/a")).Click();
            // ERROR: Caught exception [ERROR: Unsupported command [getConfirmation]]
        }
        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }
}