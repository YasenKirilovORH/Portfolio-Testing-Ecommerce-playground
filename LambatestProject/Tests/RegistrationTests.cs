using LambatestProject.Pages;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LambatestProject.Tests
{
    public class RegistrationTests : BaseTest
    {
        [Test, Order(1)]
        public void RegisterWithoutSelectingCheckbox()
        {
            // Arrange
            var user = GenerateNewUser();

            // Act
            registrationPage.PerformRegistrationWitouthCheckOnPrivacyPolicy(user.FirstName, user.LastName, user.Email, user.Telephone, user.Password, user.ConfirmPassword);

            // Assert
            Assert.That(registrationPage.GetPrivacyPolicyErrorMessage, Is.EqualTo("Warning: You must agree to the Privacy Policy!"), "Expected error message did not show");
        }

        [Test, Order(2)]
        public void RegisterWithEmptyFirstNameField()
        {
            // Arrange
            var user = GenerateNewUser(firstName: "");

            // Act
            registrationPage.PerformRegistrationWithCheckOnPrivacyPolicy(user.FirstName, user.LastName, user.Email, user.Telephone, user.Password, user.ConfirmPassword);

            // Assert
            Assert.That(registrationPage.GetFieldsErrorMessages, Is.EqualTo("First Name must be between 1 and 32 characters!"), "Account registration should not be successful with empty first name");
        }

        [Test, Order(3)]
        public void RegisterWithEmptyLastNameField()
        {
            // Arrange
            var user = GenerateNewUser(lastName: "");

            // Act
            registrationPage.PerformRegistrationWithCheckOnPrivacyPolicy(user.FirstName, user.LastName, user.Email, user.Telephone, user.Password, user.ConfirmPassword);

            // Assert
            Assert.That(registrationPage.GetFieldsErrorMessages, Is.EqualTo("Last Name must be between 1 and 32 characters!"), "Account registration should not be successful with empty last name");
        }

        [Test, Order(4)]
        public void RegistrationWithEmptyEmailField()
        {
            // Arrange
            var user = GenerateNewUser(email: "");

            // Act
            registrationPage.PerformRegistrationWithCheckOnPrivacyPolicy(user.FirstName, user.LastName, user.Email, user.Telephone, user.Password, user.ConfirmPassword);

            // Assert
            Assert.That(registrationPage.GetFieldsErrorMessages, Is.EqualTo("E-Mail Address does not appear to be valid!"), "Account registration should not be successful with empty email");
        }

        [Test, Order(5)]
        public void RegistrationWithEmptyTelephoneField()
        {
            // Arrange
            var user = GenerateNewUser(telephone: "");

            // Act
            registrationPage.PerformRegistrationWithCheckOnPrivacyPolicy(user.FirstName, user.LastName, user.Email, user.Telephone, user.Password, user.ConfirmPassword);

            // Assert
            Assert.That(registrationPage.GetFieldsErrorMessages, Is.EqualTo("Telephone must be between 3 and 32 characters!"), "Account registration should not be successful with empty phone number");
        }

        [Test, Order(6)]
        public void RegistrationWithEmptyPasswordField()
        {
            // Arrange
            var user = GenerateNewUser(password: "");

            // Act
            registrationPage.PerformRegistrationWithCheckOnPrivacyPolicy(user.FirstName, user.LastName, user.Email, user.Telephone, user.Password, user.ConfirmPassword);

            // Assert
            Assert.That(registrationPage.GetFieldsErrorMessages, Is.EqualTo("Password must be between 4 and 20 characters!"), "Account registration should not be successful with empty password");
        }

        [Test, Order(7)]
        public void RegistrationWithEmptyConfirmPasswordField()
        {
            // Arrange
            var user = GenerateNewUser(confirmPassword: "");

            // Act
            registrationPage.PerformRegistrationWithCheckOnPrivacyPolicy(user.FirstName, user.LastName, user.Email, user.Telephone, user.Password, user.ConfirmPassword);

            // Assert
            Assert.That(registrationPage.GetFieldsErrorMessages, Is.EqualTo("Password confirmation does not match password!"), "Account registration should not be successful with empty confirm password");
        }

        [Test, Order(8)]
        public void Valid_RegisterWithSelectingCheckbox()
        {
            // Arrange
            var user = GenerateNewUser();

            // Act
            registrationPage.PerformRegistrationWithCheckOnPrivacyPolicy(user.FirstName, user.LastName, user.Email, user.Telephone, user.Password, user.ConfirmPassword);

            // Assert
            Assert.That(registrationPage.GetConfirmationMessage, Is.EqualTo("Your Account Has Been Created!"), "Expected confirmation message did not show");
        }

        // Edge cases tests

        [Test, Order(9)]
        public void RegisterWithFirstNameWithOneSymbol()
        {
            // Arrange
            var user = GenerateNewUser(firstName: "A");

            // Act
            registrationPage.PerformRegistrationWithCheckOnPrivacyPolicy(user.FirstName, user.LastName, user.Email, user.Telephone, user.Password, user.ConfirmPassword);

            // Assert
            Assert.That(registrationPage.GetConfirmationMessage, Is.EqualTo("Your Account Has Been Created!"), "Expected confirmation message did not show");
        }

        [Test, Order(10)]
        public void RegisterWithFirstNameWith32Symbols()
        {
            // Arrange
            var user = GenerateNewUser(firstName: "Asdfeasdfredsaqwertgfgtrefgtreed");

            // Act
            registrationPage.PerformRegistrationWithCheckOnPrivacyPolicy(user.FirstName, user.LastName, user.Email, user.Telephone, user.Password, user.ConfirmPassword);

            // Assert
            Assert.That(registrationPage.GetConfirmationMessage, Is.EqualTo("Your Account Has Been Created!"), "Expected confirmation message did not show");
        }

        [Test, Order(11)]
        public void RegisterWithFirstNameWith33Symbols()
        {
            // Arrange
            var user = GenerateNewUser(firstName: "Asdfeasdfredsaqwertgfgtrefgtreeda");

            // Act
            registrationPage.PerformRegistrationWithCheckOnPrivacyPolicy(user.FirstName, user.LastName, user.Email, user.Telephone, user.Password, user.ConfirmPassword);

            // Assert
            Assert.That(registrationPage.GetFieldsErrorMessages, Is.EqualTo("First Name must be between 1 and 32 characters!"), "Account registration should not be successful with empty first name");
        }

        [Test, Order(12)]
        public void RegisterWithLastNameWithOneSymbol()
        {
            // Arrange
            var user = GenerateNewUser(firstName: "A");

            // Act
            registrationPage.PerformRegistrationWithCheckOnPrivacyPolicy(user.FirstName, user.LastName, user.Email, user.Telephone, user.Password, user.ConfirmPassword);

            // Assert
            Assert.That(registrationPage.GetConfirmationMessage, Is.EqualTo("Your Account Has Been Created!"), "Expected confirmation message did not show");
        }

        [Test, Order(13)]
        public void RegisterWithLastNameWith32Symbols()
        {
            // Arrange
            var user = GenerateNewUser(lastName: "Asdfeasdfredsaqwertgfgtrefgtreed");

            // Act
            registrationPage.PerformRegistrationWithCheckOnPrivacyPolicy(user.FirstName, user.LastName, user.Email, user.Telephone, user.Password, user.ConfirmPassword);

            // Assert
            Assert.That(registrationPage.GetConfirmationMessage, Is.EqualTo("Your Account Has Been Created!"), "Expected confirmation message did not show");
        }

        [Test, Order(14)]
        public void RegisterWithLastNameWith33Symbols()
        {
            // Arrange
            var user = GenerateNewUser(lastName: "Asdfeasdfredsaqwertgfgtrefgtreeda");

            // Act
            registrationPage.PerformRegistrationWithCheckOnPrivacyPolicy(user.FirstName, user.LastName, user.Email, user.Telephone, user.Password, user.ConfirmPassword);

            // Assert
            Assert.That(registrationPage.GetFieldsErrorMessages, Is.EqualTo("Last Name must be between 1 and 32 characters!"), "Account registration should not be successful with empty first name");
        }

        // No specified email parameters to test it for edge cases

        [Test, Order(15)]
        public void RegisterWithPasswordWithThreeSymbols()
        {
            // Arrange
            var user = GenerateNewUser(password: "123");

            // Act
            registrationPage.PerformRegistrationWithCheckOnPrivacyPolicy(user.FirstName, user.LastName, user.Email, user.Telephone, user.Password, user.ConfirmPassword);

            // Assert
            Assert.That(registrationPage.GetFieldsErrorMessages, Is.EqualTo("Password must be between 4 and 20 characters!"), "Account registration should not be successful with password bellow 4 symbols");
        }

        [Test, Order(16)]
        public void RegisterWithPasswordWithFourSymbols()
        {
            // Arrange
            var user = GenerateNewUser(password: "1234");

            // Act
            registrationPage.PerformRegistrationWithCheckOnPrivacyPolicy(user.FirstName, user.LastName, user.Email, user.Telephone, user.Password, user.ConfirmPassword);

            // Assert
            Assert.That(registrationPage.GetConfirmationMessage, Is.EqualTo("Your Account Has Been Created!"), "Expected confirmation message did not show");
        }

        [Test, Order(17)]
        public void RegisterWithPasswordWith20Symbols()
        {
            // Arrange
            var user = GenerateNewUser(password: "12345123451234512345");

            // Act
            registrationPage.PerformRegistrationWithCheckOnPrivacyPolicy(user.FirstName, user.LastName, user.Email, user.Telephone, user.Password, user.ConfirmPassword);

            // Assert
            Assert.That(registrationPage.GetConfirmationMessage, Is.EqualTo("Your Account Has Been Created!"), "Expected confirmation message did not show");
        }

        // BUG FOUND: IT ACCEPTS PASSWORD WITH 21 SYMBOLS AND IT SHOULD NOT
        [Test, Order(18)]
        public void RegisterWithPasswordWith21Symbols()
        {
            // Arrange
            var user = GenerateNewUser(password: "123451234512345123451");

            // Act
            registrationPage.PerformRegistrationWithCheckOnPrivacyPolicy(user.FirstName, user.LastName, user.Email, user.Telephone, user.Password, user.ConfirmPassword);

            // Assert
            Assert.That(registrationPage.GetFieldsErrorMessages, Is.EqualTo("Password must be between 4 and 20 characters!"), "Account registration should not be successful with password bellow 4 symbols");
        }

        [Test, Order(19)]
        public void RegisterWithTelephonNumberWithTwoSymbols()
        {
            // Arrange
            var user = GenerateNewUser(telephone: "12");

            // Act
            registrationPage.PerformRegistrationWithCheckOnPrivacyPolicy(user.FirstName, user.LastName, user.Email, user.Telephone, user.Password, user.ConfirmPassword);

            // Assert
            Assert.That(registrationPage.GetFieldsErrorMessages, Is.EqualTo("Telephone must be between 3 and 32 characters!"), "Account registration should not be successful with empty phone number");
        }

        [Test, Order(20)]
        public void RegisterWithTelephonNumberWithThreeSymbols()
        {
            // Arrange
            var user = GenerateNewUser(telephone: "123");

            // Act
            registrationPage.PerformRegistrationWithCheckOnPrivacyPolicy(user.FirstName, user.LastName, user.Email, user.Telephone, user.Password, user.ConfirmPassword);

            // Assert
            Assert.That(registrationPage.GetConfirmationMessage, Is.EqualTo("Your Account Has Been Created!"), "Expected confirmation message did not show");
        }
        [Test, Order(21)]
        public void RegisterWithTelephonNumberWith32Symbols()
        {
            // Arrange
            var user = GenerateNewUser(telephone: "12345123451234512345123451234512");

            // Act
            registrationPage.PerformRegistrationWithCheckOnPrivacyPolicy(user.FirstName, user.LastName, user.Email, user.Telephone, user.Password, user.ConfirmPassword);

            // Assert
            Assert.That(registrationPage.GetConfirmationMessage, Is.EqualTo("Your Account Has Been Created!"), "Expected confirmation message did not show");
        }

        [Test, Order(22)]
        public void RegisterWithTelephonNumberWith33Symbols()
        {
            // Arrange
            var user = GenerateNewUser(telephone: "123451234512345123451234512345123");

            // Act
            registrationPage.PerformRegistrationWithCheckOnPrivacyPolicy(user.FirstName, user.LastName, user.Email, user.Telephone, user.Password, user.ConfirmPassword);

            // Assert
            Assert.That(registrationPage.GetFieldsErrorMessages, Is.EqualTo("Telephone must be between 3 and 32 characters!"), "Account registration should not be successful with empty phone number");
        }

        [Test, Order(23)]
        public void RegisterWithMismatchingPasswords()
        {
            // Arrange
            var user = GenerateNewUser(password: "123456", confirmPassword: "12345");

            // Act
            registrationPage.PerformRegistrationWithCheckOnPrivacyPolicy(user.FirstName, user.LastName, user.Email, user.Telephone, user.Password, user.ConfirmPassword);

            // Assert
            Assert.That(registrationPage.GetFieldsErrorMessages, Is.EqualTo("Password confirmation does not match password!"), "Passwords should match");
        }
    }
}
