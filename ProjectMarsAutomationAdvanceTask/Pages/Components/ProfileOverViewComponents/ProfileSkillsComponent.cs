using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace ProjectMarsAutomationAdvanceTask.Pages.Components
{
    public class ProfileSkillsComponent
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public ProfileSkillsComponent(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        
        private By AddNewButton => By.XPath("//div[@data-tab='second']//th//div[contains(@class,'ui teal button') and text()='Add New']");

        private By SkillInput => By.Name("name");
        private By LevelDropdown => By.Name("level");
        private By SaveButton => By.XPath("//input[@value='Add']");
        private By UpdateButton => By.XPath("//input[@value='Update']");
        private By Toast => By.XPath("//div[contains(@class,'ns-box-inner')]");

        private By TableRows => By.XPath("//div[@data-tab='second']//table//tbody/tr");

        private IWebElement WaitAndFind(By locator)
            => _wait.Until(ExpectedConditions.ElementIsVisible(locator));

        public string AddSkill(string skill, string level)
        {
           

            
            var addNewBtn = _wait.Until(ExpectedConditions.ElementToBeClickable(AddNewButton));
            addNewBtn.Click();

            WaitAndFind(SkillInput).SendKeys(skill);

            var ddl = new SelectElement(WaitAndFind(LevelDropdown));

            
            if (!ddl.Options.Any(o => o.Text.Equals(level, StringComparison.OrdinalIgnoreCase)))
                throw new Exception($"Level '{level}' not found in dropdown.");
            ddl.SelectByText(level);

            WaitAndFind(SaveButton).Click();

            return WaitForToast();
        }

        public string UpdateSkill(string oldSkill, string newSkill, string newLevel)
        {
            var row = FindSkillRow(oldSkill);
            if (row == null)
                throw new Exception($"Skill '{oldSkill}' not found for update!");

            row.FindElement(By.XPath(".//i[contains(@class,'outline write icon')]")).Click();

            var skillInput = WaitAndFind(SkillInput);
            skillInput.Clear();
            skillInput.SendKeys(newSkill);

            var ddl = new SelectElement(WaitAndFind(LevelDropdown));
            ddl.SelectByText(newLevel);

            WaitAndFind(UpdateButton).Click();

            return WaitForToast();
        }

        public string DeleteSkill(string skill)
        {
            var row = FindSkillRow(skill);
            if (row == null)
                return null;

            row.FindElement(By.XPath(".//i[contains(@class,'remove icon')]")).Click();
            return WaitForToast();
        }

        public IWebElement FindSkillRow(string skill)
        {
            var rows = _driver.FindElements(TableRows);
            foreach (var row in rows)
            {
                if (row.Text.Contains(skill, StringComparison.OrdinalIgnoreCase))
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
