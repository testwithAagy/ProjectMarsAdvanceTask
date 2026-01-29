using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using ProjectMarsAutomationAdvanceTask.Models;
using ProjectMarsAutomationAdvanceTask.Steps;
using ProjectMarsAutomationAdvanceTask.Tests.Base;
using ProjectMarsAutomationAdvanceTask.Utilities;

namespace ProjectMarsAutomationAdvanceTask.Tests
{
    [TestFixture]
    public class ShareSkillTests : BaseTest
    {
        private ShareSkillSteps _shareSkillSteps;

        [SetUp]
        public void TestSetup()
        {
            _shareSkillSteps = new ShareSkillSteps(Driver);
        }

        [Test]
        public void AddValidShareSkill_Test()
        {
            var data = ShareSkillDataReader.Read<ShareSkillTestData>(
                "ShareSkillTestData.json",
                "AddValidShareSkill"
            );

            _shareSkillSteps.OpenShareSkillPage();
            _shareSkillSteps.EnterTitle(data.Title);
            _shareSkillSteps.EnterDescription(data.Description);
            _shareSkillSteps.SelectCategory(data.Category, data.SubCategory);
            _shareSkillSteps.EnterTags(data.Tags);
            _shareSkillSteps.SelectServiceType(data.ServiceType);
            _shareSkillSteps.SelectLocationType(data.LocationType);
            _shareSkillSteps.SelectSkillTrade(data.SkillTradeType, data.SkillTradeValue);

            if (!string.IsNullOrEmpty(data.WorkSampleFile))
                _shareSkillSteps.UploadWorkSample(data.WorkSampleFile);

            _shareSkillSteps.SelectActiveStatus(data.ActiveStatus);



            _shareSkillSteps.SaveShareSkill();

            string toastMessage = _shareSkillSteps.GetToastMessage();
            Assert.That(toastMessage.ToLower().Contains("success") || toastMessage.ToLower().Contains("added"),
                        $"Expected success toast, but got: {toastMessage}");

            TestContext.WriteLine("Share Skill positive scenario executed successfully.");



        }


        [Test]
        public void ShareSkill_MandatoryFields_ShouldShowSuccessMessage()
        {
            var data = ShareSkillDataReader.Read<ShareSkillTestData>(
                "ShareSkillTestData.json",
                "AddShareSkillMandatoryFields"
            );

            _shareSkillSteps.OpenShareSkillPage();

            _shareSkillSteps.EnterTitle(data.Title);
            _shareSkillSteps.EnterDescription(data.Description);
            _shareSkillSteps.SelectCategory(data.Category, data.SubCategory);
            _shareSkillSteps.EnterTags(data.Tags);

            _shareSkillSteps.SelectServiceType(data.ServiceType);
            _shareSkillSteps.SelectLocationType(data.LocationType);
            
            _shareSkillSteps.SelectSkillTrade(
                data.SkillTradeType,
                data.SkillTradeValue
            );

            _shareSkillSteps.SelectActiveStatus(data.ActiveStatus);

            

            _shareSkillSteps.SaveShareSkill();


            bool isSuccess =
      !string.IsNullOrEmpty(_shareSkillSteps.GetToastMessage())
      || Driver.Url.Contains("ListingManagement");

            Assert.IsTrue(isSuccess, "Share Skill was not saved successfully");
        }


        [Test]
        public void ShareSkill_Cancel_ShouldGobackToProfilePage()
        {
            var data = ShareSkillDataReader.Read<ShareSkillTestData>(
                "ShareSkillTestData.json",
                "AddShareSkillMandatoryFields"
            );

            _shareSkillSteps.OpenShareSkillPage();

            _shareSkillSteps.EnterTitle(data.Title);
            _shareSkillSteps.EnterDescription(data.Description);
            _shareSkillSteps.SelectCategory(data.Category, data.SubCategory);
            _shareSkillSteps.EnterTags(data.Tags);

            _shareSkillSteps.SelectServiceType(data.ServiceType);
            _shareSkillSteps.SelectLocationType(data.LocationType);
            
            _shareSkillSteps.SelectSkillTrade(
                data.SkillTradeType,
                data.SkillTradeValue
            );

            _shareSkillSteps.SelectActiveStatus(data.ActiveStatus);
           
            _shareSkillSteps.CancelShareSkill();

            bool isSuccess =
      !string.IsNullOrEmpty(_shareSkillSteps.GetToastMessage())
      || Driver.Url.Contains("Profile");

            Assert.IsTrue(isSuccess, "Share Skill was not saved successfully");
        }


       

