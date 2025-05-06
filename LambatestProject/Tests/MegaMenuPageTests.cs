using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace LambatestProject.Tests
{
    public class MegaMenuPageTests : BaseTest
    {
        [SetUp]
        public void SetUp()
        {
            loginPage.PerformLogin("asd@abv.bg", "123456");
        }

        // BUG FOUND: WHEN YOU SELECT THE 'NOKIA' CATEGORY IT REDIRECTS YOU TO A PAGE WITH TITLE CANNON. THERE ARE A LOT OF SUCH BUGS LIKE THIS ONE FOR DIFFERENT CATTEGORIES
        [Test]
        public void SelectValidCategory()
        {
            // Act
            megaMenuPage.SelectCategoryFromMegaMenu(3);

            // Assert
            Assert.That(megaMenuPage.CategoryNameHeader.Text, Is.EqualTo("Nokia"), "Expected category header should be Nokia");
        }

        [Test]
        public void SelectValidCategory_NoMismatchInExpectedAndActualTitle()
        {
            // Act
            megaMenuPage.SelectCategoryFromMegaMenu(0);

            // Assert
            Assert.That(megaMenuPage.CategoryNameHeader.Text, Is.EqualTo("Apple"), "Expected category header should be Apple");
        }

        [Test]
        [TestCase(-1)]
        [TestCase(28)]
        public void SelectCategoryOutOfRange_Invalid(int index)
        {
            // Arrange and Act
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                megaMenuPage.SelectCategoryFromMegaMenu(index);
            });

            // Assert
            Assert.That(ex.Message, Does.Contain("Index is out of range"));
        }

        [Test]
        public void FilterProductsByPrice_ValidValues()
        {
            // Arrange and Act
            megaMenuPage.FilerByPrice(1, 1500, 2000);

            // Assert
            var productPrices = megaMenuPage.GetAllDisplayedProductsPrices();

            foreach (var price in productPrices)
            {
                Assert.That(price, Is.GreaterThanOrEqualTo(1500).And.LessThanOrEqualTo(2000), "Filtered items are not in the correct price range");
            }
        }

        [Test]
        public void FilterProductsByPrice_InvalidValues_MinValueToBeGreaterThanMaxValue()
        {
            // Arrange and Act
            megaMenuPage.FilerByPrice(0, 2000, 1000);

            // Assert
            var productPrices = megaMenuPage.GetAllDisplayedProductsPrices();
            
            Assert.That(productPrices.Count, Is.EqualTo(0), "List of items should be empty");
        }

        [Test]
        public void FilterProductByPrice_MaxValueToBeLessThanTheCheapestProduct()
        {
            // Arrange and Act
            megaMenuPage.FilerByPrice(3, 0, 100);

            // Assert
            var productPrices = megaMenuPage.GetAllDisplayedProductsPrices();

            Assert.That(productPrices.Count, Is.EqualTo(0), "List of items should be epmpty if price range is bellow the price of the cheapest product");
        }

        [Test]
        public void FilterProductByPrice_MinAndMaxPricesToBeZero()
        {
            // Arrange and Act
            megaMenuPage.FilerByPrice(3, 0, 0);

            // Assert
            var productPrices = megaMenuPage.GetAllDisplayedProductsPrices();

            // PROBLEM DETECTED WITH THE UI OF THE WEB PAGE. IF I FILTER WITH TWO ZERO INPUTS IT RETURNS ALL PRODUCTS IF I DO A MANUAL TEST. IN THIS TEST RETURNS 0 PRODUCTS
            Console.WriteLine(productPrices.Count);

            Assert.That(productPrices.Count, Is.EqualTo(0), "List of items should be epmpty if price range is bellow the price of the cheapest product");
        }

        [Test]
        public void FilterProductsInStock()
        {
            // Arrange
            megaMenuPage.SelectCategoryFromMegaMenu(0);
          
            var expectedCountText = megaMenuPage.InStockCount.Text;
            int expectedCount = int.Parse(expectedCountText);
            
            // Act
            megaMenuPage.ClickInStockCheckbox();

            // Assert
            int actualCount = megaMenuPage.GetTotalFilteredProducts();
            Assert.That(actualCount, Is.EqualTo(expectedCount));
        }
    }   
}
