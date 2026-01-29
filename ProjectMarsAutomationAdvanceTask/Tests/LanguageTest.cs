using NUnit.Framework;
using OpenQA.Selenium;
using ProjectMarsAutomationAdvanceTask.AssertHelpers;
using ProjectMarsAutomationAdvanceTask.Drivers;
using ProjectMarsAutomationAdvanceTask.Models;
using ProjectMarsAutomationAdvanceTask.Steps;
using ProjectMarsAutomationAdvanceTask.Utilities;
using ProjectMarsAutomationAdvanceTask.Tests; 
namespace ProjectMarsAutomationAdvanceTask.Tests
{
    [TestFixture]
    public class LanguageTests : Hooks  
    {
        private LanguageSteps _languageSteps;

        [SetUp]
        public void Setup()
        {
            IWebDriver driver = BrowserManager.GetDriver();
            if (driver == null)
                Assert.Fail("Driver is not initialized. Make sure Hooks.BeforeScenario runs before tests.");

            _languageSteps = new LanguageSteps(driver);
        }

        [Test]
        public void AddLanguage_Test()
        {
            var data = LanguagesDataReader.Read<LanguageTestData>("LanguageTestData.json", "AddLanguageInput");
            Assert.IsNotNull(data, "Test data is null. Check your JSON file and key.");

            string toastMessage = _languageSteps.AddLanguage(data.Language, data.Level);
            
            ProfileAssertHelper.AssertSuccessToast($"{data.Language} has been added to your languages", toastMessage);
            TestContext.WriteLine($"Language added Toast message verification passed");

            
            ScenarioTracking.ScenarioLanguages[TestContext.CurrentContext.Test.Name] = data.Language;
        }

        [Test]
        public void AddDuplicateLanguage_Test()
        {
            
            var data = LanguagesDataReader.Read<LanguageTestData>(
                "LanguageTestData.json",
                "AddLanguageInput");

            Assert.IsNotNull(data, "Test data is null.");

           
            string firstToast = _languageSteps.AddLanguage(data.Language, data.Level);

            Assert.That(firstToast,
                Does.Contain("has been added"),
                "First language add failed.");

            TestContext.WriteLine(
                $"Language '{data.Language}' added successfully the first time.");

            
            ScenarioTracking.ScenarioLanguages[
                TestContext.CurrentContext.Test.Name] = data.Language;

            
            string duplicateToast = _languageSteps.AddLanguage(data.Language, data.Level);

            
            Assert.That(duplicateToast,
                Does.Contain("already"),
                $"Duplicate language was not blocked. Actual message: '{duplicateToast}'");

            TestContext.WriteLine(
                $"Duplicate language '{data.Language}' correctly rejected.");
        }



        [Test]
        public void AddInvalidLanguage_Test()
        {
            var data = LanguagesDataReader.Read<LanguageTestData>(
                "LanguageTestData.json",
                "InvalidLanguageInput"
            );

            Assert.IsNotNull(data, "Invalid test data is null");

            
            string toastMessage = _languageSteps.AddLanguage(data.Language, data.Level);

            
            if (!string.IsNullOrEmpty(toastMessage))
            {
                Assert.That(
                    toastMessage,
                    Does.Not.Contain("has been added"),
                    $"Unexpected success toast shown: '{toastMessage}'"
                );
            }

            TestContext.WriteLine("Invalid language was correctly blocked.");
        }

        [Test]
        public void AddDestructiveLanguage_Test()
        {
            var data = LanguagesDataReader.Read<LanguageTestData>(
                "LanguageTestData.json",
                "DestructiveLanguageInput"
            );

            Assert.IsNotNull(data, "Destructive test data is null");

            
            string toastMessage = _languageSteps.AddLanguage(data.Language, data.Level);

                       
            if (!string.IsNullOrEmpty(toastMessage))
            {
                Assert.That(
                    toastMessage,
                    Does.Not.Contain("has been added"),
                    $"Unexpected success toast shown: '{toastMessage}'"
                );
            }

            TestContext.WriteLine("Destructive language input was correctly blocked.");
        }


        [Test]
        public void UpdateLanguage_Test()
        {
            var data = LanguagesDataReader.Read<LanguageTestData>("LanguageTestData.json", "UpdateLanguageInput");
            Assert.IsNotNull(data, "Test data is null. Check your JSON file and key.");

            
            string addToast = _languageSteps.AddLanguage(data.Language, data.Level);
            ProfileAssertHelper.AssertSuccessToast($"{data.Language} has been added to your languages", addToast);

            
            string updateToast = _languageSteps.UpdateLanguage(data.Language, data.UpdatedLanguage, data.UpdatedLevel);
            ProfileAssertHelper.AssertSuccessToast($"{data.UpdatedLanguage} has been updated to your languages", updateToast);
            TestContext.WriteLine($"Language Updated Toast message verification passed");
            
            ScenarioTracking.ScenarioLanguages[TestContext.CurrentContext.Test.Name] = data.UpdatedLanguage;
        }



