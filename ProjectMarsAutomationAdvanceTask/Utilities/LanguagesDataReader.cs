using Newtonsoft.Json;
using System.IO;

namespace ProjectMarsAutomationAdvanceTask.Utilities
{
    public static class LanguagesDataReader
    {
        public static T Read<T>(string fileName, string key)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "TestData", fileName);
            var json = File.ReadAllText(filePath);

            var allData = JsonConvert.DeserializeObject<Dictionary<string, T>>(json);

            return allData[key];
        }
    }
}
