using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using InfraEdgeAutomationTest.Config;

namespace InfraEdgeAutomationTest.Base
{
    [TestFixture]
    public class BaseTest
    {
        protected IWebDriver driver;
        protected MainConfig config;

        [SetUp]
        public void SetUp()
        {
            config = MainConfig.Load();
            var options = new ChromeOptions();
#if DEBUG
            options.AddArgument("--start-maximized");
#else
            options.AddArgument("--headless");
#endif
            driver = new ChromeDriver(options);

            OpenDefaultPage();
        }

        protected virtual void OpenDefaultPage()
        {
            driver.Navigate().GoToUrl(config.baseUrl);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