        [Test]
        public void ShareSkill_AddCalendarEvent_ShouldWork()
        {
            var data = ShareSkillDataReader.Read<ShareSkillTestData>(
                "ShareSkillTestData.json",
                "AddCalendarEvent"
            );

            _shareSkillSteps.OpenShareSkillPage();

            
            _shareSkillSteps.EnterTitle(data.Title);
            _shareSkillSteps.EnterDescription(data.Description);
            _shareSkillSteps.SelectCategory(data.Category, data.SubCategory);
            _shareSkillSteps.EnterTags(data.Tags);
            _shareSkillSteps.SelectServiceType(data.ServiceType);
            _shareSkillSteps.SelectLocationType(data.LocationType);
            _shareSkillSteps.SelectSkillTrade(data.SkillTradeType, data.SkillTradeValue);
            _shareSkillSteps.SelectActiveStatus(data.ActiveStatus);

            
            var calendarRow = Driver.FindElement(By.XPath("//tr[contains(@class,'k-middle-row')]"));
            var firstColumn = calendarRow.FindElements(By.TagName("td"))[0]; 

            Actions actions = new Actions(Driver);
            actions.DoubleClick(firstColumn).Perform();

            Thread.Sleep(500); 


            _shareSkillSteps.AddCalendarEvent(
    data.CalendarEvent.Title,
    data.CalendarEvent.Start,
    data.CalendarEvent.End,
    data.CalendarEvent.AllDay,
    data.CalendarEvent.Repeat,
    data.CalendarEvent.Description,
    data.CalendarEvent.Owner
);

            _shareSkillSteps.SaveShareSkill();

            bool isSuccess = !string.IsNullOrEmpty(_shareSkillSteps.GetToastMessage())
                             || Driver.Url.Contains("ListingManagement");

            Assert.IsTrue(isSuccess, "Calendar event was not added successfully");
        }


        [Test]
        public void ShareSkill_Title_MaxLengthValidation_PreventRemainingChars()
        {
            
            var data = ShareSkillDataReader.Read<ShareSkillTestData>(
                "ShareSkillTestData.json",
                "ShareSkillMaxTtileLengthData"
            );

            _shareSkillSteps.OpenShareSkillPage();

            
            _shareSkillSteps.EnterTitle(data.Title);

            
            _shareSkillSteps.EnterDescription(data.Description);
            _shareSkillSteps.SelectCategory(data.Category, data.SubCategory);
            _shareSkillSteps.EnterTags(data.Tags);
            _shareSkillSteps.SelectServiceType(data.ServiceType);
            _shareSkillSteps.SelectLocationType(data.LocationType);
            _shareSkillSteps.SelectSkillTrade(data.SkillTradeType, data.SkillTradeValue);
            _shareSkillSteps.SelectActiveStatus(data.ActiveStatus);

            
            string counterText = _shareSkillSteps.GetTitleCharacterRemainingText();

            Assert.IsNotNull(counterText, "Character counter was not displayed");
            Assert.That(counterText, Is.EqualTo("Characters remaining: 0"),
                $"Unexpected counter text: {counterText}");

        }

        [Test]
        public void ShareSkill_Description_MaxLength_ShouldShowZeroRemainingCharacters()
        {
            var data = ShareSkillDataReader.Read<ShareSkillTestData>(
                "ShareSkillTestData.json",
                "ShareSkillMaxDescriptionLengthData"
            );

            _shareSkillSteps.OpenShareSkillPage();

            
            _shareSkillSteps.EnterTitle("Valid Title");
            _shareSkillSteps.EnterDescription(data.Description);

            string counterText = _shareSkillSteps.GetDescriptionCharacterRemainingText();

            Assert.IsNotNull(counterText, "Description character counter was not displayed");
            Assert.That(counterText, Is.EqualTo("Characters remaining: 0"),
                $"Unexpected character counter text: {counterText}");
        }


        [Test]
        public void ShareSkill_EmptyTitle_ShouldShowMandatoryFieldError()
        {
            var data = ShareSkillDataReader.Read<ShareSkillTestData>(
                "ShareSkillTestData.json",
                "AddShareSkillMandatoryFields"
            );

            _shareSkillSteps.OpenShareSkillPage();

            _shareSkillSteps.EnterDescription(data.Description);
            _shareSkillSteps.SelectCategory(data.Category, data.SubCategory);
            _shareSkillSteps.EnterTags(data.Tags);
            _shareSkillSteps.SelectServiceType(data.ServiceType);
            _shareSkillSteps.SelectLocationType(data.LocationType);
            _shareSkillSteps.SelectSkillTrade(
                data.SkillTradeType,
                data.SkillTradeValue
            );
            _shareSkillSteps.SelectActiveStatus(data.ActiveStatus);

            _shareSkillSteps.SaveShareSkill();

            
            string titleError = _shareSkillSteps.GetTitleMandatoryError();

            bool isErrorShown =
                !string.IsNullOrEmpty(titleError)
                || Driver.Url.Contains("ShareSkill");

            Assert.IsTrue(
                isErrorShown,
                "Mandatory Title error was not shown and form was saved unexpectedly"
            );

            TestContext.WriteLine("Empty Title validation displayed successfully.");
        }

