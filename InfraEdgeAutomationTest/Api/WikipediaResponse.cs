using Newtonsoft.Json.Linq;


namespace InfraEdgeAutomationTest.Api
{
    /// <summary>
    /// Represents a Wikipedia API response and provides methods to extract data
    /// </summary>
    public class WikipediaResponse
    {
        private readonly JObject responseData;
        private readonly string pageTitle;

        public WikipediaResponse(string jsonResponse, string pageTitle = "Test_automation")
        {
            if (string.IsNullOrEmpty(jsonResponse))
                throw new ArgumentException("JSON response cannot be null or empty", nameof(jsonResponse));

            this.pageTitle = pageTitle ?? throw new ArgumentNullException(nameof(pageTitle));

            try
            {
                responseData = JObject.Parse(jsonResponse);
            }
            catch (Exception ex)
            {
                throw new WikipediaApiException($"Failed to parse JSON response: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Gets the full page extract from the response
        /// </summary>
        public string GetPageExtract()
        {
            try
            {
                var pages = responseData["query"]?["pages"];
                if (pages == null)
                    throw new WikipediaApiException("Invalid response format: 'pages' not found");

                var firstPage = pages.First;
                if (firstPage == null)
                    throw new WikipediaApiException($"Page '{pageTitle}' not found in response");

                var extract = firstPage.First?["extract"]?.ToString();
                if (string.IsNullOrEmpty(extract))
                    throw new WikipediaApiException($"No content found for page '{pageTitle}'");

                return extract;
            }
            catch (Exception ex) when (!(ex is WikipediaApiException))
            {
                throw new WikipediaApiException($"Failed to extract page content: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Gets all section headers from the page
        /// </summary>
        public List<string> GetSectionHeaders()
        {
            string extract = GetPageExtract();
            var headerPattern = @"==\s*([^=]+)\s*==";
            var matches = System.Text.RegularExpressions.Regex.Matches(extract, headerPattern);

            return matches.Cast<System.Text.RegularExpressions.Match>()
                         .Select(m => m.Groups[1].Value.Trim())
                         .ToList();
        }

        /// <summary>
        /// Gets a specific section by header name
        /// </summary>
        public string GetSection(string sectionHeader)
        {
            if (string.IsNullOrEmpty(sectionHeader))
                throw new ArgumentException("Section header cannot be null or empty", nameof(sectionHeader));

            string extract = GetPageExtract();
            string pattern = $@"==\s*{System.Text.RegularExpressions.Regex.Escape(sectionHeader)}\s*==([\s\S]+?)(==|$)";
            var match = System.Text.RegularExpressions.Regex.Match(extract, pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            if (!match.Success)
                throw new WikipediaApiException($"Section '{sectionHeader}' not found in page content");

            return match.Groups[1].Value.Trim();
        }

        /// <summary>
        /// Gets the Test-driven development section specifically
        /// </summary>
        public string GetTddSectionText()
        {
            try
            {
                string sectionText = GetSection("Test-driven development");
                string formattedTitle = pageTitle.Replace("_", " ");
                return $"{formattedTitle} {sectionText}";
            }
            catch (WikipediaApiException)
            {
               
                throw;
            }
            catch (Exception ex)
            {
                throw new WikipediaApiException($"Failed to get TDD section: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Gets the page title from the response
        /// </summary>
        public string GetPageTitle()
        {
            try
            {
                var pages = responseData["query"]?["pages"];
                if (pages == null)
                    return pageTitle.Replace("_", " ");

                var firstPage = pages.First;
                var title = firstPage.First?["title"]?.ToString();

                return !string.IsNullOrEmpty(title) ? title : pageTitle.Replace("_", " ");
            }
            catch
            {
                return pageTitle.Replace("_", " ");
            }
        }

        /// <summary>
        /// Checks if a specific section exists in the page
        /// </summary>
        public bool HasSection(string sectionHeader)
        {
            try
            {
                GetSection(sectionHeader);
                return true;
            }
            catch (WikipediaApiException)
            {
                return false;
            }
        }

        /// <summary>
        /// Gets basic page information
        /// </summary>
        public WikipediaPageInfo GetPageInfo()
        {
            return new WikipediaPageInfo
            {
                Title = GetPageTitle(),
                HasContent = !string.IsNullOrEmpty(GetPageExtract()),
                SectionCount = GetSectionHeaders().Count,
                AvailableSections = GetSectionHeaders()
            };
        }
    }

    /// <summary>
    /// Contains basic information about a Wikipedia page
    /// </summary>
    public class WikipediaPageInfo
    {
        public string Title { get; set; }
        public bool HasContent { get; set; }
        public int SectionCount { get; set; }
        public List<string> AvailableSections { get; set; }
    }
}