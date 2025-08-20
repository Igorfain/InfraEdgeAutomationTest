using OpenQA.Selenium;


namespace automationexerciseTests.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver driver;

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
       
        }


        public void NavigateToHome(string baseUrl)
        {
            driver.Navigate().GoToUrl(baseUrl);
        }

       
    }
}