        [Test]
        public void ShareSkill_EmptyForm_ShouldShowFillFormToastMessage()
        {
            _shareSkillSteps.OpenShareSkillPage();
          
            _shareSkillSteps.SaveShareSkill();

           
            string toastMessage = _shareSkillSteps.GetToastMessage();
            Assert.IsFalse(string.IsNullOrEmpty(toastMessage), "Toast message was not displayed");
            Assert.AreEqual("Please complete the form correctly.", toastMessage, "Incorrect toast message shown");

            TestContext.WriteLine($"Toast validation: {toastMessage}");
        }


        [Test]
        public void ShareSkill_TitleWithSpecialCharacters_ShouldShowInlineError()
        {
            var data = ShareSkillDataReader.Read<ShareSkillTestData>(
                "ShareSkillTestData.json",
                "AddShareSkillInvalidTitleInput"
            );

            _shareSkillSteps.OpenShareSkillPage();

            
            _shareSkillSteps.EnterTitle(data.Title);

           
            _shareSkillSteps.EnterDescription(data.Description);
            _shareSkillSteps.SelectCategory(data.Category, data.SubCategory);
            _shareSkillSteps.EnterTags(data.Tags);
            _shareSkillSteps.SelectServiceType(data.ServiceType);
            _shareSkillSteps.SelectLocationType(data.LocationType);
            _shareSkillSteps.SelectSkillTrade(data.SkillTradeType, data.SkillTradeValue);
            _shareSkillSteps.SelectActiveStatus(data.ActiveStatus);

            
            _shareSkillSteps.SaveShareSkill();

            
            string titleError = _shareSkillSteps.GetTitleInlineError();
            Assert.IsFalse(string.IsNullOrEmpty(titleError), "Title inline error message is not displayed");
            TestContext.WriteLine($"Title validation message: {titleError}");
        }

        [Test]
        public void ShareSkill_InvalidDescription_ShouldShowInlineError()
        {
            var data = ShareSkillDataReader.Read<ShareSkillTestData>(
                "ShareSkillTestData.json",
                "AddShareSkillInvalidDescriptionInput"
            );

            _shareSkillSteps.OpenShareSkillPage();
            _shareSkillSteps.EnterTitle(data.Title);

            
            _shareSkillSteps.EnterDescription(data.Description);
            _shareSkillSteps.SelectCategory(data.Category, data.SubCategory);
            _shareSkillSteps.EnterTags(data.Tags);
            _shareSkillSteps.SelectServiceType(data.ServiceType);
            _shareSkillSteps.SelectLocationType(data.LocationType);
            _shareSkillSteps.SelectSkillTrade(data.SkillTradeType, data.SkillTradeValue);
            _shareSkillSteps.SelectActiveStatus(data.ActiveStatus);


            _shareSkillSteps.SaveShareSkill();

            string descError = _shareSkillSteps.GetDescriptionInlineError();
            Assert.IsFalse(string.IsNullOrEmpty(descError), "Description inline error message is not displayed");
            TestContext.WriteLine($"Description validation message: {descError}");
        }

        

        [Test]
        public void ShareSkill_DestructiveInput_ShouldShowValidationOrBeSanitized()
        {
            var data = ShareSkillDataReader.Read<ShareSkillTestData>(
                 "ShareSkillTestData.json",
                 "DestructiveInputTest"
             );

            _shareSkillSteps.OpenShareSkillPage();

            _shareSkillSteps.EnterTitle(data.Title);
            _shareSkillSteps.EnterDescription(data.Description);
            _shareSkillSteps.EnterTags(data.Tags);
         

            var titleError = _shareSkillSteps.GetTitleInlineError();
            var descError = _shareSkillSteps.GetDescriptionInlineError();

            Assert.IsTrue(
                !string.IsNullOrEmpty(titleError) ||
                !string.IsNullOrEmpty(descError),
                "Destructive input was not validated or sanitized"
            );
        }


    }


}
