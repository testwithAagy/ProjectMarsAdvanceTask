using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System;
using System.IO;

namespace ProjectMarsAutomationAdvanceTask.Reporting
{
    public static class ExtentReportManager
    {
        private static ExtentReports _extent;
        private static ExtentTest _test;

        public static ExtentReports GetExtent()
        {
            if (_extent == null)
            {
                string reportFolder = Path.Combine(Directory.GetCurrentDirectory(), "Reports");
                Directory.CreateDirectory(reportFolder);

                var htmlReporter = new ExtentSparkReporter(Path.Combine(reportFolder, "TestReport.html"));
                htmlReporter.Config.DocumentTitle = "Mars Automation Test Report";
                htmlReporter.Config.ReportName = "Mars Automation Test Results";

                _extent = new ExtentReports();
                _extent.AttachReporter(htmlReporter);
                _extent.AddSystemInfo("Environment", "QA");
                _extent.AddSystemInfo("Browser", "Chrome");
            }

            return _extent;
        }

        public static ExtentTest CreateTest(string testName)
        {
            _test = _extent.CreateTest(testName);
            return _test;
        }

        public static ExtentTest GetTest() => _test;

        public static void FlushReport() => _extent.Flush();
    }
}
