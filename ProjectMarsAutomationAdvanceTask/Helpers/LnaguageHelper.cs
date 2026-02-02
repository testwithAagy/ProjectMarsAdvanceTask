using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Linq;

namespace ProjectMarsAutomationAdvanceTask.Helpers
{
    public static class LanguageHelper
    {
        
        public static void DeleteAllLanguages(IWebDriver driver)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            try
            {
                while (true)
                {
                    var rows = wait.Until(ExpectedConditions
                        .PresenceOfAllElementsLocatedBy(By.XPath("//div[@data-tab='first']//table/tbody/tr")));

                    if (rows.Count == 0)
                        break;

                    var deleteIcon = rows[0].FindElement(By.XPath(".//i[contains(@class,'remove icon')]"));
                    deleteIcon.Click();

                    wait.Until(ExpectedConditions.StalenessOf(rows[0]));
                }

                Console.WriteLine("SUCCESS: All languages deleted.");
            }
            catch
            {
                Console.WriteLine("INFO: No languages found for cleanup.");
            }
        }

        
        public static void DeleteLanguage(IWebDriver driver, string language)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            try
            {
                var rows = wait.Until(ExpectedConditions
                    .PresenceOfAllElementsLocatedBy(By.XPath("//div[@data-tab='first']//table/tbody/tr")));

                var targetRow = rows.FirstOrDefault(row =>
                    row.Text.Contains(language, StringComparison.OrdinalIgnoreCase));

                if (targetRow == null)
                {
                    Console.WriteLine($"INFO: '{language}' not found. Nothing to delete.");
                    return;
                }

                var deleteIcon = targetRow.FindElement(By.XPath(".//i[contains(@class,'remove icon')]"));
                deleteIcon.Click();

                wait.Until(ExpectedConditions.StalenessOf(targetRow));

                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR cleaning up language '{language}': {ex.Message}");
            }
        }
    }
}
