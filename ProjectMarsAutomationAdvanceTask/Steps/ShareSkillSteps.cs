using OpenQA.Selenium;
using ProjectMarsAutomationAdvanceTask.Pages.Components;
using System;

namespace ProjectMarsAutomationAdvanceTask.Steps
{
    public class ShareSkillSteps
    {
        private readonly ShareSkillComponent _shareSkillComponent;

        public ShareSkillSteps(IWebDriver driver)
        {
            _shareSkillComponent = new ShareSkillComponent(driver);
        }

       
        public void OpenShareSkillPage()
        {
            _shareSkillComponent.OpenShareSkillPage();
        }

        
        public void EnterTitle(string title)
        {
            _shareSkillComponent.EnterTitle(title);
        }

        public void EnterDescription(string description)
        {
            _shareSkillComponent.EnterDescription(description);
        }

        public void SelectCategory(string mainCategory, string subCategory = null)
        {
            _shareSkillComponent.SelectCategory(mainCategory, subCategory);
        }

       

        public void EnterTags(string tags)
        {
           
            var tagList = tags.Split(',');
            _shareSkillComponent.EnterTags(tagList);
        }


        public void SelectServiceType(string serviceType)
        {
            _shareSkillComponent.SelectServiceType(serviceType);
        }

        public void SelectLocationType(string locationType)
        {
            _shareSkillComponent.SelectLocationType(locationType);
        }

        public void SelectSkillTrade(string tradeType, string value = null)
        {
            _shareSkillComponent.SelectSkillTrade(tradeType, value);
        }

        public void UploadWorkSample(string filePath)
        {
            _shareSkillComponent.UploadWorkSample(filePath);
        }

        public void SelectActiveStatus(string status)
        {
            _shareSkillComponent.SelectActiveStatus(status);
        }

        public void SaveShareSkill()
        {
            _shareSkillComponent.SaveShareSkill();
        }

        public void CancelShareSkill()
        {
            _shareSkillComponent.CancelShareSkill();
        }

        public string GetTitleCharacterRemainingText()
        {
            return _shareSkillComponent.GetTitleCharacterRemainingText();
        }
        public string GetDescriptionCharacterRemainingText()
        {
            return _shareSkillComponent.GetDescriptionCharacterRemainingText();
        }

        public string GetTitleMandatoryError()
        {
            return _shareSkillComponent.GetTitleMandatoryError();
        }

        public string GetTitleInlineError()
        {
            return _shareSkillComponent.GetTitleInlineError();
        }

        public string GetDescriptionInlineError()
        {
            return _shareSkillComponent.GetDescriptionInlineError();
        }


        public string GetInlineErrorForField(string fieldName)
        {
            return _shareSkillComponent.GetInlineErrorForField(fieldName);
        }


        public void AddCalendarEvent(
    string title,
    string start,
    string end,
    bool allDay ,
    string repeat,
    string description,
    string owner
)
        {
            _shareSkillComponent.AddCalendarEvent(
                title,
                start,
                end,
                allDay,
                repeat,
                description,
                owner
            );
        }


        public string GetToastMessage()
        {
            return _shareSkillComponent.GetToastMessage();
        }
    }
}
