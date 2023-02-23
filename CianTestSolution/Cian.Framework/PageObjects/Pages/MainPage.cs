using Cian.Framework.Tools;
using Infrastructure.Settings;
using OpenQA.Selenium;

namespace Cian.Framework.PageObjects.Pages
{
    public class MainPage : BasePage
    {
        private Appsettings _settings;

        public MainPage(IWebDriverManager manager, Appsettings settings) : base(manager)
        {
            _settings = settings;
        }

        #region Map of Elements

        

        #endregion

        public void OpenMainPage()
        {
            Wrapper.NavigateToUrl(_settings.BaseUrl);
        }
    }
}