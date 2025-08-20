using Newtonsoft.Json;
using System.IO;

namespace InfraEdgeAutomationTest.Config
{
    public class MainConfig
    {
        public string baseUrl { get; set; }
        public string apiUrl { get; set; }

        public static MainConfig Load()
        {
            var path = Path.Combine(TestContext.CurrentContext.TestDirectory, "Config", "MainConfig.json");
            return JsonConvert.DeserializeObject<MainConfig>(File.ReadAllText(path));
        }
    }
}
