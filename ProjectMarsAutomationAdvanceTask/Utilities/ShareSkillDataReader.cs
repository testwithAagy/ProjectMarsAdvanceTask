using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace ProjectMarsAutomationAdvanceTask.Utilities
{
    public static class ShareSkillDataReader
    {
        public static T Read<T>(string fileName, string key)
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
    }
}
