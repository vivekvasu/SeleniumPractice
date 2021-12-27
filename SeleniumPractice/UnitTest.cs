using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Collections.Generic;
using OpenQA.Selenium.Interactions;
using SeleniumExtras.WaitHelpers;

namespace SeleniumPractice
{
    [TestClass]
    public class UnitTest : BaseTest
    {

        [TestMethod]
        public void FirstTest()
        {
            IWebElement headerLabel = driver.FindElement(By.XPath("//h1"));
            IWebElement firstname = driver.FindElement(By.Id("name"));
            IWebElement bmwRadioButton = driver.FindElement(By.Id("bmwradio"));
            IWebElement benzCheckbox = driver.FindElement(By.Id("benzcheck"));
            IWebElement carDropdown = driver.FindElement(By.Id("carselect"));
            IWebElement multiSelectDropdown = driver.FindElement(By.Id("multiple-select-example"));
            IWebElement confirmButton = driver.FindElement(By.Id("confirmbtn"));
    
            Console.WriteLine(headerLabel.Text);
            Assert.AreEqual("Practice Pages", headerLabel.Text);

            firstname.SendKeys("Test Automation");

            bool isSelected = bmwRadioButton.Selected;
            Console.WriteLine(isSelected);

            bmwRadioButton.Click();
            //Printing radio button is selected
            bool isSelectedAfterSelection = bmwRadioButton.Selected;
            Console.WriteLine(isSelectedAfterSelection);
            Console.WriteLine(benzCheckbox.Selected);
            benzCheckbox.Click();
            Console.WriteLine(benzCheckbox.Selected);

            SelectElement select = new SelectElement(carDropdown);
            select.SelectByText("Benz");
            Console.WriteLine(select.SelectedOption.Text);

            select.SelectByValue("honda");
            Console.WriteLine(select.SelectedOption.Text);

            select.SelectByIndex(0);
            Console.WriteLine(select.SelectedOption.Text);

            IList<IWebElement> list = select.Options;
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine(list[i].Text);
            }


            SelectElement select1 = new SelectElement(multiSelectDropdown);
            //select multiple options from a multi select dropdowns
            select1.SelectByText("Apple");
            select1.SelectByValue("orange");

            //print all the selected options
            IList<IWebElement> list1 = select1.AllSelectedOptions;
            for (int i = 0; i < list1.Count; i++)
            {
                Console.WriteLine(list1[i].Text);
            }

            //Deselect one option from a multiselect dropdown
            select1.DeselectByText("Apple");
            //Get all the selected options and print
            IList<IWebElement> list2 = select1.AllSelectedOptions;
            for (int i = 0; i < list2.Count; i++)
            {
                Console.WriteLine(list2[i].Text);
            }

            //Alerts
            confirmButton.Click();
            IAlert alert =  driver.SwitchTo().Alert();
            //Click on Ok in Alert
            alert.Accept();

            confirmButton.Click();

            //Switching the driver control to the alert
            alert = driver.SwitchTo().Alert();
            
            //Print alert text
            Console.WriteLine(alert.Text);

            //To enter some values in alertbox
            //alert.SendKeys("");

            //TO specify username and password in alert
            //alert.SetAuthenticationCredentials("username", "password");

            //To Cancel the alert
            alert.Dismiss();
        }


        [TestMethod]
        public void VerifyButtonIsEnabledDisabed()
        {
        
            IWebElement enableDisableInput = driver.FindElement(By.Id("enabled-example-input"));
            IWebElement disableButton = driver.FindElement(By.Id("disabled-button"));

            string attributeValue = disableButton.GetAttribute("value");

            Console.WriteLine(attributeValue);

            Assert.AreEqual("Disable", attributeValue);

            //Printing whether the button is enabled
            Console.WriteLine(enableDisableInput.Enabled);

            enableDisableInput.SendKeys("Test Automation");
            disableButton.Click();
            Console.WriteLine(enableDisableInput.Enabled);

        }

