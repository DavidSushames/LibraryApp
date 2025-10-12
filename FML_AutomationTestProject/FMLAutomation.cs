using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
namespace FML_AutomationTestProject
{
    [TestClass]
    public sealed class FMLAutomation
    {
        private static ChromeDriver _driver;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            options.AddArgument("--ignore-certificate-errors");

            //Using these methods to stop po-ups messages, it just annoying for automation testing
            options.AddUserProfilePreference("credentials_enable_service", false);
            options.AddUserProfilePreference("profile.password_manager_enabled", false);
            options.AddUserProfilePreference("profile.default_content_setting_values.notifications", 2);

            _driver = new ChromeDriver(options);
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10); // wait 10 sec 
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
            _driver.Navigate().GoToUrl("https://localhost:7030/Admins/Register/");
            DelayForDemo();

            //User Name
            IWebElement userName = _driver.FindElement(By.Id("UserName"));
            userName.Clear();
            userName.SendKeys("Test User");
            DelayForDemo();

            //Invalid email
            //IWebElement invalidEmail = _driver.FindElement(By.Id("Email"));
            //invalidEmail.Clear();
            //invalidEmail.SendKeys("test-user-gmail.com");
            //DelayForDemo();

            //Invalid Password 
            //IWebElement invalidPass = _driver.FindElement(By.Id("Password"));
            //invalidPass.Clear();
            //invalidPass.SendKeys("Password123");
            //DelayForDemo();

            //Password checking 
            //IWebElement invalidConfrim = _driver.FindElement(By.Id("ConfirmPassword"));
            //invalidConfrim.Clear();
            //invalidConfrim.SendKeys("Password123");
            //DelayForDemo();

            //_driver.FindElement(By.CssSelector("button.btn.btn-success.w-100")).Click();
            //DelayForDemo();

            //IWebElement validEmail1 = _driver.FindElement(By.Id("Email"));
            //validEmail1.Clear();
            //validEmail1.SendKeys("test.user@gmail.com");
            //DelayForDemo();

            //IWebElement invalidPass1 = _driver.FindElement(By.Id("Password"));
            //invalidPass1.Clear();
            //invalidPass1.SendKeys("Password123");
            //DelayForDemo();

            //IWebElement confirmPass1 = _driver.FindElement(By.Id("ConfirmPassword"));
            //confirmPass1.Clear();
            //confirmPass1.SendKeys("Password123");
            //DelayForDemo();

            //_driver.FindElement(By.CssSelector("button.btn.btn-success.w-100")).Click();
            //DelayForDemo();

            //IWebElement validEmail2 = _driver.FindElement(By.Id("Email"));
            //validEmail2.Clear();
            //validEmail2.SendKeys("test.user@gmail.com");
            //DelayForDemo();

            //IWebElement validPass2 = _driver.FindElement(By.Id("Password"));
            //validPass2.Clear();
            //validPass2.SendKeys("Pa$$w0rd");

            //IWebElement inavlidPass2 = _driver.FindElement(By.Id("ConfirmPassword"));
            //inavlidPass2.Clear();
            //inavlidPass2.SendKeys("Password123");
            //DelayForDemo();

            //_driver.FindElement(By.CssSelector("button.btn.btn-success.w-100")).Click();
            //DelayForDemo();

            IWebElement validEmail3 = _driver.FindElement(By.Id("Email"));
            validEmail3.Clear();
            validEmail3.SendKeys("test.user@gmail.com");
            DelayForDemo();

            IWebElement password = _driver.FindElement(By.Id("Password"));
            password.Clear();
            password.SendKeys("Pa$$w0rd");
            DelayForDemo();

            IWebElement confirmPass = _driver.FindElement(By.Id("ConfirmPassword"));
            confirmPass.Clear();
            confirmPass.SendKeys("Pa$$w0rd");
            DelayForDemo();

            _driver.FindElement(By.CssSelector("button.btn.btn-success.w-100")).Click();
            DelayForDemo();


            //Login page 
            _driver.Navigate().GoToUrl("https://localhost:7030/Admins/Login");
            DelayForDemo();

            IWebElement invalidEmail1 = _driver.FindElement(By.Id("Email"));
            invalidEmail1.Clear();
            invalidEmail1.SendKeys("test-user-email.com");
            DelayForDemo();

            IWebElement invalidPass3 = _driver.FindElement(By.Id("Password"));
            invalidPass3.Clear();
            invalidPass3.SendKeys("Password123");
            DelayForDemo();

            _driver.FindElement(By.CssSelector("button.btn.btn-primary.w-100")).Click();
            DelayForDemo();

