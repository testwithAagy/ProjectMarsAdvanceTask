using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using ProjectMarsAutomationAdvanceTask.Models;
using ProjectMarsAutomationAdvanceTask.Steps;
using ProjectMarsAutomationAdvanceTask.Tests.Base;
using ProjectMarsAutomationAdvanceTask.Utilities;

namespace ProjectMarsAutomationAdvanceTask.Tests
{
    [TestFixture]
    public class SearchSkillTests : BaseTest
    {
        private SearchSkillSteps _searchSkillSteps;

        [SetUp]
        public void TestSetup()
        {
            _searchSkillSteps = new SearchSkillSteps(Driver);
        }

        [Test]
        public void SearchSkill_NavigateToSearchSkillPage_ShouldSucess()
        {
            
            var currentUrl = Driver.Url;

            
            _searchSkillSteps.NaviagteToSearchSkillPage();

           
            Assert.IsTrue(
                Driver.Url != currentUrl && Driver.Url.Contains("Search", System.StringComparison.OrdinalIgnoreCase),
                "Clicking the search icon did not navigate to the Search Skill page."
            );
        }

        [Test]
        public void SearchSkill_WithValidKeyword_ShouldDisplayResults()
        {
            
            var data = SearchSkillDataReader.Read<SearchSkillTestData>(
                "SearchSkillTestData.json",
                "SearchSkill_ValidKeyword"
            );

            _searchSkillSteps.NaviagteToSearchSkillPage();

            
            _searchSkillSteps.SearchSkillByKeyword(data.Keyword);

            
            Assert.IsTrue(
                _searchSkillSteps.IsSearchResultDisplayed(),
                "Search results were not displayed for a valid keyword."
            );
        }

        [Test]
        public void SearchSkill_CaseInsensitiveSearch_ShouldDisplayResults()
        {
           
            var data = SearchSkillDataReader.Read<SearchSkillTestData>(
                "SearchSkillTestData.json",
                "SearchSkill_CaseInsensitive"
            );

            _searchSkillSteps.NaviagteToSearchSkillPage();

            
            _searchSkillSteps.SearchSkillByKeyword(data.KeywordUpper);
            Assert.IsTrue(
                _searchSkillSteps.IsSearchResultDisplayed(),
                "Search results were not displayed for uppercase keyword."
            );

            
            _searchSkillSteps.SearchSkillByKeyword(data.KeywordLower);
            Assert.IsTrue(
                _searchSkillSteps.IsSearchResultDisplayed(),
                "Search results were not displayed for lowercase keyword."
            );
        }

        [Test]
        public void SearchSkill_FilterByMainCategory_ShouldDisplayRelevantResults()
        {
            var data = SearchSkillDataReader.Read<SearchSkillTestData>(
                "SearchSkillTestData.json",
                "SearchSkill_FilterByMainCategory"
            );

            _searchSkillSteps.NaviagteToSearchSkillPage();
            _searchSkillSteps.FilterByMainCategory(data.MainCategory);

            var results = _searchSkillSteps.GetSearchResultsText();

            Assert.IsTrue(
                results.Any(r =>
                    r.Contains(data.ExpectedResultContains, StringComparison.OrdinalIgnoreCase)),
                $"No results found for category: {data.MainCategory}"
            );
        }

        [Test]
        public void SearchSkill_FilterBySubCategory_ShouldDisplayRelevantResults()
        {
            var data = SearchSkillDataReader.Read<SearchSkillTestData>(
                "SearchSkillTestData.json",
                "SearchSkill_FilterBySubCategory"
            );

            _searchSkillSteps.NaviagteToSearchSkillPage();
            _searchSkillSteps.FilterBySubCategory(data.MainCategory, data.SubCategory);

            var results = _searchSkillSteps.GetSearchResultsText();

            Assert.IsTrue(
                results.Any(r =>
                    r.Contains(data.ExpectedResultContains, StringComparison.OrdinalIgnoreCase)),
                $"No results found for subcategory: {data.SubCategory}"
            );
        }


        [Test]
        public void SearchSkill_AllCategories_ShouldDisplayAllItems()
        {
            var data = SearchSkillDataReader.Read<SearchSkillTestData>(
                "SearchSkillTestData.json",
                "SearchSkill_AllCategories"
            );

            _searchSkillSteps.NaviagteToSearchSkillPage();
            _searchSkillSteps.SelectAllCategories();

            var results = _searchSkillSteps.GetSearchResultsText();

            Assert.IsTrue(
                results.Count >= data.ExpectedMinimumResults,
                "All Categories did not display any results."
            );
        }

        [Test]
        public void SearchSkill_FilterByOnline_ShouldDisplayOnlineSkills()
        {
            var data = SearchSkillDataReader.Read<SearchSkillTestData>(
                "SearchSkillTestData.json",
                "SearchSkill_OnlineFilter"
            );

            
            _searchSkillSteps.NaviagteToSearchSkillPage();

            
            _searchSkillSteps.ApplyOnlineFilter();

            
            var results = _searchSkillSteps.GetSearchResultsText();

            Assert.IsTrue(
                results.Count >= data.ExpectedMinimumResults,
                "Online filter did not return any results."
            );
        }

        [Test]
        public void SearchSkill_FilterByOnsite_ShouldDisplayOnsiteSkills()
        {
            var data = SearchSkillDataReader.Read<SearchSkillTestData>(
                "SearchSkillTestData.json",
                "SearchSkill_OnsiteFilter"
            );

            
            _searchSkillSteps.NaviagteToSearchSkillPage();

            
            _searchSkillSteps.ApplyOnsiteFilter();

            
            var results = _searchSkillSteps.GetSearchResultsText();

            Assert.IsTrue(
                results.Count >= data.ExpectedMinimumResults,
                "Onsite filter did not return any results."
            );
        }

