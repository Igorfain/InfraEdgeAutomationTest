# InfraEdgeAutomationTest

## ✅ Overview
This project is an automated test suite written in **C#** using **Selenium**, **NUnit**, **RestSharp**, and **Allure**.

It performs:
- 🧪 **UI Testing**: Extracts and analyzes the "Test-driven development" section from the Wikipedia page for "Test automation".
- 🌐 **API Testing**: Retrieves the same section via Wikipedia API.
- 🔍 **Validation**: Compares the unique word counts between UI and API results.

---

## 📂 Structure

```
InfraEdgeAutomationTest/
├── Api/                   → REST API clients (RestSharp)
├── Base/                  → Base test classes
├── Config/                → Configuration loader and JSON file
├── Pages/                 → Selenium Page Objects (POM)
├── Steps/                 → Test logic steps (optional layer)
├── Tests/                 → NUnit test classes (UI, API, Compare)
├── Utils/                 → Utility methods (e.g., text normalization)
├── allureConfig.json      → Allure configuration
├── MainConfig.json        → Base/API URLs
├── WikipediaUiTestCase.md → Test case documentation
```

---

## ⚙️ Requirements

- [.NET 8.0+](https://dotnet.microsoft.com/en-us/download)
- [Visual Studio 2022+](https://visualstudio.microsoft.com/)

### 📦 NuGet Dependencies:
- `Selenium.WebDriver`
- `Selenium.Support`
- `NUnit`
- `NUnit3TestAdapter`
- `Microsoft.NET.Test.Sdk`
- `RestSharp`
- `Newtonsoft.Json`
- `Allure.NUnit`

---


## ▶️ Running Tests

1. Open project in Visual Studio
2. Run `WikipediaUiTest`, `WikipediaApiTest`, or `CompareUiApiTest`
3. Optionally, generate Allure report from `allure-results`

---
📄 [Wikipedia UI Test Case](WikipediaUiTestCase.md)

---

## 📝 Notes

- Content may vary on Wikipedia - test validates word count, not exact text
- Text normalization removes brackets, punctuation, and is case-insensitive
