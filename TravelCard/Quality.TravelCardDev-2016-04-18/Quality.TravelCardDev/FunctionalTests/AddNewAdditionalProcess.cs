using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumTests
{
    [TestFixture]
    public class AddNewAdditionalProcess
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;

        [SetUp]
        public void SetupTest()
        {
            driver = new ChromeDriver();
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
        public void TheAddNewAdditionalProcessTest()
        {
            driver.Navigate().GoToUrl("http://tn-sqldevel:9016/");
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
           
            IWebElement select= driver.FindElement(By.Id("AdditionalProcess_SequenceID"));
           
            select.SendKeys("18");
            // ERROR: Caught exception [ERROR: Unsupported command [addSelection]]
            driver.FindElement(By.CssSelector("input.style2")).Click();
            try
            {
                Assert.IsTrue(IsElementPresent(By.CssSelector("span.notification")));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
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
