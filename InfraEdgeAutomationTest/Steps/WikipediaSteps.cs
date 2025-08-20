using InfraEdgeAutomationTest.Pages;
using InfraEdgeAutomationTest.Utils;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace InfraEdgeAutomationTest.Steps
{
    public class WikipediaSteps
    {
        private readonly WikipediaPage page;

        public WikipediaSteps(IWebDriver driver)
        {
            page = new WikipediaPage(driver);
        }

        [AllureStep("Validate unique word count from Wikipedia UI")]
        public void ValidateUniqueWordsFromUi()
        {
            page.ScrollToTddSection();

            string pageTitle = page.GetPageTitle();
            string sectionText = page.GetTddSectionText();
            string combinedText = $"{pageTitle} {sectionText}";

            Dictionary<string, int> wordCounts = TextProcessor.Process(combinedText);

            TestContext.WriteLine($"Unique word count: {wordCounts.Count}");
            foreach (var pair in wordCounts)
                TestContext.WriteLine($"{pair.Key}: {pair.Value}");

            Assert.That(wordCounts.Count, Is.GreaterThan(0), "No words were counted in UI text.");
        }
    }
}
