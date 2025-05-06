using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LambatestProject.Pages
{
    public class MegaMenuPage : BasePage
    {
        public MegaMenuPage(IWebDriver driver) : base(driver)
        {

        }

        public IReadOnlyCollection<IWebElement> ProductCategoriesInMegaMenu => driver.FindElements(By.XPath("//div[@class='menu-items d-flex align-items-start']//ul[@class='nav flex-column vertical']//li[@class='nav-item']"));
        public IWebElement CategoryNameHeader => driver.FindElement(By.XPath("//h1[@class='h4']"));
        public IWebElement MinimumPriceField => driver.FindElement(By.XPath("(//div[@class='d-flex align-items-center']//input[@name='mz_fp[min]'])[2]"));
        public IWebElement MaximumPriceField => driver.FindElement(By.XPath("(//div[@class='d-flex align-items-center']//input[@name='mz_fp[max]'])[2]"));
        public IReadOnlyCollection<IWebElement> ProductPrice => driver.FindElements(By.XPath("//span[@class='price-new']"));
        public IWebElement InStockCheckbox => driver.FindElement(By.XPath("(//label[@class='custom-control-label'])[28]"));
        public IWebElement InStockCount => driver.FindElement(By.XPath("(//label[text()='In stock']//parent::div//following-sibling::span)[2]"));
        public IReadOnlyCollection<IWebElement> ProductElements => driver.FindElements(By.XPath("//div[@class='carousel-item active']"));
        public IWebElement AddButtonToCompare => driver.FindElement(By.XPath("//div[@id='entry_216844']//button[@title='Compare this Product']"));
        public IWebElement GetProductCategoryByIndex(int index)
        {
            var productCategories = ProductCategoriesInMegaMenu.ToList();

            if (index >= 0 && index < productCategories.Count)
            {
                return productCategories.ElementAt(index);
            }
            else
            {
                throw new ArgumentOutOfRangeException($"Invalid index: {index}. Index is out of range");
            }
        }

        public void SelectCategoryFromMegaMenu(int categoryIndex)
        {
            Actions actions = new Actions(driver);

            actions.MoveToElement(MegaMenuButton).Perform();

            WaitUntilElementIsVisible(By.XPath("//div[@id='entry281_216475']"));

            var categoryToSelect = GetProductCategoryByIndex(categoryIndex);

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(driver => categoryToSelect.Displayed && categoryToSelect.Enabled);

            categoryToSelect.Click();
        }

        public void FilerByPrice(int categoryIndex, double minPrice, double maxPrice)
        {
            Actions actions = new Actions(driver);

            actions.MoveToElement(MegaMenuButton).Perform();

            WaitUntilElementIsVisible(By.XPath("//div[@id='entry281_216475']"));

            var categoryToSelect = GetProductCategoryByIndex(categoryIndex);

            categoryToSelect.Click();

            WaitUntilElementIsVisible(By.XPath("//h3[text()='Filter']"));

            MinimumPriceField.Clear();
            MinimumPriceField.SendKeys(minPrice.ToString());

            MaximumPriceField.Clear();
            MaximumPriceField.SendKeys(maxPrice.ToString());

            MaximumPriceField.SendKeys(Keys.Enter);
        }

        public List<double> GetAllDisplayedProductsPrices()
        {
            var priceElement = ProductPrice;
            var prices = new List<double>();

            foreach (var element in priceElement)
            {
                var priceText = element.Text.Replace("$", "").Trim();

                if (double.TryParse(priceText, out double price))
                {
                    prices.Add(price);
                }
            }
            return prices;
        }
        public void ClickInStockCheckbox()
        {
            WaitUntilElementIsVisible(By.XPath("//h3[text()='Filter']"));
            InStockCheckbox.Click();

            Thread.Sleep(5000); 
        }
        public int GetTotalFilteredProducts()
        {


            int totalCount = 0;

            while (true)
            {
                var productsOnPage = ProductElements.Count;
                totalCount += productsOnPage;

                var nextButtons = driver.FindElements(By.XPath("//a[text()='>']"));

                if (nextButtons.Count() == 0 || !nextButtons[0].Displayed || !nextButtons[0].Enabled)
                {
                    break;
                }
                Actions actions = new Actions(driver);
                actions.MoveToElement(nextButtons[0]).Click().Perform();

                WaitUntilElementIsVisible(By.XPath("//div[@class='carousel-item active']"));
            }

            return totalCount;
        }

        public void AddItemToBeCompared(int categoryIndex, int firstItem, int secondItem)
        {
            SelectCategoryFromMegaMenu(categoryIndex);

            int[] productsToAddForCompare = { firstItem, secondItem };

            foreach(int product in productsToAddForCompare)
            {
                if(product < 1 || product > ProductElements.Count)
                {
                    Console.WriteLine($"Invalid product index: {product}");
                    continue;
                }

                IWebElement productToClick = ProductElements.ElementAt(product - 1);
                productToClick.Click();

                try
                {
                    IWebElement compareButton = WaitUntilElementIsClickable(By.XPath("//div[@id='entry_216844']//button[@title='Compare this Product']"));
                    Actions actions = new Actions(driver);
                    actions.MoveToElement(compareButton).Perform();
                    SafeClickOnElement(By.XPath("//div[@id='entry_216844']//button[@title='Compare this Product']"));

                }
                catch (WebDriverTimeoutException)
                {
                    Console.WriteLine("Compare button was not found.");
                }

                driver.Navigate().Back();
            }
        }
    }
}