using Newtonsoft.Json;
using ProjectMarsAutomationAdvanceTask.Models;
using System.IO;

namespace ProjectMarsAutomationAdvanceTask.Utilities
{
    public static class JsonDataReader
    {
        public static AboutInfoTestData GetAboutInfoData(string path)
        {
            var json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<AboutInfoTestData>(json);
        }
    }
}
