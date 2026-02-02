namespace ProjectMarsAutomationAdvanceTask.Models
{
    public class SearchSkillTestData
    {
        public string Keyword { get; set; }

        public string KeywordUpper { get; set; }
        public string KeywordLower { get; set; }

        public string PartialKeyword { get; set; }
        public string ExpectedResultContains { get; set; }

        public string MainCategory { get; set; }
        public int ExpectedMinimumResults { get; set; }

        public string ExpectedMessage { get; set; }

        public int PayloadLength { get; set; }

        public string SubCategory { get; set; }

    }
}