        [TestMethod]
        public void VerifyButtonIsDisplayed()
        {

            IWebElement showHideInput = driver.FindElement(By.Name("show-hide"));
            IWebElement hideButton = driver.FindElement(By.Id("hide-textbox"));

            //Printing whether the button is enabled
            Console.WriteLine(showHideInput.Displayed);

            showHideInput.SendKeys("Test Automation");
            hideButton.Click();
            Console.WriteLine(showHideInput.Displayed);

        }

        [TestMethod]
        public void VerifyTopButtonIsWorking()
        {
            IWebElement topButton = driver.FindElement(By.XPath("//a[text()='Top']"));
            IWebElement mousehoverButton = driver.FindElement(By.Id("mousehover"));

            Console.WriteLine(topButton.Displayed);

            //Moving mouse pointer to the button
            Actions actions = new Actions(driver);
            actions.MoveToElement(mousehoverButton).Perform();

            //To perform right click
            
            //actions.ContextClick();

            //To perform doiuble click
            //actions.DoubleClick();

            Console.WriteLine(topButton.Displayed);
            topButton.Click();
            Assert.Fail();
        }

        [TestMethod]
        public void VerifyMultipleWindows()
        {
            IWebElement openTabButton = driver.FindElement(By.Id("opentab"));

            string mainWindow = driver.CurrentWindowHandle;
            //tab 1 = "abc"
            Console.WriteLine(mainWindow);

            openTabButton.Click();

            IList<string> tabsList =  driver.WindowHandles; // {"abc","def"}

            Console.WriteLine($"Tab Count : {tabsList.Count}");

            Assert.IsTrue(tabsList.Count == 2);
            Assert.AreEqual(2, tabsList.Count);

            // 2 tabs
            //tab 1 = "abc" // 0
            //tab 2 = "def" // 1

            for(int i = 0; i < tabsList.Count; i++)
            {
                Console.WriteLine(tabsList[i]);
                if(tabsList[i] != mainWindow)
                {
                    driver.SwitchTo().Window(tabsList[i]);
                }
            }

            IWebElement searchInput = driver.FindElement(By.Id("search"));
            Console.WriteLine(searchInput.Displayed);
        }

        [TestMethod]
        public void VerifyIframe()
        {
            //Launch url/application                Application should be launched    Application is launched
            //Verify search input is displayed within frame     Input should be displayed          ....
            //Verify opentab is displayed outside frame     opentab should be displayed          ....

            IWebElement frameElement = driver.FindElement(By.Id("courses-iframe"));
            driver.SwitchTo().Frame(frameElement);
            IWebElement searchInput = driver.FindElement(By.Id("search"));
            bool actual = searchInput.Displayed;

            //searchInput should be displayed
            Assert.AreEqual(false, actual);

            driver.SwitchTo().DefaultContent();
            IWebElement openTabButton = driver.FindElement(By.Id("opentab"));

            //opentab should be displayed
            Assert.AreEqual(true, openTabButton.Displayed);
        }

        [TestMethod]
        public void TakeScreenshot()
        {
            //Dire
            ITakesScreenshot screenshot = driver as ITakesScreenshot;
            screenshot.GetScreenshot().SaveAsFile(@"C:\Users\Vivek Vasu\source\repos\SeleniumPractice\SeleniumPractice\Screenshots\screenshot.jpeg");
        }


        [TestMethod]
        public void DifferentTypesOfWaits()
        {
            By openTabButton = By.Id("opentab");

            // Implicit Wait
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            // Explicit Wait
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.ElementToBeClickable(openTabButton));

            driver.FindElement(openTabButton).Click();

            // Fluent Wait

            // max wait 60 sec
            // interval 10 sec
            DefaultWait<IWebDriver> fluentWait = new DefaultWait<IWebDriver>(driver);
            fluentWait.Timeout = TimeSpan.FromSeconds(60);
            fluentWait.PollingInterval = TimeSpan.FromSeconds(5);
            fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            fluentWait.Until(ExpectedConditions.ElementIsVisible(openTabButton));

            driver.FindElement(openTabButton).Click();
        }
    

        [TestMethod]
        public void Elements()
        {
           IList<IWebElement> elements =  driver.FindElements(By.XPath("//input[@type='radio']"));
           Console.WriteLine(elements.Count);
           Assert.IsTrue(elements.Count == 3);
        }
    }
}
