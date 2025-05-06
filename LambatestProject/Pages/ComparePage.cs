using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LambatestProject.Pages
{
    public class ComparePage : BasePage
    {
        public ComparePage(IWebDriver driver) : base(driver)
        {
            
        }
        public IWebElement ProductComparisonTitlte => driver.FindElement(By.XPath("//h1[@class='h4']"));
        public IReadOnlyCollection<IWebElement> ProductImages => driver.FindElements(By.XPath("//tr//td[@class='text-center']"));

        public void NavigateToComparePage()
        {
            CompareButton.Click();
            WaitUntilElementIsVisible(By.XPath("//h1[@class='h4']"));
        }
    }
}
