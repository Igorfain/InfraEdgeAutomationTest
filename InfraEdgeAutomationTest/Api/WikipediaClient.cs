using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Text.RegularExpressions;

namespace InfraEdgeAutomationTest.Api
{
    public class WikipediaApiClient
    {
        private readonly string apiUrl;

        public WikipediaApiClient(string apiUrl)
        {
            this.apiUrl = apiUrl;
        }

        public string GetTddSectionText()
        {
            var client = new RestClient(apiUrl);

            var request = new RestRequest()
                .AddParameter("action", "query")
                .AddParameter("format", "json")
                .AddParameter("prop", "extracts")
                .AddParameter("explaintext", true)
                .AddParameter("titles", "Test_automation");

            var response = client.Get(request);

            if (!response.IsSuccessful)
                throw new Exception("API call failed");

            var json = JObject.Parse(response.Content);

            var pages = json["query"]["pages"];
            var firstPage = pages.First;
            var extract = firstPage.First["extract"]?.ToString();

            // Найдём секцию "Test-driven development"
            var match = Regex.Match(extract, @"==\s*Test-driven development\s*==([\s\S]+?)(==|$)");

            if (!match.Success)
                throw new Exception("Section 'Test-driven development' not found");

            string section = match.Groups[1].Value;
            string title = "Test automation";

            return $"{title} {section}";
        }
    }
}
