using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Xml.Linq;

namespace TestProject2
{
    public class Tests
    {

        public static IWebDriver driver;
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);

        }

        [Test]
        public void Test1()
        {
            driver.Url = "https://www.t-mobile.com/tablets";
            driver.Manage().Window.Maximize();
            selectFilters("Deals", "New", "Special offer");
            selectFilters("Brands", "all");
            selectFilters("Operating System", "Android", "iPadOS", "Other");
        }

        public static void selectFilters(string fieldSet, params string[] filters)
        {
            Thread.Sleep(2000);
            IWebElement ele = driver.FindElement(By.XPath($"//mat-expansion-panel-header//legend[contains(text(), '{fieldSet}')]"));
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20)); 
            wait.Until(ExpectedConditions.ElementToBeClickable(ele)); 
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView({behavior: 'smooth', block: 'center'});", ele);
            ele.Click();
            
            if(filters[0] == "all")
            {
                IList<IWebElement> filterElements = ele.FindElements(By.XPath($"//div[@aria-label='{fieldSet}']//mat-checkbox//span[@class='filter-display-name']"));
                foreach(IWebElement element in filterElements)
                {
                    element.Click();
                }
            }
            else
            {
                foreach (string str in filters)
                {
                    ele.FindElement(By.XPath($"//div[@aria-label='{fieldSet}']//mat-checkbox//span[contains(text(),'{str}')]")).Click();
                }
            }
        }
    }
}