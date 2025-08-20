using InfraEdgeAutomationTest.Api;
using InfraEdgeAutomationTest.Config;
using InfraEdgeAutomationTest.Pages;
using InfraEdgeAutomationTest.Utils;
using NUnit.Allure.Attributes;
using OpenQA.Selenium;

namespace InfraEdgeAutomationTest.Steps
{
    public class CompareSteps
    {
        private readonly WikipediaPage page;
        private readonly WikipediaApiClient api;

        public CompareSteps(IWebDriver driver)
        {
            page = new WikipediaPage(driver);
            api = new WikipediaApiClient(MainConfig.Load().apiUrl);
        }

        [AllureStep("Compare unique word counts from UI and API")]
        public void CompareUiVsApi()
        {
            string uiRaw = $"{page.GetPageTitle()} {page.GetTddSectionText()}";
            var uiWords = TextProcessor.Process(uiRaw);

            string apiRaw = api.GetTddSectionText();
            var apiWords = TextProcessor.Process(apiRaw);

            TestContext.WriteLine($"UI word count: {uiWords.Count}");
            TestContext.WriteLine($"API word count: {apiWords.Count}");

            Assert.That(Math.Abs(uiWords.Count - apiWords.Count), Is.LessThanOrEqualTo(1),
                $"Mismatch in unique word counts: UI={uiWords.Count}, API={apiWords.Count}");
        }
    }
}
