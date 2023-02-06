using System;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;

namespace Cian.Framework.Tools
{
    public class ScreenshotMaker
    {
        private readonly IWebDriver _driver;
        public string Path { get; private set; }

        public ScreenshotMaker(IWebDriver driver, string testName)
        {
            _driver = driver;

            TakeScreenShotForCurrent(testName);
        }

        private string GetFilePath()
        {
            return System.IO.Path.Combine(Environment.CurrentDirectory, "Reports", "Screenshots");
        }

        public string GetFileName(string basePath, string testName)
        {
            Path = System.IO.Path.Combine(basePath, $"{testName}_{DateTime.Now:yyyyMMdd_HHmmss}.jpg");

            return Path;
        }

        public void TakeScreenShotForCurrent(string testName)
        {
            var path = GetFilePath();

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            _driver.TakeScreenshot().SaveAsFile(GetFileName(path, testName));
        }
    }
}