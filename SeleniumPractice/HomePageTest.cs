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
    public class HomePageTest : BaseTest
    {
        [TestMethod]
        public void VerifyButtonIsDisplayed()
        {
            HomePage homepage = new HomePage(driver);
            Assert.AreEqual(3, homepage.GetElementsCount());
            Assert.AreEqual("Practice Page", homepage.GetHeaderText());
        }
    }
}
