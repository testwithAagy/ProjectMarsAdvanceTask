using OpenQA.Selenium;
using ProjectMarsAutomationAdvanceTask.Pages.Components;
using System;

namespace ProjectMarsAutomationAdvanceTask.Steps
{
    public class SearchSkillSteps
    {
        private readonly SearchSkillComponent _searchSkillComponent;

        public SearchSkillSteps(IWebDriver driver)
        {
            _searchSkillComponent = new SearchSkillComponent(driver);
        }

        
        public void NaviagteToSearchSkillPage()
        {
            _searchSkillComponent.NaviagteToSearchSkillPage();
        }

        
        public void SearchSkillByKeyword(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                throw new ArgumentException("Keyword cannot be null or empty.", nameof(keyword));
            _searchSkillComponent.SearchSkillByKeyword(keyword);
        }

        
        public bool IsSearchResultDisplayed()
        {
            return _searchSkillComponent.IsSearchResultDisplayed();
        }

        public List<string> GetSearchResultsText()
        {
            return _searchSkillComponent.GetSearchResultsText();
        }

        public void FilterByMainCategory(string category)
        {
            _searchSkillComponent.FilterByMainCategory(category);
        }

        public void FilterBySubCategory(string mainCategory, string subCategory)
        {
            _searchSkillComponent.FilterBySubCategory(mainCategory, subCategory);
        }

        public void SelectAllCategories()
        {
            _searchSkillComponent.SelectAllCategories();
        }
        public void ApplyOnlineFilter()
        {
            _searchSkillComponent.ApplyOnlineFilter();
        }

        public void ApplyOnsiteFilter()
        {
            _searchSkillComponent.ApplyOnsiteFilter();
        }
        public void ApplyShowAllFilter()
        {
            _searchSkillComponent.ApplyShowAllFilter();
        }

        public void ClickFirstSkillCard()
        {
            _searchSkillComponent.ClickFirstSkillCard();
        }

        public bool IsNoResultsMessageDisplayed(string expectedMessage)
        {
            return _searchSkillComponent.IsNoResultsMessageDisplayed(expectedMessage);
        }


    }
}
