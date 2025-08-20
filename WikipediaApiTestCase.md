# Test Case: Validate Unique Word Count from Wikipedia API

**ID**: TC_API_001  
**Title**: Extract and count unique words from "Test-driven development" section via Wikipedia API  

**Preconditions**:  
- Internet connection is active  
- Wikipedia API endpoint is reachable  
- Proper request parameters are known  

**Steps**:
1. Send a `GET` request to Wikipedia API:
2. Extract the full text of the page from the `extract` field
3. Locate the "Test-driven development" section
4. Normalize text: lowercase, remove brackets, punctuation, etc.
5. Split into words and count unique ones
6. Print each word and its count to console/log

**Expected Result**:
- Number of unique words is greater than 0  
- No exceptions thrown during parsing