        [Test]
        public void SearchSkill_ShowAllFilter_ShouldResetFiltersAndDisplayAllItems()
        {
            var data = SearchSkillDataReader.Read<SearchSkillTestData>(
                "SearchSkillTestData.json",
                "SearchSkill_ShowAllFilter"
            );

            
            _searchSkillSteps.NaviagteToSearchSkillPage();

            
            _searchSkillSteps.ApplyOnlineFilter();

            
            _searchSkillSteps.ApplyShowAllFilter();

            
            var results = _searchSkillSteps.GetSearchResultsText();

            Assert.IsTrue(
                results.Count >= data.ExpectedMinimumResults,
                "ShowAll filter did not reset results correctly."
            );
        }

        [Test]
        public void SearchSkill_ClickingSkillCard_ShouldGotoServiceBooking()
        {
            
            _searchSkillSteps.NaviagteToSearchSkillPage();
            string urlBeforeClick = Driver.Url;

            
            _searchSkillSteps.ClickFirstSkillCard();

            
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            wait.Until(driver => driver.Url != urlBeforeClick);
            Assert.IsTrue(
    Driver.Url.Contains("Service") || Driver.Url.Contains("Detail"),
    "URL changed but did not navigate to service page."
);

        }



        [Test]
        public void SearchSkill_PartialKeywordSearch_ShouldDisplayRelevantResults()
        {
            
            var data = SearchSkillDataReader.Read<SearchSkillTestData>(
                "SearchSkillTestData.json",
                "SearchSkill_PartialKeyword"
            );

           
            Assert.IsFalse(string.IsNullOrWhiteSpace(data.PartialKeyword), "PartialKeyword is missing in JSON");
            Assert.IsFalse(string.IsNullOrWhiteSpace(data.ExpectedResultContains), "ExpectedResultContains is missing in JSON");

            
            _searchSkillSteps.NaviagteToSearchSkillPage();

           
            _searchSkillSteps.SearchSkillByKeyword(data.PartialKeyword);

            
            var results = _searchSkillSteps.GetSearchResultsText();

            Assert.IsTrue(
                results.Any(r => r.Contains(data.ExpectedResultContains, StringComparison.OrdinalIgnoreCase)),
                $"No search results contained the expected text: '{data.ExpectedResultContains}'"
            );
        }



        [Test]
        public void SearchSkill_TrimmedKeyword_ShouldDisplayRelevantResults()
        {
           
            var data = SearchSkillDataReader.Read<SearchSkillTestData>(
                "SearchSkillTestData.json",
                "SearchSkill_TrimmedKeyword"
            );

            
            _searchSkillSteps.NaviagteToSearchSkillPage();

            
            _searchSkillSteps.SearchSkillByKeyword(data.Keyword);

            
            var results = _searchSkillSteps.GetSearchResultsText();

           
            Assert.IsTrue(
                results.Any(r => r.Contains(data.ExpectedResultContains, StringComparison.OrdinalIgnoreCase)),
                $"Search with leading/trailing spaces did not return expected results containing: {data.ExpectedResultContains}"
            );
        }


        [Test]
       
        public void SearchSkill_NoMatchingKeyword_ShouldShowNoResultsMessage()
        {
            
            var data = SearchSkillDataReader.Read<SearchSkillTestData>(
                "SearchSkillTestData.json",
                "SearchSkill_NoMatchingKeyword"
            );

            _searchSkillSteps.NaviagteToSearchSkillPage();

            
            _searchSkillSteps.SearchSkillByKeyword(data.Keyword);

            
            Assert.IsTrue(
                _searchSkillSteps.IsNoResultsMessageDisplayed(data.ExpectedMessage),
                "No results message was not displayed for invalid search keyword."
            );
        }

        [Test]
        public void SearchSkill_RandomSpecialCharactersOnly_ShouldShowNoResultsMessage()
        {
            
            var data = SearchSkillDataReader.Read<SearchSkillTestData>(
                "SearchSkillTestData.json",
                "SearchSkill_SpecialCharactersOnly"
            );

            _searchSkillSteps.NaviagteToSearchSkillPage();

            
            _searchSkillSteps.SearchSkillByKeyword(data.Keyword);

            
            Assert.IsTrue(
                _searchSkillSteps.IsNoResultsMessageDisplayed(data.ExpectedMessage),
                "No results message was not displayed for special characters search."
            );
        }

        [Test]
        public void SearchSkill_XSSInjectionAttempt_ShouldNotExecuteScript_AndShowNoResults()
        {
            
            var data = SearchSkillDataReader.Read<SearchSkillTestData>(
                "SearchSkillTestData.json",
                "SearchSkill_XSSInjectionAttempt"
            );

            _searchSkillSteps.NaviagteToSearchSkillPage();

            
            _searchSkillSteps.SearchSkillByKeyword(data.Keyword);

            
            Assert.IsTrue(
                _searchSkillSteps.IsNoResultsMessageDisplayed(data.ExpectedMessage),
                "Application did not safely handle XSS injection attempt."
            );
        }


        [Test]
        public void SearchSkill_HugePayload_ShouldShowNoResults()
        {
            var data = SearchSkillDataReader.Read<SearchSkillTestData>(
                "SearchSkillTestData.json",
                "SearchSkill_HugePayload"
            );

           
            string keyword = data.PayloadLength > 0
                ? new string('A', data.PayloadLength)
                : data.Keyword;

            _searchSkillSteps.NaviagteToSearchSkillPage();
            _searchSkillSteps.SearchSkillByKeyword(keyword);

            Assert.IsTrue(
                _searchSkillSteps.IsNoResultsMessageDisplayed(data.ExpectedMessage),
                "No results message was not displayed for huge payload input."
            );
        }

    }
}
