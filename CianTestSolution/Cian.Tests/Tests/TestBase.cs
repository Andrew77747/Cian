using System;
using Cian.Framework.Tools;
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
        //public BasePage BasePage;
        //public TopMenu TopMenu;
        //public AuthorizationPage AuthorizationPage;

        public TestBase()
        {
            Manager = new WebDriverManager();
            Settings = new ConfigurationManager().GetSettings();
            //BasePage = new BasePage(Manager);
            //TopMenu = new TopMenu(Manager);
            //AuthorizationPage = new AuthorizationPage(Manager, Settings);
        }

        //[SetUp]
        //public void StartUpTest()
        //{
        //    //BasePage.CloseAlert();
        //    //_authorizationPage.OpenAuthorizationPage();
        //}

        [TearDown]
        public void TeardownTest()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                var screenshot = new ScreenshotMaker(Manager.Driver, TestContext.CurrentContext.Test.Name);
                Console.WriteLine("The screen shot was made into " + screenshot.Path);
                TestContext.AddTestAttachment(screenshot.Path);
            }

            //Manager.Dispose();
            Manager.ClearCookies();
        }

        [OneTimeTearDown]
        public void Stop()
        {
            Manager.Dispose();
        }
    }
}