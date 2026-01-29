using System.IO;
using Newtonsoft.Json;

namespace ProjectMarsAutomationAdvanceTask.Utilities
{
    public static class SearchSkillDataReader
    {
        public static T Read<T>(string fileName, string testCaseName)
        {
            var basePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "TestData",
                fileName
            );

            var jsonData = File.ReadAllText(basePath);

            var jsonObject = JsonConvert.DeserializeObject<dynamic>(jsonData);

            return JsonConvert.DeserializeObject<T>(
                jsonObject[testCaseName].ToString()
            );
        }
    }
}
