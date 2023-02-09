using Cian.Framework.Tools;
using Infrastructure.Settings;
using OpenQA.Selenium;

namespace Cian.Framework.PageObjects.Elements
{
    public class LoginModal : BaseElement
    {
        private Appsettings _settings;

        public LoginModal(WebDriverManager manager, Appsettings settings) : base(manager)
        {
            _settings = settings;
        }

        #region Map of Elements

        private readonly By _enterAnotherWayBtn = By.CssSelector(
            "[data-name='AuthenticationModal'] button._25d45facb5--button--jfWOF");
        private readonly By _userNameInput = By.CssSelector("[name='username']");
        private readonly By _continueBtn = By.CssSelector("[data-name='ContinueAuthBtn']");
        private readonly By _passwordInput = By.CssSelector("[name='password']");
        private readonly By _loginBtn = By.CssSelector("[data-name='ContinueAuthBtn']");
        private readonly By _helpLink = By.CssSelector("._25d45facb5--link-content--jk8CC");

        #endregion


        public void Login()
        {
            Wrapper.ClickElement(_enterAnotherWayBtn);
            Wrapper.TypeAndSend(_userNameInput, _settings.Login);
            Wrapper.ClickElement(_continueBtn);
            Wrapper.TypeAndSend(_passwordInput, _settings.Password);
            Wrapper.ClickElement(_loginBtn);
        }

        public void ClickHelpLink()
        {
            Wrapper.ClickElement(_helpLink);
        }
    }
}