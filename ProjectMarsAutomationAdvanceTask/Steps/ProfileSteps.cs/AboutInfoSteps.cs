using OpenQA.Selenium;
using ProjectMarsAutomationAdvanceTask.Pages.Components.ProfileOverviewComponents;

namespace ProjectMarsAutomationAdvanceTask.Steps.ProfileSteps
{
    public class AboutInfoSteps
    {
        private readonly AboutInfoComponent _aboutInfo;

        public AboutInfoSteps(IWebDriver driver)
        {
            _aboutInfo = new AboutInfoComponent(driver);
        }

       
        public string UpdateAllFields(string availability, string hours, string earnTarget)
        {
            _aboutInfo.UpdateAvailability(availability);
            _aboutInfo.UpdateHours(hours);
            return _aboutInfo.UpdateEarnTarget(earnTarget);
        }

       
        public string UpdateAvailabilityOnly(string availability) =>
            _aboutInfo.UpdateAvailability(availability);

        public string UpdateHoursOnly(string hours) =>
            _aboutInfo.UpdateHours(hours);

        public string UpdateEarnTargetOnly(string earnTarget) =>
            _aboutInfo.UpdateEarnTarget(earnTarget);
    }
}
