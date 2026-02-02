using NUnit.Framework;
using OpenQA.Selenium;
using ProjectMarsAutomationAdvanceTask.Drivers;
using ProjectMarsAutomationAdvanceTask.Helpers;
using ProjectMarsAutomationAdvanceTask.Reporting;
using System;
using System.Collections.Generic;
using System.IO;

namespace ProjectMarsAutomationAdvanceTask.Tests
{
    public static class ScenarioTracking
    {
        public static Dictionary<string, string> ScenarioLanguages = new Dictionary<string, string>();

        public static Dictionary<string, string> ScenarioSkills = new Dictionary<string, string>(); 
    }

    [TestFixture] 
    public class Hooks
    {
        private static readonly object _reportLock = new object();

        [SetUp]
        public void BeforeScenario()
        {
            
            BrowserManager.InitializeDriver();
            var driver = BrowserManager.GetDriver();

            if (driver == null)
                Assert.Fail("Driver not initialized.");

            
            LoginHelper.LoginAsTestUser(driver);

            
            if (TestContext.CurrentContext.Test.Name.Contains("Language"))
            {
                try
                {
                    LanguageHelper.DeleteAllLanguages(driver);
                    Console.WriteLine("All languages deleted before running this test.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Language cleanup before test failed: {ex.Message}");
                }
            }



            
            if (TestContext.CurrentContext.Test.Name.Contains("Skill"))
            {
                try
                {
                    SkillHelper.CleanupAllTestSkills(driver);
                    Console.WriteLine("All skills deleted before running this test.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Skill cleanup before test failed: {ex.Message}");
                }
            }



            ExtentReportManager.GetExtent(); 
           
            ExtentReportManager.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [TearDown]
        public void AfterScenario()
        {
            var driver = BrowserManager.GetDriver();
            var test = ExtentReportManager.GetTest();

            
            if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                try
                {
                    var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                    string screenshotFolder = Path.Combine(AppContext.BaseDirectory, "Reports", "Screenshots");
                    Directory.CreateDirectory(screenshotFolder);

                    string screenshotPath = Path.Combine(screenshotFolder, $"Screenshot_{DateTime.Now.Ticks}.png");
                    screenshot.SaveAsFile(screenshotPath);

                    lock (_reportLock)
                    {
                        test.Fail("Test Failed",
                            AventStack.ExtentReports.MediaEntityBuilder
                                .CreateScreenCaptureFromPath(screenshotPath)
                                .Build()
                        );
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Screenshot capture failed: {ex.Message}");
                }
            }

           
            string testName = TestContext.CurrentContext.Test.Name;
            if (ScenarioTracking.ScenarioLanguages.ContainsKey(testName))
            {
                try
                {
                    string language = ScenarioTracking.ScenarioLanguages[testName];
                    LanguageHelper.DeleteLanguage(driver, language);
                    ScenarioTracking.ScenarioLanguages.Remove(testName);
                    Console.WriteLine($"Scenario language '{language}' deleted after test '{testName}'.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Scenario-specific language cleanup failed: {ex.Message}");
                }
            }



            
            if (ScenarioTracking.ScenarioSkills.ContainsKey(testName))
            {
                try
                {
                    string skill = ScenarioTracking.ScenarioSkills[testName];
                    SkillHelper.CleanupScenarioSkill(driver, skill);
                    ScenarioTracking.ScenarioSkills.Remove(testName);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Scenario-specific skill cleanup failed: {ex.Message}");
                }
            }

           
            BrowserManager.QuitDriver();
            ExtentReportManager.FlushReport();
        }
    }
}
