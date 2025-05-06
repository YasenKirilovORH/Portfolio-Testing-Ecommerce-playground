using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LambatestProject.Pages
{
    public class BasePage
    {
        protected IWebDriver driver;
        protected WebDriverWait wait;
        protected string BaseUrl = "https://ecommerce-playground.lambdatest.io/";

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        }

        public void GoToHomePage()
        {
            driver.Navigate().GoToUrl(BaseUrl);
        }

        public IWebElement LambadaTestHomeButton => driver.FindElement(By.XPath("//a[@title='Poco Electro']"));
        public IWebElement AllCategoriesDropdown => driver.FindElement(By.XPath("(//div[@class='dropdown search-category']//button[@class='btn dropdown-toggle'])[1]"));
        public IWebElement SearchField => driver.FindElement(By.XPath("//div[@class='flex-fill']//input[@data-autocomplete='5']"));
        public IWebElement SearchButton => driver.FindElement(By.XPath("//button[text()='Search']"));
        public IWebElement MegaMenuButton => driver.FindElement(By.XPath("//li[@class='nav-item dropdown dropdown-hoverable mega-menu position-static']//div[@class='info']//span[@class='title']"));
        public IWebElement CompareButton => driver.FindElement(By.XPath("//div[@id='entry_217823']"));
        public IWebElement WishListButton => driver.FindElement(By.XPath("//div[@id='entry_217824']"));
        public IWebElement CartButton => driver.FindElement(By.XPath("//div[@id='entry_217825']"));
        public IWebElement ShopByCategoryButton => driver.FindElement(By.XPath("//div[@id='entry_217832']//a[@href='#mz-component-1626147655']"));
        public IWebElement HomeButton => driver.FindElement(By.XPath("//ul[contains(@class, 'navbar-nav') and contains(@class, 'horizontal')]//li//a[contains(@class, 'icon-left') and contains(@class, 'both') and contains(@class, 'nav-link')]//span[normalize-space(text())='Home']"));
        public IWebElement MyAccountButton => wait.Until(driver => driver.FindElement(By.XPath("(//a[@class='icon-left both nav-link dropdown-toggle']//div[@class='info']//span)[3]")));
        public IWebElement LoginButton => driver.FindElement(By.XPath("//a[@href='https://ecommerce-playground.lambdatest.io/index.php?route=account/login']"));
        public IWebElement RegisterButton => driver.FindElement(By.XPath("//a[@href='https://ecommerce-playground.lambdatest.io/index.php?route=account/register']"));

        public IWebElement WaitUntilElementIsVisible(By locator)
        {
            return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
        }

        public IWebElement WaitUntilElementIsClickable(By locator)
        {
           return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(locator));
        }
        public void SafeClickOnElement(By locator, int timeoutSeconds = 20, int dellayAfterScrollInMs = 500)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));
                IWebElement element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(locator));

                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView({block: 'center'});", element);

                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", element);
            }
            catch(WebDriverTimeoutException)
            {
                Console.WriteLine($"Element with locator: {locator} was not clickable within the given time");
            }
        }
    }
}
