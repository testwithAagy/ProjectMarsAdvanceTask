using NUnit.Framework;
using ProjectMarsAutomationAdvanceTask.Helpers;
using ProjectMarsAutomationAdvanceTask.Steps.ProfileSteps;
using ProjectMarsAutomationAdvanceTask.Tests.Base;
using ProjectMarsAutomationAdvanceTask.Utilities;
using ProjectMarsAutomationAdvanceTask.Models;
using ProjectMarsAutomationAdvanceTask.AssertHelpers;
using System.IO;

namespace ProjectMarsAutomationAdvanceTask.Tests
{
    [TestFixture]
    public class ProfileAccountTests : BaseTest
    {
        private AboutInfoSteps _aboutInfoSteps;
        private AboutInfoTestData _testData;

        [SetUp]
        public void SetupTestData()
        {
            _aboutInfoSteps = new AboutInfoSteps(Driver);

            string jsonPath = Path.Combine(
                AppContext.BaseDirectory,
                "..", "..", "..",
                "TestData",
                "AboutInfoData.json"
            );

            if (!File.Exists(jsonPath))
                throw new FileNotFoundException($"Test data file not found: {jsonPath}");

            _testData = JsonDataReader.GetAboutInfoData(jsonPath);
        }

        
        [Test]
        public void Profile_UpdateAllAboutInfoFields_ShouldSucceed()
        {
            string toastMessage = _aboutInfoSteps.UpdateAllFields(
                _testData.Availability,
                _testData.Hours,
                _testData.EarnTarget
            );

            ProfileAssertHelper.AssertSuccessToast(
                toastMessage,
                "Availability updated"
            );
        }

        
        [Test]
        public void Profile_UpdateAvailabilityOnly_ShouldSucceed()
        {
            string toastMessage =
                _aboutInfoSteps.UpdateAvailabilityOnly(_testData.Availability);

            ProfileAssertHelper.AssertSuccessToast(
                toastMessage,
                "Availability updated"
            );
        }

        
        [Test]
        public void Profile_UpdateHoursOnly_ShouldSucceed()
        {
            string toastMessage =
                _aboutInfoSteps.UpdateHoursOnly(_testData.Hours);

            ProfileAssertHelper.AssertSuccessToast(
                toastMessage,
                "Availability updated"
            );
        }

        
        [Test]
        public void Profile_UpdateEarnTargetOnly_ShouldSucceed()
        {
            string toastMessage =
                _aboutInfoSteps.UpdateEarnTargetOnly(_testData.EarnTarget);

            ProfileAssertHelper.AssertSuccessToast(
                toastMessage,
                "Availability updated"
            );
        }
    }
}
