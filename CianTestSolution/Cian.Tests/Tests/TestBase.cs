using System;
using Cian.Framework.PageObjects.Pages;
using Cian.Framework.Tools;
using Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace Cian.Tests.Tests
{
    [TestFixture]
    public class TestBase
    {
        public WebDriverManager Manager;
        public Appsettings Settings;
        public readonly MainPage _MainPage;

        public TestBase()
        {
            Manager = new WebDriverManager();
            Settings = new ConfigurationManager().GetSettings();
            _MainPage = new MainPage(Manager, Settings);
        }

        [SetUp]
        public void Start()
        {
            _MainPage.OpenMainPage();
        }

        [TearDown]
        public void TeardownTest()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                var screenshot = new ScreenshotMaker(Manager.Driver, TestContext.CurrentContext.Test.Name);
                Console.WriteLine("The screen shot was made into " + screenshot.Path);
                TestContext.AddTestAttachment(screenshot.Path);
            }

            Manager.ClearCookies();
        }

        [OneTimeTearDown]
        public void Stop()
        {
            Manager.Dispose();
        }
    }
}