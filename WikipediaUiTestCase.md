# Test Case: Validate Unique Word Count from Wikipedia UI

**ID** (Tagged in Allure- Igor): TC_UI_001  
**Title**: Extract and count unique words from "Test-driven development" section on Wikipedia  
**Preconditions**:  
- Wikipedia is available and reachable  
- Browser is installed  
- ChromeDriver is accessible  

**Steps**:
1. Navigate to https://en.wikipedia.org/wiki/Test_automation  
2. Scroll to the “Test-driven development” section  
3. Extract all visible text and the page title  
4. Normalize text (lowercase, remove brackets/extra characters)  
5. Split into words and count unique ones  
6. Print each word and its count to console/log

**Expected Result**:
- Number of unique words is greater than 0  
- No exceptions thrown
