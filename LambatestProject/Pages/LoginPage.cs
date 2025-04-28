using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LambatestProject.Pages
{
    public class LoginPage : BasePage
    {
        public LoginPage(IWebDriver driver) : base(driver)
        {
            
        }

        public IWebElement EmailLoginField => driver.FindElement(By.XPath("//input[@id='input-email']"));
        public IWebElement PasswordLoginField => driver.FindElement(By.XPath("//input[@id='input-password']"));
        public IWebElement SubmitLoginButton => driver.FindElement(By.XPath("//input[@value='Login']"));
        public IWebElement ForgottenPasswordLink => driver.FindElement(By.XPath("//a[text()='Forgotten Password']"));
        public IWebElement ErrorMessageForLoginAndResetPassword => driver.FindElement(By.XPath("//div[@class='alert alert-danger alert-dismissible']"));
        public IWebElement MyAccountHeader => driver.FindElement(By.XPath("//h2[text()='My Account']"));
        public IWebElement ResetPasswordEmailField => driver.FindElement(By.XPath("//input[@id='input-email']"));
        public IWebElement ResetPasswordSubmitButton => driver.FindElement(By.XPath("//button[text()='Continue']"));
        public IWebElement ConfirmationMessageForPasswordReset =>  driver.FindElement(By.XPath("//div[@class='alert alert-success alert-dismissible']"));
        public void PerformLogin(string email, string password)
        {
            Actions actions = new Actions(driver);
            actions.MoveToElement(MyAccountButton).Perform();

            WaitUntilElementIsVisible(By.XPath("//a[@href='https://ecommerce-playground.lambdatest.io/index.php?route=account/login']"));

            LoginButton.Click();

            WaitUntilElementIsVisible(By.XPath("//input[@value='Login']"));

            EmailLoginField.SendKeys(email);
            PasswordLoginField.SendKeys(password);

            WaitUntilElementIsClickable(By.XPath("//input[@value='Login']"));
            SubmitLoginButton.Click();
        }

        public void UseForgottenPasswordOption(string email)
        {
            Actions actions = new Actions(driver);
            actions.MoveToElement(MyAccountButton).Perform();

            WaitUntilElementIsVisible(By.XPath("//a[@href='https://ecommerce-playground.lambdatest.io/index.php?route=account/login']"));

            LoginButton.Click();

            WaitUntilElementIsVisible(By.XPath("//input[@value='Login']"));

            WaitUntilElementIsClickable(By.XPath("//a[text()='Forgotten Password']"));
            ForgottenPasswordLink.Click();

            ResetPasswordEmailField.SendKeys(email);

            WaitUntilElementIsVisible(By.XPath("//button[text()='Continue']"));

            ResetPasswordSubmitButton.Click();
        }
    }
}
