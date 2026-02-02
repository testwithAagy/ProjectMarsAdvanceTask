using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace ProjectMarsAutomationAdvanceTask.Pages.Components
{
    public class SearchSkillComponent
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public SearchSkillComponent(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        private By SearchSkillIcon =>
    By.XPath("//input[@placeholder='Search skills']/following-sibling::i[contains(@class,'search')]");

        private By SearchSkillInput =>
    By.XPath("//input[@placeholder='Search skills']");
        public void NaviagteToSearchSkillPage()
        {
            var searchInput = WaitAndFind(SearchSkillInput);
            searchInput.Click();

            WaitAndFind(SearchSkillIcon).Click();
        }
        private IWebElement WaitAndFind(By locator)
        {
            return _wait.Until(ExpectedConditions.ElementIsVisible(locator));
        }


        private By SearchResults =>
            By.XPath("//div[contains(@class,'ui card')]");

       
        public void SearchSkillByKeyword(string keyword)
        {

            if (string.IsNullOrWhiteSpace(keyword))
                throw new ArgumentException("Keyword cannot be null or empty.", nameof(keyword));
            var input = WaitAndFind(SearchSkillInput);
            input.Clear();
            input.SendKeys(keyword);

            WaitAndFind(SearchSkillIcon).Click();
        }

      
        public bool IsSearchResultDisplayed()
        {
            return _wait.Until(driver =>
                driver.FindElements(SearchResults).Count > 0);
        }

        public List<string> GetSearchResultsText()
        {
            _wait.Until(driver => driver.FindElements(SearchResults).Count > 0);
            var results = _driver.FindElements(SearchResults);
            return results.Select(r => r.Text.Trim()).ToList();
        }

        private By MainCategory(string category) =>
    By.XPath($"//a[contains(@class,'category') and contains(normalize-space(.),'{category}')]");

        public void FilterByMainCategory(string category)
        {
            if (string.IsNullOrWhiteSpace(category))
                throw new ArgumentException("MainCategory is missing in test data");

            var categoryElement = _wait.Until(
                ExpectedConditions.ElementToBeClickable(MainCategory(category))
            );

            ((IJavaScriptExecutor)_driver)
                .ExecuteScript("arguments[0].scrollIntoView({block:'center'});", categoryElement);

            categoryElement.Click();

            _wait.Until(driver => driver.FindElements(SearchResults).Count >= 0);
        }


        private By SubCategory(string subCategory) =>
    By.XPath($"//a[contains(@class,'subcategory') and contains(normalize-space(.),'{subCategory}')]");

        public void FilterBySubCategory(string mainCategory, string subCategory)
        {
            if (string.IsNullOrWhiteSpace(mainCategory))
                throw new ArgumentException("MainCategory is missing");

            if (string.IsNullOrWhiteSpace(subCategory))
                throw new ArgumentException("SubCategory is missing");

            FilterByMainCategory(mainCategory);

            
            var subCategoryElement = _wait.Until(
                ExpectedConditions.ElementToBeClickable(SubCategory(subCategory))
            );

            ((IJavaScriptExecutor)_driver)
                .ExecuteScript("arguments[0].scrollIntoView({block:'center'});", subCategoryElement);

            subCategoryElement.Click();

            
            _wait.Until(driver => driver.FindElements(SearchResults).Count >= 0);
        }

        private By AllCategories =>
    By.XPath("//a[contains(normalize-space(.),'All Categories')]");

        public void SelectAllCategories()
        {
            var allCategories = _wait.Until(
                ExpectedConditions.ElementToBeClickable(AllCategories)
            );

            ((IJavaScriptExecutor)_driver)
                .ExecuteScript("arguments[0].scrollIntoView({block:'center'});", allCategories);

            allCategories.Click();

            
            _wait.Until(driver => driver.FindElements(SearchResults).Count > 0);
        }


        private By OnlineFilterButton =>
    By.XPath("//button[normalize-space()='Online']");
        public void ApplyOnlineFilter()
        {
            WaitAndFind(OnlineFilterButton).Click();

            _wait.Until(driver => driver.FindElements(SearchResults).Count > 0);
        }

        private By OnsiteFilterButton =>
    By.XPath("//button[normalize-space()='Onsite']");
        public void ApplyOnsiteFilter()
        {
            WaitAndFind(OnsiteFilterButton).Click();

            _wait.Until(driver => driver.FindElements(SearchResults).Count > 0);
        }
        private By ShowAllFilterButton =>
    By.XPath("//button[normalize-space()='ShowAll']");

        public void ApplyShowAllFilter()
        {
            WaitAndFind(ShowAllFilterButton).Click();

            _wait.Until(driver => driver.FindElements(SearchResults).Count > 0);
        }


        private By SkillCards =>
    By.XPath("(//div[contains(@class,'ui card')]//a[contains(@href,'/Home/ServiceDetail')])[1]");
        public void ClickFirstSkillCard()
        {
            _wait.Until(driver => driver.FindElements(SkillCards).Count > 0);
            _driver.FindElements(SkillCards).First().Click();
        }

        private By NoResultsMessage =>
    By.XPath("//h3[contains(text(),'No results found')]");

        public bool IsNoResultsMessageDisplayed(string expectedMessage)
        {
            var messageElement = _wait.Until(
                ExpectedConditions.ElementIsVisible(NoResultsMessage)
            );

            return messageElement.Text.Trim()
                .Equals(expectedMessage, StringComparison.OrdinalIgnoreCase);
        }



    }
}