using InfraEdgeAutomationTest.Api;
using InfraEdgeAutomationTest.Base;
using InfraEdgeAutomationTest.Utils;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System.Collections.Generic;

namespace InfraEdgeAutomationTest.Tests
{
    [AllureNUnit]
    [TestFixture]
    public class WikipediaApiTest : BaseTest
    {
        private Dictionary<string, int> apiWordCounts;

        [Test(Description = "Extract and count words from Wikipedia API")]
        [AllureTag("API")]
        public void CountWords_FromApi()
        {
            var apiClient = new WikipediaApiClient(config.apiUrl);

            string sectionText = apiClient.GetTddSectionText();

            apiWordCounts = TextProcessor.Process(sectionText);

            TestContext.WriteLine($"Unique word count: {apiWordCounts.Count}");
            foreach (var pair in apiWordCounts)
                TestContext.WriteLine($"{pair.Key}: {pair.Value}");

            Assert.That(apiWordCounts.Count, Is.GreaterThan(0), "No words were counted in API text.");

        }

        public Dictionary<string, int> GetApiWordCounts() => apiWordCounts;
    }
}
