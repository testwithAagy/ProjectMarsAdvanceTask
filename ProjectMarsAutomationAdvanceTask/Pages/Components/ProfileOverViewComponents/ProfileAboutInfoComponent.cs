using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace ProjectMarsAutomationAdvanceTask.Pages.Components.ProfileOverviewComponents
{
    public class AboutInfoComponent
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public AboutInfoComponent(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        private IWebElement AvailabilityEditBtn =>
            _driver.FindElement(By.XPath("//div[@class='item'][.//strong[text()='Availability']]//i[contains(@class,'write icon')]"));

        private IWebElement HoursEditBtn =>
            _driver.FindElement(By.XPath("//div[@class='item'][.//strong[text()='Hours']]//i[contains(@class,'write icon')]"));

        private IWebElement EarnTargetEditBtn =>
            _driver.FindElement(By.XPath("//div[@class='item'][.//strong[text()='Earn Target']]//i[contains(@class,'write icon')]"));
        private IWebElement AvailabilityDropdown =>
            _driver.FindElement(By.XPath("//select[@name='availabiltyType']"));

        private IWebElement HoursDropdown =>
            _driver.FindElement(By.XPath("//select[@name='availabiltyHour']"));

        private IWebElement EarnTargetDropdown =>
            _driver.FindElement(By.XPath("//select[@name='availabiltyTarget']"));
        private IWebElement SuccessToast =>
            _driver.FindElement(By.XPath("//div[contains(@class,'ns-box-inner')]"));

       
        public string UpdateAvailability(string value)
        {
            AvailabilityEditBtn.Click();
            SelectFromDropdownIfNeeded(AvailabilityDropdown, value);
            return WaitForToast();
        }

        public string UpdateHours(string value)
        {
            HoursEditBtn.Click();
            SelectFromDropdownIfNeeded(HoursDropdown, value);
            return WaitForToast();
        }

        public string UpdateEarnTarget(string value)
        {
            EarnTargetEditBtn.Click();
            SelectFromDropdownIfNeeded(EarnTargetDropdown, value);
            return WaitForToast();
        }

       
        private void SelectFromDropdownIfNeeded(IWebElement dropdown, string value)
        {
            var select = new SelectElement(dropdown);
            if (select.SelectedOption.Text != value)
            {
                select.SelectByText(value);
            }
        }

        private string WaitForToast()
        {
            try
            {
                var toast = _wait.Until(driver =>
                {
                    var elements = driver.FindElements(By.CssSelector(".ns-box-inner"));
                    return elements.Count > 0 && elements[0].Displayed ? elements[0] : null;
                });

                return toast?.Text;
            }
            catch
            {
                return null;
            }
        }
    }
}
