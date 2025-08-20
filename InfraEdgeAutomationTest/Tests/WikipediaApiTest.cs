using automationexerciseTests.Api;
using InfraEdgeAutomationTest.Base;
using InfraEdgeAutomationTest.Steps;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;

namespace InfraEdgeAutomationTest.Tests
{
    [AllureNUnit]
    [TestFixture]
    public class WikipediaApiTest : BaseTest
    {
        protected override bool RequiresBrowser => false;

        private Dictionary<string, int> apiWordCounts;
        private WikipediaApiSteps apiSteps;

        [SetUp]
        public void Setup()
        {
            apiSteps = new WikipediaApiSteps(Endpoints.BaseApiUrl);
        }

        [Test(Description = "Extract and count words from Wikipedia API")]
        [AllureTag("API")]
        [AllureTag("TC_API_001")]
        public void CountWords_FromApi()
        {
            apiWordCounts = apiSteps.ValidateUniqueWordsFromApi();
        }

        public Dictionary<string, int> GetApiWordCounts() => apiWordCounts;
    }
}