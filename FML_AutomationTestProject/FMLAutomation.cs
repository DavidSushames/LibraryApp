using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace FML_AutomationTestProject
{
    [TestClass]
    public sealed class FMLAutomation
    {
        private static IWebDriver _driver;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            options.AddArgument("--ignore-certificate-errors");

            _driver = new ChromeDriver(options);
            Console.WriteLine("Chrome opens for once for all test cases.");
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            //this will close the tabs after all the test are run 
            _driver.Quit();
            Console.WriteLine("Chrome browser closed after all tests");
        }

        [TestMethod]
        public void LaunchBrowser()
        {
            _driver.Navigate().GoToUrl("https://localhost:7030/");
        }

        [TestMethod]
        public void FullMetalLibrary_AutomationTests()
        {
            //Register
            _driver.Navigate().GoToUrl("https://localhost:7030/Admins/Register/");
            DelayForDemo();

            //User Name
            IWebElement userName = _driver.FindElement(By.Id("UserName"));
            userName.Clear();
            userName.SendKeys("TestUser");
            DelayForDemo();

            //Email
            IWebElement email = _driver.FindElement(By.Id("Email"));
            email.Clear();
            email.SendKeys("test.user@gmail.com");
            DelayForDemo();

            //Password 
            IWebElement password = _driver.FindElement(By.Id("Password"));
            password.Clear();
            password.SendKeys("Pa$$w0rd");
            DelayForDemo();

            //Confirm password
            IWebElement confirmPass = _driver.FindElement(By.Id("ConfirmPassword"));
            confirmPass.Clear();
            confirmPass.SendKeys("Pa$$w0rd");
            DelayForDemo();

            _driver.FindElement(By.CssSelector("button.btn.btn-success.w-100")).Click();


            //Login
            _driver.Navigate().GoToUrl("https://localhost:7030/Admins/Login");
            DelayForDemo();

            //Valid Email
            IWebElement loginEmail = _driver.FindElement(By.Id("Email"));
            loginEmail.Clear();
            loginEmail.SendKeys("test.user@gmail.com");
            DelayForDemo();

            //Valid Password
            IWebElement loginPass = _driver.FindElement(By.Id("Password"));
            loginPass.Clear();
            loginPass.SendKeys("Pa$$w0rd");
            DelayForDemo();

            //Click login button
            _driver.FindElement(By.CssSelector("button.btn.btn-primary.w-100")).Click();

            _driver.Navigate().GoToUrl("https://localhost:7030/Books/");
            DelayForDemo();

            _driver.Navigate().GoToUrl("https://localhost:7030/Books/Create/");
            DelayForDemo();

            //_driver.FindElement(By.CssSelector("button.btn.btn-success.w-100")).Click();
            //DelayForDemo();

            IWebElement title = _driver.FindElement(By.Id("Title"));
            title.Clear();
            title.SendKeys("The Book Name");
            DelayForDemo();

            IWebElement author = _driver.FindElement(By.Id("AuthorId"));
            author.SendKeys("1");

            IWebElement selectDate = _driver.FindElement(By.Id("PublishDate"));
            selectDate.Clear();
            selectDate.SendKeys("1994-10-06");
        }

        //[TestMethod]
        //public void GoTo_BookPage()
        //{
        //    _driver.Navigate().GoToUrl("https://localhost:7030/Books/");
        //    DelayForDemo();

        //    _driver.Navigate().GoToUrl("https://localhost:7030/Books/Create");
        //    DelayForDemo();

        //    _driver.FindElement(By.CssSelector("button.btn.btn-success.w-100")).Click();

        //    //Add a title 
        //    IWebElement title = _driver.FindElement(By.Id("Title"));
        //    title.Clear();
        //    title.SendKeys("Automation form Hell");
        //    DelayForDemo();
        //}

        private static void DelayForDemo()
        {
            Thread.Sleep(2000);
        }
    }
}
