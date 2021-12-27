using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumPractice
{
    public class HomePage
    {
        private IWebDriver driver;

        public HomePage(IWebDriver _driver)
        {
            driver = _driver;
        }

        private IList<IWebElement> elements { get { return driver.FindElements(By.XPath("//input[@type='radio']")); } }

        private IWebElement headerLabel { get { return driver.FindElement(By.XPath("//h1")); }}

        public int GetElementsCount()
        {
            int count = elements.Count;
            return count;
        }

        public string GetHeaderText()
        {
            string text = headerLabel.Text;
            return text;
        }
    }
}
