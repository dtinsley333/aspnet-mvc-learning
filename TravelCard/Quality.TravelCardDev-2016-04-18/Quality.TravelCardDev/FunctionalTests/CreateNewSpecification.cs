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
    public class CreateNewSpecification
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
        public void TheCreateNewSpecificationTest()
        {
            driver.Navigate().GoToUrl("http://tn-sqldevel:9016/");
            driver.FindElement(By.Id("ItemID")).Clear();
            driver.FindElement(By.Id("ItemID")).SendKeys("4125052");
            driver.FindElement(By.Name("SubmitConfirmation")).Click();
            driver.FindElement(By.LinkText("Add New Part Specification")).Click();
            driver.FindElement(By.Id("PartSpecification_Characteristic")).Clear();
            driver.FindElement(By.Id("PartSpecification_Characteristic")).SendKeys("test");
            driver.FindElement(By.Id("PartSpecification_CharacteristicES")).Clear();
            driver.FindElement(By.Id("PartSpecification_CharacteristicES")).SendKeys("Test");
            driver.FindElement(By.Id("PartSpecification_CharacteristicCN")).Clear();
            driver.FindElement(By.Id("PartSpecification_CharacteristicCN")).SendKeys("Test Test");
            // ERROR: Caught exception [ERROR: Unsupported command [select]]
            // ERROR: Caught exception [ERROR: Unsupported command [select]]
            // ERROR: Caught exception [ERROR: Unsupported command [select]]
            // ERROR: Caught exception [ERROR: Unsupported command [select]]
            driver.FindElement(By.Id("PartSpecification_LowSpec")).Clear();
            driver.FindElement(By.Id("PartSpecification_LowSpec")).SendKeys(".5");
            driver.FindElement(By.Id("PartSpecification_HighSpec")).Clear();
            driver.FindElement(By.Id("PartSpecification_HighSpec")).SendKeys(".9");
            driver.FindElement(By.Id("PartSpecification_OperationCode")).Clear();
            driver.FindElement(By.Id("PartSpecification_OperationCode")).SendKeys("0");
            driver.FindElement(By.Id("PartSpecification_Notes")).Click();
            driver.FindElement(By.Id("PartSpecification_Notes")).Clear();
            driver.FindElement(By.Id("PartSpecification_Notes")).SendKeys("Test NOtes");
            driver.FindElement(By.CssSelector("input.style2")).Click();
            // ERROR: Caught exception [ERROR: Unsupported command [isTextPresent]]
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
