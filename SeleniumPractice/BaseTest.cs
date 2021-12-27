using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumPractice
{
    [TestClass]
    public class BaseTest
    {
        public IWebDriver driver;

        private TestContext _testContext;

        public TestContext TestContext 
        { 
            get { return _testContext; }

            set { _testContext = value; } 
        }

        [TestInitialize]
        public void SetUpDriver()
        {
            //read notepad and check the value of browser
            string browser = "chrome"; // firefox
            if (browser.Equals("chrome"))
            {
                driver = new ChromeDriver(@"C:\Users\Vivek Vasu\source\repos\SeleniumPractice\SeleniumPractice\Drivers");
            }
            else if (browser.Equals("firefox"))
            {
                driver = new FirefoxDriver(@"C:\Users\Vivek Vasu\source\repos\SeleniumPractice\SeleniumPractice\Drivers");
            }

            driver.Manage().Window.Maximize();
            driver.Url = "https://courses.letskodeit.com/practice";
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [TestCleanup]
        public void CloseDriver()
        {
            if(TestContext.CurrentTestOutcome == UnitTestOutcome.Failed)
            {
                ITakesScreenshot screenshot = driver as ITakesScreenshot;
                screenshot.GetScreenshot().SaveAsFile($@"C:\Users\Vivek Vasu\source\repos\SeleniumPractice\SeleniumPractice\Screenshots\{TestContext.TestName}.jpeg");
            }

            driver.Close();
            driver.Quit();
        }
    }
}
