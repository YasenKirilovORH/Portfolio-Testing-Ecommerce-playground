using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Interactions;

namespace LambatestProject.Pages
{
    public class RegistrationPage : BasePage
    {
        public RegistrationPage(IWebDriver driver) : base(driver)
        {

        }
        public IWebElement FirstNameInputField => driver.FindElement(By.XPath("//input[@id='input-firstname']"));
        public IWebElement LastNameInputField => driver.FindElement(By.XPath("//input[@id='input-lastname']"));
        public IWebElement EmailInputField => driver.FindElement(By.XPath("//input[@id='input-email']"));
        public IWebElement TelephoneInputField => driver.FindElement(By.XPath("//input[@id='input-telephone']"));
        public IWebElement PasswordInputField => driver.FindElement(By.XPath("//input[@id='input-password']"));
        public IWebElement PasswordConfirmInputField => driver.FindElement(By.XPath("//input[@id='input-confirm']"));
        public IWebElement ConfirmPolicyAgreementCheckbox => driver.FindElement(By.XPath("//div[@class='custom-control custom-checkbox custom-control-inline']"));
        public IWebElement ContinueButton => driver.FindElement(By.XPath("//input[@value='Continue']"));
        public IWebElement ErrorMessageForPrivacyPolicy => wait.Until(driver => driver.FindElement(By.XPath("//div[@class='alert alert-danger alert-dismissible']")));
        public IWebElement ConfirmationMessageForAccountCreation => wait.Until(driver => driver.FindElement(By.XPath("//h1")));
        public IWebElement ErrorMessageForInvalidValues => wait.Until(driver => driver.FindElement(By.XPath("//div[@class='text-danger']")));

        public void PerformRegistrationWithCheckOnPrivacyPolicy(string firstName, string lastName, string email, string telephone, string password, string confirmPassword)
        {
            Actions actions = new Actions(driver);
            actions.MoveToElement(MyAccountButton).Perform();

            WaitUntilElementIsVisible(By.XPath("//a[@href='https://ecommerce-playground.lambdatest.io/index.php?route=account/register']"));

            RegisterButton.Click();

            WaitUntilElementIsClickable(By.XPath("//input[@value='Continue']"));

            FirstNameInputField.SendKeys(firstName);
            LastNameInputField.SendKeys(lastName);
            EmailInputField.SendKeys(email);
            TelephoneInputField.SendKeys(telephone);
            PasswordInputField.SendKeys(password);
            PasswordConfirmInputField.SendKeys(confirmPassword);

            if (!ConfirmPolicyAgreementCheckbox.Selected)
            {
                ConfirmPolicyAgreementCheckbox.Click();
            }

            ContinueButton.Click();
        }

        public void PerformRegistrationWitouthCheckOnPrivacyPolicy(string firstName, string lastName, string email, string telephone, string password, string confirmPassword)
        {
            Actions actions = new Actions(driver);
            actions.MoveToElement(MyAccountButton).Perform();

            WaitUntilElementIsVisible(By.XPath("//a[@href='https://ecommerce-playground.lambdatest.io/index.php?route=account/register']"));
            
            RegisterButton.Click();

            WaitUntilElementIsClickable(By.XPath("//input[@value='Continue']"));

            FirstNameInputField.SendKeys(firstName);
            LastNameInputField.SendKeys(lastName);
            EmailInputField.SendKeys(email);
            TelephoneInputField.SendKeys(telephone);
            PasswordInputField.SendKeys(password);
            PasswordConfirmInputField.SendKeys(confirmPassword);

            if (ConfirmPolicyAgreementCheckbox.Selected)
            {
                ConfirmPolicyAgreementCheckbox.Click();
            }

            ContinueButton.Click();
        }

        public string GetPrivacyPolicyErrorMessage()
        {
            try
            {
                return ErrorMessageForPrivacyPolicy.Text;
            }
            catch(NoSuchElementException)
            {
                return "Privacy Policy Error message not found.";
            }
        }

        public string GetConfirmationMessage()
        {
            try
            {
                return ConfirmationMessageForAccountCreation.Text.Trim();
            }
            catch(NoSuchElementException)
            {
                return "Confirmation message was not found";
            }
        }
        public string GetFieldsErrorMessages()
        {
            try
            {
                return ErrorMessageForInvalidValues.Text.Trim();
            }
            catch(NoSuchElementException)
            {
                return "Field error messages not found.";
            }
        }
    }
}
