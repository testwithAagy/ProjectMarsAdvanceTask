namespace ProjectMarsAutomationAdvanceTask.Models
{
    public class ShareSkillTestData
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string Tags { get; set; }
        public string ServiceType { get; set; } 
        public string LocationType { get; set; } 
        public string SkillTradeType { get; set; } 
        public string SkillTradeValue { get; set; } 
        public string WorkSampleFile { get; set; } 
        public string ActiveStatus { get; set; } 

        
        public CalendarEvent CalendarEvent { get; set; }
    }

    public class CalendarEvent
    {
        public string Title { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public bool AllDay { get; set; }
        public string Repeat { get; set; }
        
        public string Description { get; set; }
        public string Owner { get; set; }

    }
}
