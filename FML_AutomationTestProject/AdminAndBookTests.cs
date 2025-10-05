using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FML_AutomationTestProject
{
    [TestClass]
    public sealed class AdminAndBookTests
    {
        private static IWebDriver _driver;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            options.AddArgument("--ignore-certificate-errors");

            _driver = new ChromeDriver(options);
            Console.WriteLine("Chrome opened once for all test cases.");
        }

        [ClassCleanup]
        public static void TearDown()
        {
            _driver.Quit();
        }

        private static void DelayForDemo()
        {
            Thread.Sleep(1000);
        }


        //Admin tests

        //[TestMethod]
       // public void Redirect_ToLogin_WhenNotLoggedIn()
       // {
       //     _driver.Navigate().GoToUrl("https://localhost:7030/Books/Create");  
        //    DelayForDemo();

        //    Assert.IsTrue(_driver.Url.Contains("/Admins/Login"));
        //}

        [TestMethod]
        public void Login_Successfully()
        {
            _driver.Navigate().GoToUrl("https://localhost:7030/Admins/Register/");
            DelayForDemo();

            _driver.FindElement(By.Id("UserName")).SendKeys("AutoUser");
            _driver.FindElement(By.Id("Email")).SendKeys("auto@admin.com");
            _driver.FindElement(By.Id("Password")).SendKeys("TestAuto@123");
            _driver.FindElement(By.Id("ConfirmPassword")).SendKeys("TestAuto@123");
            _driver.FindElement(By.CssSelector("button.btn.btn-success.w-100")).Click();
            DelayForDemo();

            Assert.IsTrue(_driver.PageSource.Contains("Logout", StringComparison.OrdinalIgnoreCase));
        }
        [TestMethod]
        public void Show_AdminIndex()
        {
            _driver.Navigate().GoToUrl("https://localhost:7030/");
            DelayForDemo();

            _driver.FindElement(By.LinkText("Admins")).Click();
            DelayForDemo();

            Assert.IsTrue(_driver.PageSource.Contains("Add New Admin", StringComparison.OrdinalIgnoreCase));
        }

        [TestMethod]
        public void Search_Admins()
        {
            _driver.Navigate().GoToUrl("https://localhost:7030/");
            DelayForDemo();

            _driver.FindElement(By.LinkText("Admins")).Click();
            DelayForDemo();

            IWebElement searchBox = _driver.FindElement(By.Name("searchString")); 
            searchBox.Clear();
            searchBox.SendKeys("auto");
            DelayForDemo();

            _driver.FindElement(By.CssSelector("input[type='submit'][value='Search']")).Click();
            DelayForDemo();

            Assert.IsTrue(_driver.PageSource.Contains("auto", StringComparison.OrdinalIgnoreCase));
        }

    }
    
}
