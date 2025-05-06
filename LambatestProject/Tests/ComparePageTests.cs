using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LambatestProject.Tests
{
    public class ComparePageTests : BaseTest
    {
        [SetUp]
        public void SetUp()
        {
            loginPage.PerformLogin("asd@abv.bg", "123456");
        }

        [Test]
        public void CompareTwoValidProducts()
        {
            // Arrange and Act
            megaMenuPage.AddItemToBeCompared(0, 1, 3);
            comparePage.NavigateToComparePage();

            // Assert
            Assert.That(comparePage.ProductComparisonTitlte.Text, Is.EqualTo("Product Comparison"), "Redirection to comparison page was not successful");

            Assert.That(comparePage.ProductImages.Count, Is.EqualTo(2), "Items count is not as expected.");
        }
    }
}
