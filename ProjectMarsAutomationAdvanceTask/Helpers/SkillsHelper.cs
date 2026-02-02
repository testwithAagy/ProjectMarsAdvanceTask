using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Linq;

namespace ProjectMarsAutomationAdvanceTask.Helpers
{
    public static class SkillHelper
    {
        
        public static void CleanupAllTestSkills(IWebDriver driver)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            try
            {
                
                var tab = wait.Until(ExpectedConditions.ElementToBeClickable(
                    By.XPath("//a[@data-tab='second' and text()='Skills']")));
                tab.Click();

                while (true)
                {
                    
                    var table = wait.Until(ExpectedConditions.ElementIsVisible(
                        By.XPath("//div[@data-tab='second']//table")));

                    
                    var row = table.FindElements(By.XPath(".//tbody//tr")).FirstOrDefault();
                    if (row == null)
                        break; 

                    
                    var deleteIcon = row.FindElement(By.XPath(".//i[contains(@class,'remove icon')]"));
                    deleteIcon.Click();

                    
                    wait.Until(ExpectedConditions.StalenessOf(row));
                }

                Console.WriteLine("Global cleanup complete: all skills deleted.");
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine("No skills found for global cleanup.");
            }
        }

        
        public static void CleanupScenarioSkill(IWebDriver driver, string skill)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            try
            {
                
                var tab = wait.Until(ExpectedConditions.ElementToBeClickable(
                    By.XPath("//a[@data-tab='second' and text()='Skills']")));
                tab.Click();

                
                var table = wait.Until(ExpectedConditions.ElementIsVisible(
                    By.XPath("//div[@data-tab='second']//table")));

                var rows = table.FindElements(By.XPath(".//tbody//tr"));

                
                var targetRow = rows.FirstOrDefault(row =>
                    row.Text.IndexOf(skill, StringComparison.OrdinalIgnoreCase) >= 0);

                if (targetRow != null)
                {
                    var deleteIcon = targetRow.FindElement(By.XPath(".//i[contains(@class,'remove icon')]"));
                    deleteIcon.Click();
                    wait.Until(ExpectedConditions.StalenessOf(targetRow));

                    Console.WriteLine($"Scenario cleanup: '{skill}' deleted successfully.");
                }
                else
                {
                    Console.WriteLine($"No matching row found for '{skill}', skipping deletion.");
                }
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine($"Timeout while cleaning up skill '{skill}'.");
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine($"Delete icon not found for '{skill}'.");
            }
        }
    }
}
