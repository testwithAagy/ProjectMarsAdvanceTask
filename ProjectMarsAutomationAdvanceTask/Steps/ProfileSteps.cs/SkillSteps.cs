using ProjectMarsAutomationAdvanceTask.Pages.Components;
using OpenQA.Selenium;

namespace ProjectMarsAutomationAdvanceTask.Steps
{
    public class SkillSteps
    {
        private readonly ProfileSkillsComponent _skillsComponent;

        
        public SkillSteps(IWebDriver driver)
        {
            _skillsComponent = new ProfileSkillsComponent(driver);
        }

        
        public string AddSkill(string skill, string level)
        {
            return _skillsComponent.AddSkill(skill, level);
        }

        
        public string UpdateSkill(string oldSkill, string newSkill, string newLevel)
        {
            return _skillsComponent.UpdateSkill(oldSkill, newSkill, newLevel);
        }

        
        public string DeleteSkill(string skill)
        {
            return _skillsComponent.DeleteSkill(skill);
        }

       
        public bool IsSkillPresent(string skill)
        {
            var row = _skillsComponent.FindSkillRow(skill);
            return row != null;
        }
    }
}

