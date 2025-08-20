using Newtonsoft.Json.Linq;
using RestSharp;
using System.Text.RegularExpressions;

namespace InfraEdgeAutomationTest.Api
{
    public class WikipediaApiClient
    {
        private readonly string apiUrl;
        private readonly RestClient client;

        private const string DEFAULT_PAGE_TITLE = "Test_automation";
        private const string TDD_SECTION_PATTERN = @"==\s*Test-driven development\s*==([\s\S]+?)(==|$)";

        public WikipediaApiClient(string apiUrl)
        {
            this.apiUrl = apiUrl ?? throw new ArgumentNullException(nameof(apiUrl));
            client = new RestClient(apiUrl);
        }

        /// <summary>
        /// Gets the Test-driven development section text from Wikipedia
        /// </summary>
        /// <param name="pageTitle">Wikipedia page title (defaults to "Test_automation")</param>
        /// <returns>Combined title and section text</returns>
        public string GetTddSectionText(string pageTitle = DEFAULT_PAGE_TITLE)
        {
            try
            {
                string fullPageText = GetPageExtract(pageTitle);
                string sectionText = ExtractTddSection(fullPageText);

                return $"{pageTitle.Replace("_", " ")} {sectionText}";
            }
            catch (Exception ex)
            {
                throw new WikipediaApiException($"Failed to get TDD section text for page '{pageTitle}': {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Gets the full page extract from Wikipedia API
        /// </summary>
        private string GetPageExtract(string pageTitle)
        {
            var request = CreateWikipediaRequest(pageTitle);
            var response = client.Get(request);

            if (!response.IsSuccessful)
            {
                throw new WikipediaApiException($"API call failed with status: {response.StatusCode}. Error: {response.ErrorMessage}");
            }

            if (string.IsNullOrEmpty(response.Content))
            {
                throw new WikipediaApiException("API returned empty response");
            }

            return ParsePageContent(response.Content, pageTitle);
        }

        /// <summary>
        /// Creates the Wikipedia API request
        /// </summary>
        private RestRequest CreateWikipediaRequest(string pageTitle)
        {
            return new RestRequest()
                .AddParameter("action", "query")
                .AddParameter("format", "json")
                .AddParameter("prop", "extracts")
                .AddParameter("explaintext", true)
                .AddParameter("titles", pageTitle);
        }

        /// <summary>
        /// Parses the JSON response to extract page content
        /// </summary>
        private string ParsePageContent(string jsonContent, string pageTitle)
        {
            try
            {
                var json = JObject.Parse(jsonContent);
                var pages = json["query"]?["pages"];

                if (pages == null)
                {
                    throw new WikipediaApiException("Invalid API response format: 'pages' not found");
                }

                var firstPage = pages.First;
                if (firstPage == null)
                {
                    throw new WikipediaApiException($"Page '{pageTitle}' not found in API response");
                }

                var extract = firstPage.First?["extract"]?.ToString();
                if (string.IsNullOrEmpty(extract))
                {
                    throw new WikipediaApiException($"No content found for page '{pageTitle}'");
                }

                return extract;
            }
            catch (Exception ex) when (!(ex is WikipediaApiException))
            {
                throw new WikipediaApiException($"Failed to parse API response: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Extracts the Test-driven development section using regex
        /// </summary>
        private string ExtractTddSection(string fullText)
        {
            var match = Regex.Match(fullText, TDD_SECTION_PATTERN, RegexOptions.IgnoreCase);

            if (!match.Success)
            {
                throw new WikipediaApiException("Section 'Test-driven development' not found in the page content");
            }

            string sectionContent = match.Groups[1].Value.Trim();

            if (string.IsNullOrEmpty(sectionContent))
            {
                throw new WikipediaApiException("Test-driven development section found but content is empty");
            }

            return sectionContent;
        }

        /// <summary>
        /// Cleanup resources
        /// </summary>
        public void Dispose()
        {
            client?.Dispose();
        }
    }

    /// <summary>
    /// Custom exception for Wikipedia API operations
    /// </summary>
    public class WikipediaApiException : Exception
    {
        public WikipediaApiException(string message) : base(message) { }
        public WikipediaApiException(string message, Exception innerException) : base(message, innerException) { }
    }
}