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
    public class SkillTests : Hooks
    {
        private SkillSteps _skillsSteps;

        [SetUp]
        public void Setup()
        {
            IWebDriver driver = BrowserManager.GetDriver();
            if (driver == null)
                Assert.Fail("Driver is not initialized. Make sure Hooks.BeforeScenario runs before tests.");

            _skillsSteps = new SkillSteps(driver);
        }

        [Test]
        public void AddSkill_Test()
        {
            var data = SkillsDataReader.Read<SkillTestData>("SkillTestData.json", "AddSkillInput");

            string toastMessage = _skillsSteps.AddSkill(data.Skill, data.Level);
            ProfileAssertHelper.AssertSuccessToast($"{data.Skill} has been added to your skills", toastMessage);

            TestContext.WriteLine($"Skill added Toast message verification passed");

            
            ScenarioTracking.ScenarioSkills[TestContext.CurrentContext.Test.Name] = data.Skill;
        }


        [Test]
        public void AddDuplicateSkill_Test()
        {
            var data = SkillsDataReader.Read<SkillTestData>("SkillTestData.json", "AddDuplicateSkillInput");

            
            string firstToastMessage = _skillsSteps.AddSkill(data.Skill, data.Level);
            ProfileAssertHelper.AssertSuccessToast($"{data.Skill} has been added to your skills", firstToastMessage);
            TestContext.WriteLine($"First skill addition verification passed");

           
            ScenarioTracking.ScenarioSkills[TestContext.CurrentContext.Test.Name] = data.Skill;

           
            string duplicateToastMessage = _skillsSteps.AddSkill(data.Skill, data.Level);

            
            ProfileAssertHelper.AssertSuccessToast($"This skill is already exist in your skill list.", duplicateToastMessage);
            TestContext.WriteLine($"Duplicate skill addition verification passed");
        }



        [Test]
        public void AddInvalidSkill_Test()
        {
            var data = SkillsDataReader.Read<SkillTestData>(
                "SkillTestData.json",
                "AddInvalidSkillInput"
            );

            
            string toastMessage = _skillsSteps.AddSkill(data.Skill, data.Level);

            
            Assert.That(
                string.IsNullOrEmpty(toastMessage)
                || toastMessage.ToLower().Contains("error")
                || toastMessage.ToLower().Contains("invalid"),
                "Invalid skill should not be added, but no validation error was shown."
            );

            TestContext.WriteLine("Invalid skill input was correctly blocked.");

            
            Assert.False(
                _skillsSteps.IsSkillPresent(data.Skill),
                "Invalid skill should not be present in the Skills table."
            );
        }





        [Test]
        public void AddDestructiveSkill_Test()
        {
            var data = SkillsDataReader.Read<SkillTestData>(
                "SkillTestData.json",
                "AddDestructiveSkillInput"
            );

            
            string toastMessage = _skillsSteps.AddSkill(data.Skill, data.Level);

            
            Assert.That(
                string.IsNullOrEmpty(toastMessage)
                || toastMessage.ToLower().Contains("error")
                || toastMessage.ToLower().Contains("invalid"),
                "Destructive skill input should not be added, but no validation error was shown."
            );

            TestContext.WriteLine("Destructive skill input was correctly blocked.");

            
            Assert.False(
                _skillsSteps.IsSkillPresent(data.Skill),
                "Destructive skill should not be present in the Skills table."
            );
        }




        [Test]
        public void UpdateSkill_Test()
        {
            var data = SkillsDataReader.Read<SkillTestData>(
                "SkillTestData.json",
                "UpdateSkillInput"
            );

            
            _skillsSteps.AddSkill(data.Skill, data.Level);

            
            ScenarioTracking.ScenarioSkills[TestContext.CurrentContext.Test.Name] = data.Skill;

            
            string toastMessage = _skillsSteps.UpdateSkill(
                data.Skill,
                data.UpdatedSkill,
                data.UpdatedLevel
            );

            
            ProfileAssertHelper.AssertSuccessToast(
                $"{data.UpdatedSkill} has been updated to your skills",
                toastMessage
            );

            TestContext.WriteLine("Skill updated successfully.");

            
            Assert.True(
                _skillsSteps.IsSkillPresent(data.UpdatedSkill),
                "Updated skill was not found in the Skills table."
            );

            
            ScenarioTracking.ScenarioSkills[TestContext.CurrentContext.Test.Name] = data.UpdatedSkill;
        }






        [Test]
        public void UpdateNonExistingSkill_Test()
        {
            var data = SkillsDataReader.Read<SkillTestData>(
                "SkillTestData.json",
                "UpdateSkillInput"   
            );

            try
            {
                _skillsSteps.UpdateSkill(
                    data.Skill,
                    data.UpdatedSkill,
                    data.UpdatedLevel
                );

                Assert.Fail("Update should not succeed for a non-existing skill.");
            }
            catch (Exception ex)
            {
                TestContext.WriteLine(
                    $"Expected behavior: unable to update non-existing skill. Reason: {ex.Message}"
                );
            }

            
            Assert.False(
                _skillsSteps.IsSkillPresent(data.Skill),
                "Original skill should not exist."
            );

            Assert.False(
                _skillsSteps.IsSkillPresent(data.UpdatedSkill),
                "Updated skill should NOT be created."
            );

            TestContext.WriteLine("Update non-existing skill test passed successfully.");
        }


        [Test]
        public void UpdateInvalidSkill_Test()
        {
            var data = SkillsDataReader.Read<SkillTestData>(
                "SkillTestData.json",
                "UpdateInvalidSkillInput"
            );

            
            string addToast = _skillsSteps.AddSkill(data.Skill, data.Level);
            ProfileAssertHelper.AssertSuccessToast(
                $"{data.Skill} has been added to your skills",
                addToast
            );

            
            ScenarioTracking.ScenarioSkills[TestContext.CurrentContext.Test.Name] = data.Skill;

            
            string updateToast = null;

            try
            {
                updateToast = _skillsSteps.UpdateSkill(
                    data.Skill,
                    data.UpdatedSkill,
                    data.UpdatedLevel
                );
            }
            catch
            {
                
            }

           
            Assert.That(
                string.IsNullOrEmpty(updateToast)
                || updateToast.ToLower().Contains("error")
                || updateToast.ToLower().Contains("invalid"),
                "Invalid skill update should not be allowed."
            );

            
            Assert.True(
                _skillsSteps.IsSkillPresent(data.Skill),
                "Original skill should remain unchanged after invalid update."
            );

            
            if (!string.IsNullOrEmpty(data.UpdatedSkill))
            {
                Assert.False(
                    _skillsSteps.IsSkillPresent(data.UpdatedSkill),
                    "Invalid updated skill should not be present."
                );
            }

            TestContext.WriteLine("UpdateInvalidSkill_Test passed successfully.");
        }


        [Test]
        public void UpdateDestructiveSkill_Test()
        {
            var data = SkillsDataReader.Read<SkillTestData>(
                "SkillTestData.json",
                "UpdateSkillDestructiveInput"
            );

            
            string addToast = _skillsSteps.AddSkill(data.Skill, data.Level);
            ProfileAssertHelper.AssertSuccessToast(
                $"{data.Skill} has been added to your skills",
                addToast
            );

            
            ScenarioTracking.ScenarioSkills[TestContext.CurrentContext.Test.Name] = data.Skill;

            string updateToast = null;

            
            try
            {
                updateToast = _skillsSteps.UpdateSkill(
                    data.Skill,
                    data.UpdatedSkill,
                    data.UpdatedLevel
                );
            }
            catch
            {
                
            }

            
            Assert.That(
                string.IsNullOrEmpty(updateToast)
                || updateToast.ToLower().Contains("error")
                || updateToast.ToLower().Contains("invalid"),
                "Destructive skill update should not succeed."
            );

            
            Assert.True(
                _skillsSteps.IsSkillPresent(data.Skill),
                "Original skill should remain after destructive update attempt."
            );

            
            Assert.False(
                _skillsSteps.IsSkillPresent(data.UpdatedSkill),
                "Destructive skill should not be added or updated."
            );

            TestContext.WriteLine("Destructive skill update correctly blocked.");
        }


        [Test]
        public void DeleteSkill_Test()
        {
            
            var data = SkillsDataReader.Read<SkillTestData>(
                "SkillTestData.json",
                "DeleteSkillInput"
            );

            string addToast = _skillsSteps.AddSkill(data.Skill, data.Level);

            ProfileAssertHelper.AssertSuccessToast(
                $"{data.Skill} has been added to your skills",
                addToast
            );

            TestContext.WriteLine("Skill added successfully before delete.");

            
            string deleteToast = _skillsSteps.DeleteSkill(data.Skill);

            
            Assert.That(
                deleteToast != null &&
                deleteToast.ToLower().Contains("deleted"),
                "Skill was not deleted successfully."
            );

            TestContext.WriteLine("Skill deleted successfully.");

            
            Assert.False(
                _skillsSteps.IsSkillPresent(data.Skill),
                "Deleted skill is still present in the Skills table."
            );
        }

        [Test]
        public void DeleteNonExistingSkill_Test()
        {
            
            string nonExistingSkill = "NonExistingSkill123";

            
            string deleteToast = _skillsSteps.DeleteSkill(nonExistingSkill);

            
            Assert.IsNull(
                deleteToast,
                "No delete toast should appear for a non-existing skill."
            );

            TestContext.WriteLine(
                $"Delete attempted for non-existing skill '{nonExistingSkill}' – no action performed as expected."
            );

            
            Assert.False(
                _skillsSteps.IsSkillPresent(nonExistingSkill),
                "Non-existing skill should still not be present after delete attempt."
            );
        }




    }
}
