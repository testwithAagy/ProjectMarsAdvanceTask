using ProjectMarsAutomationAdvanceTask.Pages.Components;
using OpenQA.Selenium;

namespace ProjectMarsAutomationAdvanceTask.Steps
{
    public class LanguageSteps
    {
        private readonly ProfileLanguageComponent _languageComponent;

        public LanguageSteps(IWebDriver driver)
        {
            _languageComponent = new ProfileLanguageComponent(driver);
        }

        
        public string AddLanguage(string language, string level)
        {
            return _languageComponent.AddLanguage(language, level);
        }

        
        public string UpdateLanguage(string oldLanguage, string newLanguage, string newLevel)
        {
            return _languageComponent.UpdateLanguage(oldLanguage, newLanguage, newLevel);
        }

      
        public string DeleteLanguage(string language)
        {
            return _languageComponent.DeleteLanguage(language);
        }

       
        public bool IsLanguagePresent(string language)
        {
            var row = _languageComponent.FindLanguageRow(language);
            return row != null;
        }
    }
}
