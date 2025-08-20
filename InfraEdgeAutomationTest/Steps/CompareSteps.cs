using automationexerciseTests.Api;
using InfraEdgeAutomationTest.Api;
using InfraEdgeAutomationTest.Pages;
using InfraEdgeAutomationTest.Utils;
using NUnit.Allure.Attributes;
using OpenQA.Selenium;

namespace InfraEdgeAutomationTest.Steps
{
    public class CompareSteps
    {
        private readonly WikipediaPage page;
        private readonly WikipediaApiClient apiClient;

        public CompareSteps(IWebDriver driver)
        {
            page = new WikipediaPage(driver);
            apiClient = new WikipediaApiClient(Endpoints.BaseApiUrl);
        }

        [AllureStep("Compare unique word counts from UI and API")]
        public void CompareUiVsApi()
        {
            var uiWords = GetUiWords();
            var apiWords = GetApiWords();

            int diff = Math.Abs(uiWords.Count - apiWords.Count);
            TestContext.WriteLine($"UI: {uiWords.Count}, API: {apiWords.Count}, Diff: {diff}");

            Assert.That(diff, Is.LessThanOrEqualTo(1),
                $"Word count mismatch: UI={uiWords.Count}, API={apiWords.Count}");
        }

        private Dictionary<string, int> GetUiWords()
        {
            page.ScrollToTddSection();
            string text = $"{page.GetPageTitle()} {page.GetTddSectionText()}";
            return TextProcessor.Process(text);
        }

        private Dictionary<string, int> GetApiWords()
        {
            return TextProcessor.Process(apiClient.GetTddSectionText());
        }
    }
}