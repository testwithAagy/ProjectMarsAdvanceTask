using NUnit.Framework;
using OpenQA.Selenium;
using ProjectMarsAutomationAdvanceTask.Drivers;
using ProjectMarsAutomationAdvanceTask.Helpers;
using ProjectMarsAutomationAdvanceTask.Reporting;
using System;
using System.IO;

namespace ProjectMarsAutomationAdvanceTask.Tests.Base
{
    public class BaseTest
    {
        protected IWebDriver Driver;
        protected AventStack.ExtentReports.ExtentTest Test;

        private static readonly object _reportLock = new object();

        [OneTimeSetUp]
        public void GlobalSetup()
        {
            
            ExtentReportManager.GetExtent();
        }

        [SetUp]
        public void Setup()
        {
            
            Driver = BrowserManager.InitializeDriver();
           
            LoginHelper.LoginAsTestUser(Driver);
          
            Test = ExtentReportManager.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [TearDown]
        public void TearDown()
        {
            try
            {
                
                if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
                {
                    var screenshot = ((ITakesScreenshot)Driver).GetScreenshot();
                    string reportFolder = Path.Combine(Directory.GetCurrentDirectory(), "Reports");
                    string screenshotFolder = Path.Combine(reportFolder, "Screenshots");
                    Directory.CreateDirectory(screenshotFolder);

                    string screenshotPath = Path.Combine(screenshotFolder, $"Screenshot_{DateTime.Now.Ticks}.png");
                    screenshot.SaveAsFile(screenshotPath); 

                    
                    string relativePath = Path.Combine("Screenshots", Path.GetFileName(screenshotPath));

                    lock (_reportLock)
                    {
                        Test.Fail("Test Failed",
                            AventStack.ExtentReports.MediaEntityBuilder
                                .CreateScreenCaptureFromPath(relativePath)
                                .Build()
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Screenshot capture failed: {ex.Message}");
            }
            finally
            {
                
                Driver?.Quit();
                Driver?.Dispose(); 
                BrowserManager.QuitDriver();
               
                ExtentReportManager.FlushReport();
            }
        }

        [OneTimeTearDown]
        public void GlobalTearDown()
        {
            
            ExtentReportManager.FlushReport();
        }
    }
}
