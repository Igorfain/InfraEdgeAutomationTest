using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V137.Storage;
using OpenQA.Selenium.Support.UI;
//using SeleniumExtras.WaitHelpers;
using System;

namespace automationexerciseTests.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver driver;

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
        }

      //  private IWebElement EmailInput => driver.FindElement(By.CssSelector("[data-qa='login-email']"));


        public void NavigateToHome(string baseUrl)
        {
            driver.Navigate().GoToUrl(baseUrl);
        }

       
    }
}
