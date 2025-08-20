using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace InfraEdgeAutomationTest.Pages
{
    public class WikipediaPage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        public WikipediaPage(IWebDriver driver)
        {
            this.driver = driver;
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        private By TddNavLink => By.CssSelector("a.vector-toc-link[href='#Test-driven_development']");
        private By ContentBlock => By.XPath("//h3[@id='Test-driven_development']/ancestor::div/following-sibling::p[1]");

        public void ScrollToTddSection()
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(TddNavLink));
            driver.FindElement(TddNavLink).Click();
        }

        public string GetPageTitle()
        {
            return driver.Title;
        }

        public string GetTddSectionText()
        {
            ScrollToTddSection();
            wait.Until(ExpectedConditions.ElementIsVisible(ContentBlock));
            return driver.FindElement(ContentBlock).Text;
        }
    }
}
