using OpenQA.Selenium;

namespace InfraEdgeAutomationTest.Pages
{
    public abstract class BasePage
    {
        protected readonly IWebDriver driver;

        protected BasePage(IWebDriver driver)
        {
            this.driver = driver;
        }
    }
}
