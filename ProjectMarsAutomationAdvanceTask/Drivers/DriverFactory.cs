using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace ProjectMarsAutomationAdvanceTask.Drivers
{
    public static class BrowserManager
    {
        private static IWebDriver _driver;

        public static IWebDriver InitializeDriver(bool headless = false)
        {
            if (_driver != null)
                return _driver;

            new DriverManager().SetUpDriver(new ChromeConfig());

            var options = new ChromeOptions();
            if (headless)
            {
                options.AddArgument("--headless=new"); 
            }

            _driver = new ChromeDriver(options);
            _driver.Manage().Window.Maximize();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            return _driver;
        }

        public static IWebDriver GetDriver() => _driver;

        public static void QuitDriver()
        {
            if (_driver != null)
            {
                _driver.Quit();
                _driver.Dispose();
                _driver = null;
            }
        }
    }
}
