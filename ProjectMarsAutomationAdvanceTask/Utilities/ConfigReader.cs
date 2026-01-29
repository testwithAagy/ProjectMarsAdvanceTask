using Newtonsoft.Json;

namespace ProjectMarsAutomationAdvanceTask.Config
{
    public static class ConfigReader
    {
        public static TestSettings Settings { get; private set; }

        public static void Initialize()
        {
            var json = File.ReadAllText("testsettings.json");
            Settings = JsonConvert.DeserializeObject<TestSettings>(json);
        }
    }
}
