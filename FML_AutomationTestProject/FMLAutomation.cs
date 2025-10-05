using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace FML_AutomationTestProject
{
    [TestClass]
    public sealed class Test1
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
            _driver.Navigate().GoToUrl("https://localhost:7030/Admins/Register/");
            DelayForDemo();

            //User Name
            IWebElement userName = _driver.FindElement(By.Id("UserName"));
            userName.SendKeys("Richa");
            DelayForDemo();

            //Email
            IWebElement email = _driver.FindElement(By.Id("Email"));
            email.SendKeys("rich.richa@gmail.com");
            DelayForDemo();

            //Password 
            IWebElement password = _driver.FindElement(By.Id("Password"));
            password.SendKeys("Pa$$w0rd");
            DelayForDemo();

            //Confirm password
            IWebElement confirmPass = _driver.FindElement(By.Id("ConfirmPassword"));
            confirmPass.SendKeys("Pa$$w0rd");
            DelayForDemo();

            _driver.FindElement(By.CssSelector("button.btn.btn-success.w-100")).Click();
            DelayForDemo();
        }
        private static void DelayForDemo()
        {
            Thread.Sleep(1000);
        }

      

    }
}