            IWebElement validEmail4 = _driver.FindElement(By.Id("Email"));
            validEmail4.Clear();
            validEmail4.SendKeys("test.user@gmail.com");
            DelayForDemo();

            IWebElement invalidPass4 = _driver.FindElement(By.Id("Password"));
            invalidPass4.Clear();
            invalidPass4.SendKeys("Password123");
            DelayForDemo();

            _driver.FindElement(By.CssSelector("button.btn.btn-primary.w-100")).Click();
            DelayForDemo();

            IWebElement validEmail5 = _driver.FindElement(By.Id("Email"));
            validEmail5.Clear();
            validEmail5.SendKeys("test.user@gmail.com");
            DelayForDemo();

            IWebElement validPass5 = _driver.FindElement(By.Id("Password"));
            validPass5.Clear();
            validPass5.SendKeys("Pa$$w0rd");
            DelayForDemo();

            _driver.FindElement(By.CssSelector("button.btn.btn-primary.w-100")).Click();
            DelayForDemo();

            //Book 
            _driver.Navigate().GoToUrl("https://localhost:7030/Books/");
            DelayForDemo();

            _driver.FindElement(By.CssSelector("a[href='/Books/Create']")).Click();

            IWebElement emptyTitle = _driver.FindElement(By.Id("Title"));
            emptyTitle.Clear();
            emptyTitle.SendKeys("");
            DelayForDemo();

            var authorSelect = new OpenQA.Selenium.Support.UI.SelectElement(_driver.FindElement(By.Id("AuthorId")));
            authorSelect.SelectByText("William Powell");
            DelayForDemo();

            IWebElement selected = _driver.FindElement(By.Id("PublishDate"));
            selected.Clear();
            selected.SendKeys("24-02-1999");
            DelayForDemo();

            IWebElement genre = _driver.FindElement(By.Id("Genre"));
            genre.Clear();
            genre.SendKeys("Educational");
            DelayForDemo();

            IWebElement tickBtn = _driver.FindElement(By.Id("Available"));
            if (!tickBtn.Selected)
            {
                tickBtn.Click();
            }
            DelayForDemo();

            _driver.FindElement(By.CssSelector("input.btn.btn-primary")).Click();
            DelayForDemo();

            IWebElement title = _driver.FindElement(By.Id("Title"));
            title.Clear();
            title.SendKeys("The Damn Book");
            DelayForDemo();

            var authorSelect2 = new OpenQA.Selenium.Support.UI.SelectElement(_driver.FindElement(By.Id("AuthorId")));
            authorSelect2.SelectByText("William Powell");
            DelayForDemo();

            IWebElement genre1 = _driver.FindElement(By.Id("Genre"));
            genre1.Clear();
            genre1.SendKeys("Educational");
            DelayForDemo();


            IWebElement tickBtn2 = _driver.FindElement(By.Id("Available"));
            if (!tickBtn2.Selected)
            {
                tickBtn2.Click();
            }
            DelayForDemo();

            _driver.FindElement(By.CssSelector("input.btn.btn-primary")).Click();
            DelayForDemo();

            _driver.FindElement(By.XPath("//a[contains(@href, '/Books/Edit')]")).Click();
            DelayForDemo();

            IWebElement title1 = _driver.FindElement(By.Id("Title"));
            title1.Clear();
            title1.SendKeys("Another Damn Book");
            DelayForDemo();

            var authorSelect3 = new OpenQA.Selenium.Support.UI.SelectElement(_driver.FindElement(By.Id("AuthorId")));
            authorSelect3.SelectByText("Lee Marvin");
            DelayForDemo();

            IWebElement date1 = _driver.FindElement(By.Id("PublishDate"));
            date1.Clear();
            date1.SendKeys("09-10-2000");
            DelayForDemo();

            IWebElement genre2 = _driver.FindElement(By.Id("Genre"));
            genre2.Clear();
            genre2.SendKeys("Sci-Fi");
            DelayForDemo();

            IWebElement tickBtn3 = _driver.FindElement(By.Id("Available"));
            if (!tickBtn3.Selected)
            {
                tickBtn3.Click();
            }
            DelayForDemo();

            _driver.FindElement(By.CssSelector("input.btn.btn-primary")).Click();
            DelayForDemo();

            _driver.FindElement(By.XPath("//a[contains(@href, '/Books/Details')]")).Click();
            DelayForDemo();

            _driver.Navigate().GoToUrl("https://localhost:7030/Books");
            DelayForDemo();

            _driver.FindElement(By.XPath("//a[contains(@href, '/Books/Delete')]")).Click();
            DelayForDemo();

            _driver.FindElement(By.CssSelector("input.btn.btn-danger")).Click();
            DelayForDemo();

