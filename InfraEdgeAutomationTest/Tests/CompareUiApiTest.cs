using InfraEdgeAutomationTest.Base;
using InfraEdgeAutomationTest.Steps;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;

namespace InfraEdgeAutomationTest.Tests
{
    [AllureNUnit]
    [TestFixture]
    public class CompareUiApiTest : BaseTest
    {
        [Test(Description = "Compare unique word count from UI and API")]
        [AllureTag("COMPARE")]
        public void CompareUiAndApiWordCounts()
        {
            new CompareSteps(driver).CompareUiVsApi();
        }
    }
}
