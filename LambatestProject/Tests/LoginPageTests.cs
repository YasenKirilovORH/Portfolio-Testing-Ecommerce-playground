using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LambatestProject.Tests
{

    public class LoginPageTests : BaseTest
    {
        // Use static details for login to avoid fails if tests are ran separately

        [Test, Order(1)]
        public void LoginWithValidDetails()
        {
            // Act
            loginPage.PerformLogin("asd@abv.bg", "123456");

            // Assert
            Assert.That(loginPage.MyAccountHeader.Text, Is.EqualTo("My Account"), "Login did not redirect to the correct page");
        }

        [Test, Order(2)]
        public void LoginWithInvalidPassword()
        {
            // Act
            loginPage.PerformLogin("asd@abv.bg", "1234567");

            // Assert
            Assert.That(loginPage.ErrorMessageForLoginAndResetPassword.Text, Is.EqualTo("Warning: No match for E-Mail Address and/or Password."), "Login was successful with invalid password");
        }

        [Test, Order(3)]
        public void LoginWithInvalidEmail()
        {
            // Act
            loginPage.PerformLogin("invalidemail@abv.bg", "123456");

            // Assert
            Assert.That(loginPage.ErrorMessageForLoginAndResetPassword.Text, Is.EqualTo("Warning: No match for E-Mail Address and/or Password."), "Login was successful with invalid email");
        }

        // BUG FOUND: ERROR MESSAGE IS NOT THE CORRECT ONE. IT TELLS THAT LOGIN ATTENPTS ARE EXCEEDED INSTEAD OF TELLING US THAT FILEDS ARE EMPTY OR INCORRECT
        [Test, Order(4)]
        public void LoginWithEmptyFields()
        {
            // Act
            loginPage.PerformLogin("", "");

            // Assert
            Assert.That(loginPage.ErrorMessageForLoginAndResetPassword.Text, Is.EqualTo("Warning: No match for E-Mail Address and/or Password."), "Login was successful with empty fields");
        }

        // BUG FOUND: ERROR MESSAGE IS NOT THE CORRECT ONE. IT TELLS THAT LOGIN ATTENPTS ARE EXCEEDED INSTEAD OF TELLING US THAT FILEDS ARE EMPTY OR INCORRECT
        [Test, Order(5)]
        public void LoginWithEmptyEmailField()
        {
            // Act
            loginPage.PerformLogin("", "123456");

            // Assert
            Assert.That(loginPage.ErrorMessageForLoginAndResetPassword.Text, Is.EqualTo("Warning: No match for E-Mail Address and/or Password."), "Login was successful with empty email");
        }

        [Test, Order(6)]
        public void LoginWithEmptyPasswordField()
        {
            // Act
            loginPage.PerformLogin("asd@abv.bg", "");

            // Assert
            
            Assert.That(loginPage.ErrorMessageForLoginAndResetPassword.Text, Is.EqualTo("Warning: No match for E-Mail Address and/or Password."), "Login was successful with empty password");
        }

        [Test, Order(7)]
        public void ResetPasswordWithValidEmail()
        {
            // Act
            loginPage.UseForgottenPasswordOption("asd@abv.bg");

            // Assert

            Assert.That(loginPage.ConfirmationMessageForPasswordReset.Text, Is.EqualTo("An email with a confirmation link has been sent your email address."), "Password reset was not successful with valid email");

            Assert.That(driver.Url.Contains("account/login"));
        }

        [Test, Order(8)]
        public void ResetPasswordWithInvalidEmail()
        {
            // Act
            loginPage.UseForgottenPasswordOption("invalidemail@abv.bg");

            // Assert
            Assert.That(loginPage.ErrorMessageForLoginAndResetPassword.Text, Is.EqualTo("Warning: The E-Mail Address was not found in our records, please try again!"), "Password reset was successful with invalid email");

            Assert.That(driver.Url.Contains("account/forgotten"));
        }
    }
}