            IWebElement sortZA1 = _driver.FindElement(By.CssSelector("a.btn.btn-outline-primary[href='/Books?sortOrder=za']"));
            sortZA1.Click();
            DelayForDemo();

            IWebElement sortAZ= _driver.FindElement(By.CssSelector("a.btn.btn-outline-primary[href='/Books?sortOrder=az']"));
            sortAZ.Click();
            DelayForDemo();

            IWebElement search1 = _driver.FindElement(By.Name("searchString"));
            search1.Clear();
            search1.SendKeys("The Damn Book");
            DelayForDemo();

            _driver.FindElement(By.CssSelector("input.btn.btn-primary")).Click();
            DelayForDemo();

            _driver.Navigate().GoToUrl("https://localhost:7030/Authors");
            DelayForDemo();

            _driver.FindElement(By.CssSelector("a.btn.btn-success[href='/Authors/Create']")).Click();
            DelayForDemo();

            IWebElement fname = _driver.FindElement(By.Id("FirstName"));
            fname.Clear();
            fname.SendKeys("");
            DelayForDemo();

            IWebElement lname = _driver.FindElement(By.Id("LastName"));
            lname.Clear();
            lname.SendKeys("Mario");
            DelayForDemo();

            _driver.FindElement(By.CssSelector("input.btn.btn-primary[value='Create']")).Click();
            DelayForDemo();

            IWebElement fname1 = _driver.FindElement(By.Id("FirstName"));
            fname1.Clear();
            fname1.SendKeys("Mario");
            DelayForDemo();

            IWebElement lname1 = _driver.FindElement(By.Id("LastName"));
            lname1.Clear();
            lname1.SendKeys("");
            DelayForDemo();

            _driver.FindElement(By.CssSelector("input.btn.btn-primary[value='Create']")).Click();
            DelayForDemo();

            IWebElement fname2 = _driver.FindElement(By.Id("FirstName"));
            fname2.Clear();
            fname2.SendKeys("Mario");
            DelayForDemo();

            IWebElement lname2 = _driver.FindElement(By.Id("LastName"));
            lname2.Clear();
            lname2.SendKeys("Mario");
            DelayForDemo();

            _driver.FindElement(By.CssSelector("input.btn.btn-primary[value='Create']")).Click();
            DelayForDemo();

            _driver.FindElement(By.XPath("//a[contains(@href, '/Authors/Edit')]")).Click();
            DelayForDemo();

            IWebElement fname3= _driver.FindElement(By.Id("FirstName"));
            fname3.Clear();
            fname3.SendKeys("Luigi");
            DelayForDemo();

            IWebElement lname3 = _driver.FindElement(By.Id("LastName"));
            lname3.Clear();
            lname3.SendKeys("Luigi");
            DelayForDemo();

            _driver.FindElement(By.CssSelector("input.btn.btn-primary[value='Save']")).Click();
            DelayForDemo();

            _driver.FindElement(By.XPath("//a[contains(@href, '/Authors/Details')]")).Click();

            _driver.Navigate().GoToUrl("https://localhost:7030/Authors");
            DelayForDemo();

            _driver.FindElement(By.XPath("//a[contains(@href, '/Authors/Delete')]")).Click();

            _driver.FindElement(By.CssSelector("input.btn.btn-danger")).Click();
            DelayForDemo();

            IWebElement sortZA2 = _driver.FindElement(By.CssSelector("a.btn.btn-outline-primary[href='/Authors?sortOrder=za']"));
            sortZA2.Click();
            DelayForDemo();

            IWebElement sortAZ2 = _driver.FindElement(By.CssSelector("a.btn.btn-outline-primary[href='/Authors?sortOrder=az']"));
            sortAZ2.Click();
            DelayForDemo();

            IWebElement search2 = _driver.FindElement(By.Name("searchString"));
            search2.Clear();
            search2.SendKeys("William");
            DelayForDemo();

            _driver.FindElement(By.CssSelector("input.btn.btn-primary")).Click();
            DelayForDemo();

            _driver.Navigate().GoToUrl("https://localhost:7030/Admins");
            DelayForDemo();

            _driver.Navigate().GoToUrl("https://localhost:7030/Admins");
            DelayForDemo();

            _driver.Navigate().GoToUrl("https://localhost:7030/Admins/Create");
            DelayForDemo();

            IWebElement adName = _driver.FindElement(By.Id("UserName"));
            adName.Clear();
            adName.SendKeys("");
            DelayForDemo();

            IWebElement adEmail = _driver.FindElement(By.Id("EmailAddress"));
            adEmail.Clear();
            adEmail.SendKeys("");
            DelayForDemo();

