using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace ProjectMarsAutomationAdvanceTask.Pages.Components
{
    public class ProfileLanguageComponent
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public ProfileLanguageComponent(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        private By AddNewButton => By.XPath("//div[contains(@class,'ui teal button') and text()='Add New']");
        private By LanguageInput => By.Name("name");
        private By LevelDropdown => By.Name("level");
        private By SaveButton => By.XPath("//input[@value='Add']");
        private By UpdateButton => By.XPath("//input[@value='Update']");
        private By Toast => By.XPath("//div[contains(@class,'ns-box-inner')]");

        private By TableRows => By.XPath("//div[@data-tab='first']//table//tbody/tr");

        private IWebElement WaitAndFind(By locator)
            => _wait.Until(ExpectedConditions.ElementIsVisible(locator));

        public string AddLanguage(string language, string level)
        {
            WaitAndFind(AddNewButton).Click();
            WaitAndFind(LanguageInput).SendKeys(language);

            var ddl = new SelectElement(WaitAndFind(LevelDropdown));
            ddl.SelectByText(level);

            WaitAndFind(SaveButton).Click();

            return WaitForToast();
        }

        public string UpdateLanguage(string oldLang, string newLang, string newLevel)
        {
            var row = FindLanguageRow(oldLang);
            if (row == null)
                throw new Exception($"Language '{oldLang}' not found for update!");

            row.FindElement(By.XPath(".//i[contains(@class,'outline write icon')]")).Click();

            var langInput = WaitAndFind(LanguageInput);
            langInput.Clear();
            langInput.SendKeys(newLang);

            var ddl = new SelectElement(WaitAndFind(LevelDropdown));
            ddl.SelectByText(newLevel);

            WaitAndFind(UpdateButton).Click();

            return WaitForToast();
        }

        public string DeleteLanguage(string language)
        {
            var row = FindLanguageRow(language);
            if (row == null)
                return null;

            row.FindElement(By.XPath(".//i[contains(@class,'remove icon')]")).Click();
            return WaitForToast();
        }

        public IWebElement FindLanguageRow(string language)
        {
            var rows = _driver.FindElements(TableRows);
            foreach (var row in rows)
            {
                if (row.Text.Contains(language, StringComparison.OrdinalIgnoreCase))
                    return row;
            }
            return null;
        }

      

        private string WaitForToast()
        {
            try
            {
                var toastElement = _wait.Until(ExpectedConditions.ElementIsVisible(Toast));
                string message = toastElement.Text;

                
                _wait.Until(ExpectedConditions.InvisibilityOfElementLocated(Toast));

                return message;
            }
            catch
            {
                return null;
            }
        }




    }
}
