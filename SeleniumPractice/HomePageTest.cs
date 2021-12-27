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
    class HomePageTest : BaseTest
    {

        [TestMethod]
        public void VerifyButtonIsDisplayed()
        {
            Console.WriteLine("Homepage Test");
        }
    }
}
