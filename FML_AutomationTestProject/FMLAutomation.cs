using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
<<<<<<< HEAD
using OpenQA.Selenium.Support.UI;
=======
>>>>>>> d9f2fb977a0baac8e441c54f0f484b880f8bf346

namespace FML_AutomationTestProject
{
    [TestClass]
<<<<<<< HEAD
    public sealed class FMLAutomation
=======
    public sealed class Test1
>>>>>>> d9f2fb977a0baac8e441c54f0f484b880f8bf346
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

<<<<<<< HEAD
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
=======
        [TestMethod]
        [Priority(0)]
        public void LaunchBrowser()
        {
            _driver.Navigate().GoToUrl("https://localhost:7030/");
            DelayForDemo();
        }

        [TestMethod]
        [Priority(1)]
        public void ShouldCreate_NewAdmin_And_Login()
        {
>>>>>>> d9f2fb977a0baac8e441c54f0f484b880f8bf346
            _driver.Navigate().GoToUrl("https://localhost:7030/Admins/Register/");
            DelayForDemo();

            //User Name
            IWebElement userName = _driver.FindElement(By.Id("UserName"));
<<<<<<< HEAD
            userName.Clear();
            userName.SendKeys("TestUser");
=======
            userName.SendKeys("Richa");
>>>>>>> d9f2fb977a0baac8e441c54f0f484b880f8bf346
            DelayForDemo();

            //Email
            IWebElement email = _driver.FindElement(By.Id("Email"));
<<<<<<< HEAD
            email.Clear();
            email.SendKeys("test.user@gmail.com");
=======
            email.SendKeys("rich.richa@gmail.com");
>>>>>>> d9f2fb977a0baac8e441c54f0f484b880f8bf346
            DelayForDemo();

            //Password 
            IWebElement password = _driver.FindElement(By.Id("Password"));
<<<<<<< HEAD
            password.Clear();
=======
>>>>>>> d9f2fb977a0baac8e441c54f0f484b880f8bf346
            password.SendKeys("Pa$$w0rd");
            DelayForDemo();

            //Confirm password
            IWebElement confirmPass = _driver.FindElement(By.Id("ConfirmPassword"));
<<<<<<< HEAD
            confirmPass.Clear();
=======
>>>>>>> d9f2fb977a0baac8e441c54f0f484b880f8bf346
            confirmPass.SendKeys("Pa$$w0rd");
            DelayForDemo();

            _driver.FindElement(By.CssSelector("button.btn.btn-success.w-100")).Click();
<<<<<<< HEAD


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
=======
            DelayForDemo();
        }
        private static void DelayForDemo()
        {
            Thread.Sleep(1000);
        }

      

>>>>>>> d9f2fb977a0baac8e441c54f0f484b880f8bf346
    }
}
