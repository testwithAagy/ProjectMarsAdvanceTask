using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace ProjectMarsAutomationAdvanceTask.Utilities
{
    public static class SkillsDataReader
    {
        public static T Read<T>(string fileName, string key)
        {
            try
            {
                string basePath = AppContext.BaseDirectory + "/TestData/";
                string filePath = Path.Combine(basePath, fileName);

                if (!File.Exists(filePath))
                    throw new FileNotFoundException($"Test data file not found: {filePath}");

                var jsonText = File.ReadAllText(filePath);
                var jsonObject = JsonConvert.DeserializeObject<Dictionary<string, T>>(jsonText);

                if (!jsonObject.ContainsKey(key))
                    throw new KeyNotFoundException($"Key '{key}' not found in {fileName}");

                return jsonObject[key];
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading test data: {ex.Message}");
                throw;
            }
        }
       

    }
}