        [Test]
        public void UpdateNonExistingLanguage_Test()
        {
            var data = LanguagesDataReader.Read<LanguageTestData>("LanguageTestData.json", "UpdateLanguageInput");
            Assert.IsNotNull(data, "Test data is null. Check your JSON file and key.");

            string nonExistingLanguage = "NonExistingLang";

            try
            {
                
                _languageSteps.UpdateLanguage(nonExistingLanguage, data.UpdatedLanguage, data.UpdatedLevel);

                TestContext.WriteLine($"WARNING: No exception thrown when updating non-existing language '{nonExistingLanguage}'");
                Assert.Fail("Expected exception for non-existing language, but none thrown.");
            }
            catch (Exception ex)
            {
                
                if (ex.Message.Contains("not found"))
                {
                    TestContext.WriteLine($"SUCCESS: UpdateNonExistingLanguage_Test passed. {ex.Message}");
                }
                else
                {
                    
                    throw;
                }
            }
        }

        [Test]
        public void UpdateWithInvalidLanguage_Test()
        {
            
            var data = LanguagesDataReader.Read<LanguageTestData>("LanguageTestData.json", "UpdateLanguageInvalidInput");
            Assert.IsNotNull(data, "Test data is null. Check your JSON file and key.");

            
            string addToast = _languageSteps.AddLanguage(data.Language, data.Level);
            ProfileAssertHelper.AssertSuccessToast($"{data.Language} has been added to your languages", addToast);
            TestContext.WriteLine($"Language '{data.Language}' added successfully for update test.");

            
            ScenarioTracking.ScenarioLanguages[TestContext.CurrentContext.Test.Name] = data.Language;

            
            string updateToast = _languageSteps.UpdateLanguage(data.Language, data.UpdatedLanguage, data.UpdatedLevel);

            
            Assert.IsTrue(updateToast != null && updateToast.Contains("Invalid", StringComparison.OrdinalIgnoreCase),
                $"Invalid language update was not blocked. Actual toast: '{updateToast}'");

            TestContext.WriteLine($"Invalid language update attempt blocked as expected. Toast: '{updateToast}'");
        }

        [Test]
        public void UpdateWithDestructiveInput_Test()
        {
            
            var data = LanguagesDataReader.Read<LanguageTestData>("LanguageTestData.json", "UpdateLanguageDestructiveInput");
            Assert.IsNotNull(data, "Test data is null. Check your JSON file and key.");

            
            string addToast = _languageSteps.AddLanguage(data.Language, data.Level);
            ProfileAssertHelper.AssertSuccessToast($"{data.Language} has been added to your languages", addToast);
            TestContext.WriteLine($"Language '{data.Language}' added successfully for destructive update test.");

            
            ScenarioTracking.ScenarioLanguages[TestContext.CurrentContext.Test.Name] = data.Language;

            
            string updateToast = _languageSteps.UpdateLanguage(data.Language, data.UpdatedLanguage, data.UpdatedLevel);

            
            Assert.IsTrue(updateToast != null && updateToast.Contains("Invalid", StringComparison.OrdinalIgnoreCase),
                $"Destructive language update was not blocked. Actual toast: '{updateToast}'");

            TestContext.WriteLine($"Destructive language update attempt blocked as expected. Toast: '{updateToast}'");
        }




        [Test]
        public void DeleteLanguage_Test()
        {
            
            var data = LanguagesDataReader.Read<LanguageTestData>(
                "LanguageTestData.json",
                "DeleteLanguageInput");

            Assert.IsNotNull(data, "Test data is null. Check JSON file.");

            
            string addToast = _languageSteps.AddLanguage(data.Language, data.Level);
            ProfileAssertHelper.AssertSuccessToast(
                $"{data.Language} has been added to your languages",
                addToast);

            TestContext.WriteLine($"Language '{data.Language}' added successfully.");

            
            ScenarioTracking.ScenarioLanguages[TestContext.CurrentContext.Test.Name] = data.Language;

            
            string deleteToast = _languageSteps.DeleteLanguage(data.Language);

            
            Assert.IsTrue(
                deleteToast != null &&
                deleteToast.Contains("deleted", StringComparison.OrdinalIgnoreCase),
                $"Language deletion failed. Actual toast: '{deleteToast}'");

            TestContext.WriteLine($"Language '{data.Language}' deleted successfully.");
        }



        [Test]
        public void DeleteNonExistingLanguage_Test()
        {
            
            var data = LanguagesDataReader.Read<LanguageTestData>(
                "LanguageTestData.json",
                "DeleteNonExistingLanguageInput");

            Assert.IsNotNull(data, "Test data is null. Check your JSON file and key.");

            
            string toastMessage = _languageSteps.DeleteLanguage(data.Language);

            
            Assert.IsTrue(string.IsNullOrEmpty(toastMessage),
                $"Expected no action for non-existing language, but got toast: '{toastMessage}'");

            TestContext.WriteLine($"Delete non-existing language test passed. Language '{data.Language}' was not found, no deletion performed.");
        }


    }
}
