using InfraEdgeAutomationTest.Api;
using InfraEdgeAutomationTest.Utils;
using NUnit.Allure.Attributes;

namespace InfraEdgeAutomationTest.Steps
{
    public class WikipediaApiSteps
    {
        private readonly WikipediaApiClient apiClient;

        public WikipediaApiSteps(string apiUrl)
        {
            apiClient = new WikipediaApiClient(apiUrl);
        }

        [AllureStep("Validate unique word count from Wikipedia API")]
        public Dictionary<string, int> ValidateUniqueWordsFromApi()
        {
            string sectionText = apiClient.GetTddSectionText();

            if (string.IsNullOrEmpty(sectionText))
            {
                Assert.Fail("Section text is empty or null");
                return null;
            }

            Dictionary<string, int> wordCounts = TextProcessor.Process(sectionText);

            TestContext.WriteLine($"Unique word count: {wordCounts.Count}");
            foreach (var pair in wordCounts)
                TestContext.WriteLine($"{pair.Key}: {pair.Value}");

            Assert.That(wordCounts.Count, Is.GreaterThan(0), "No words were counted in API text.");

            return wordCounts;
        }

        [AllureStep("Get TDD section text from Wikipedia API")]
        public string GetTddSectionText()
        {
            return apiClient.GetTddSectionText();
        }
    }
}