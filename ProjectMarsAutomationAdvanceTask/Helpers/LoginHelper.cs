using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace ProjectMarsAutomationAdvanceTask.Helpers
{
    public static class LoginHelper
    {
        
        private static readonly string Email = "newuser2@test.com";
        private static readonly string Password = "Pass@123";
        private static readonly string BaseUrl = "http://localhost:5003";

        
        public static void LoginAsTestUser(IWebDriver driver)
        {
            if (driver == null) throw new ArgumentNullException(nameof(driver));

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));

            driver.Navigate().GoToUrl(BaseUrl);

            try
            {
                
                var signInButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[text()='Sign In']")));
                signInButton.Click();

                
                wait.Until(ExpectedConditions.ElementIsVisible(By.Name("email")));
                var emailInput = driver.FindElement(By.Name("email"));
                var passwordInput = driver.FindElement(By.Name("password"));

                
                emailInput.Clear();
                emailInput.SendKeys(Email);
                passwordInput.Clear();
                passwordInput.SendKeys(Password);

                
                var loginButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[text()='Login']")));
                loginButton.Click();

                
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//a[contains(text(),'Languages')]")));

                Console.WriteLine("Logged in successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Login failed: {ex.Message}");
                throw;
            }
        }

        
        public static void SignOut(IWebDriver driver)
        {
            if (driver == null) return;

            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

               
                var signOutButton = wait.Until(ExpectedConditions.ElementToBeClickable(
                    By.XPath("//a[@class='item']/button[text()='Sign Out']")));
                signOutButton.Click();

                
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//a[text()='Sign In']")));

                Console.WriteLine("User signed out successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SignOut failed: {ex.Message}");
            }
        }
    }
}
