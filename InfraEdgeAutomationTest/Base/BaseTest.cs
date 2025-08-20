using Allure.Commons;
using InfraEdgeAutomationTest.Config;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace InfraEdgeAutomationTest.Base
{
    [TestFixture]
    public abstract class BaseTest
    {
        protected IWebDriver driver;
        protected WebDriverWait wait;
        protected MainConfig config;

        protected virtual bool RequiresBrowser => true;

        [SetUp]
        public void SetUp()
        {
            config = MainConfig.Load();

            if (RequiresBrowser)
            {
                InitializeBrowser();
                OpenDefaultPage();
            }
        }

        private void InitializeBrowser()
        {
            var options = new ChromeOptions();
#if DEBUG
            options.AddArgument("--start-maximized");
#else
            options.AddArgument("--headless");
#endif
            driver = new ChromeDriver(options);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        protected virtual void OpenDefaultPage()
        {
            if (RequiresBrowser && driver != null)
            {
                driver.Navigate().GoToUrl(config.baseUrl);
            }
        }

        [TearDown]
        public void TearDown()
        {
            if (RequiresBrowser && driver != null)
            {
                if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
                {
                    var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                    var screenshotPath = Path.Combine(Path.GetTempPath(), $"{TestContext.CurrentContext.Test.Name}_{DateTime.Now:yyyyMMdd_HHmmss}.png");
                    screenshot.SaveAsFile(screenshotPath);
                    AllureLifecycle.Instance.AddAttachment("Screenshot", "image/png", screenshotPath);
                }

                driver.Quit();
                driver.Dispose();
            }
        }
    }
}