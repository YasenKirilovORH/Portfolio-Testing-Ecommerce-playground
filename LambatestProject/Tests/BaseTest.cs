using LambatestProject.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V133.FedCm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LambatestProject.Tests
{
    public class BaseTest
    {
        protected IWebDriver driver;
        protected BasePage basePage;
        protected RegistrationPage registrationPage;
        protected LoginPage loginPage;
        protected Random random;


        [SetUp]
        public void Setup()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddUserProfilePreference("profile.password_manager_enabled", false);
            driver = new ChromeDriver(chromeOptions);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            random = new Random();
            basePage = new BasePage(driver);
            registrationPage = new RegistrationPage(driver);
            loginPage = new LoginPage(driver);
            basePage.GoToHomePage();
        }
        public class UserModel
        {
            public string FirstName;
            public string LastName;
            public string Email;
            public string Telephone;
            public string Password;
            public string ConfirmPassword;
        }
        public UserModel GenerateNewUser(string? firstName = null, string? lastName = null, string? email = null, string? telephone = null, string? password = null, string? confirmPassword = null)
        {

            var generatedPassword = password ?? "!Pass" + random.Next(1, 999999999);

            var user = new UserModel
            {
                FirstName = firstName ?? "first" + random.Next(1, 9999999),
                LastName = lastName ?? "last" + random.Next(1, 9999999),
                Email = email ?? "randomEmail" + random.Next(1, 9999999) + "@gmail.com",
                Telephone = telephone ?? "+359" + random.Next(100000000, 999999999),
                Password = generatedPassword,
                ConfirmPassword = confirmPassword ?? generatedPassword
            };

            return user;
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