            IWebElement adPass = _driver.FindElement(By.Id("PasswordHash"));
            adPass.Clear();
            adPass.SendKeys("");
            DelayForDemo();

            _driver.FindElement(By.CssSelector("input.btn.btn-primary")).Click();
            DelayForDemo();

            IWebElement adName2 = _driver.FindElement(By.Id("UserName"));
            adName2.Clear();
            adName2.SendKeys("Test User");
            DelayForDemo();

            IWebElement adEmail2 = _driver.FindElement(By.Id("EmailAddress"));
            adEmail2.Clear();
            adEmail2.SendKeys("test-user-gmail.com");
            DelayForDemo();

            IWebElement adPass2 = _driver.FindElement(By.Id("PasswordHash"));
            adPass2.Clear();
            adPass2.SendKeys("Pa$$w0rd");
            DelayForDemo();

            _driver.FindElement(By.CssSelector("input.btn.btn-primary")).Click();
            DelayForDemo();

            IWebElement adName3 = _driver.FindElement(By.Id("UserName"));
            adName3.Clear();
            adName3.SendKeys("Test User 2");
            DelayForDemo();

            IWebElement adEmail3 = _driver.FindElement(By.Id("EmailAddress"));
            adEmail3.Clear();
            adEmail3.SendKeys("test.user2@gmail.com");
            DelayForDemo();

            IWebElement adPass3 = _driver.FindElement(By.Id("PasswordHash"));
            adPass3.Clear();
            adPass3.SendKeys("Password123");
            DelayForDemo();

            _driver.FindElement(By.CssSelector("input.btn.btn-primary")).Click();
            DelayForDemo();

            IWebElement adName4 = _driver.FindElement(By.Id("UserName"));
            adName4.Clear();
            adName4.SendKeys("Test User 2");
            DelayForDemo();

            IWebElement adEmail4 = _driver.FindElement(By.Id("EmailAddress"));
            adEmail4.Clear();
            adEmail4.SendKeys("test.user2@gmail.com");
            DelayForDemo();

            IWebElement adPass4 = _driver.FindElement(By.Id("PasswordHash"));
            adPass4.Clear();
            adPass4.SendKeys("Pa$$w0rd");
            DelayForDemo();

            _driver.FindElement(By.CssSelector("input.btn.btn-primary")).Click();
            DelayForDemo();

            _driver.FindElement(By.XPath("//a[contains(@href, '/Admins/Edit')]")).Click();
            DelayForDemo();

            IWebElement adUserName = _driver.FindElement(By.Id("UserName"));
            adUserName.Clear();
            adUserName.SendKeys("Admin User");
            DelayForDemo();

            IWebElement adEmail5 = _driver.FindElement(By.Id("EmailAddress"));
            adEmail5.Clear();
            adEmail5.SendKeys("admin.testuser@gmail.com");
            DelayForDemo();

            IWebElement adPass5 = _driver.FindElement(By.Id("PasswordHash"));
            adPass5.Clear();
            adPass5.SendKeys("Adm!n123");
            DelayForDemo();

            IWebElement tickBtn4 = _driver.FindElement(By.Id("IsActive"));
            if (!tickBtn4.Selected)
            {
                tickBtn4.Click();
            }
            DelayForDemo();

            _driver.FindElement(By.CssSelector("input.btn.btn-primary")).Click();

            IWebElement sortZA3 = _driver.FindElement(By.CssSelector("a.btn.btn-outline-primary[href='/Admins?sortOrder=za']"));
            sortZA3.Click();
            DelayForDemo();

            IWebElement sortAZ3 = _driver.FindElement(By.CssSelector("a.btn.btn-outline-primary[href='/Admins?sortOrder=az']"));
            sortAZ3.Click();
            DelayForDemo();

            IWebElement search3 = _driver.FindElement(By.Name("searchString"));
            search3.Clear();
            search3.SendKeys("Admin User");
            DelayForDemo();

            _driver.FindElement(By.CssSelector("input.btn.btn-primary")).Click();
            DelayForDemo();

            _driver.FindElement(By.XPath("//a[contains(@href, '/Admins/Details')]")).Click();
            DelayForDemo();

            _driver.Navigate().GoToUrl("https://localhost:7030/Admins");
            DelayForDemo();

            _driver.FindElement(By.XPath("//a[contains(@href, '/Admins/Delete')]")).Click();
            DelayForDemo();

            _driver.Navigate().GoToUrl("https://localhost:7030/");
            DelayForDemo();

            _driver.FindElement(By.CssSelector("a.btn.btn-outline-danger[href='/Admins/Logout']")).Click();
            DelayForDemo();

        }

        private static void DelayForDemo()
        {
            Thread.Sleep(2000);
        }
    }
}
