using InfraEdgeAutomationTest.Base;
using InfraEdgeAutomationTest.Steps;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;

namespace InfraEdgeAutomationTest.Tests
{
    [AllureNUnit]
    [TestFixture]
    public class WikipediaUiTest : BaseTest
    {
        [Test(Description = "Validate unique word count from Wikipedia UI")]
        [AllureTag("UI")]
        [AllureTag("TC_UI_001")]
        public void ValidateUiWordCount()
        {
            new WikipediaSteps(driver).ValidateUniqueWordsFromUi();
        }
    }
}
