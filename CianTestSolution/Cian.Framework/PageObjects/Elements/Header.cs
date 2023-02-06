using Cian.Framework.Tools;
using OpenQA.Selenium;

namespace Cian.Framework.PageObjects.Elements
{
    public class Header : BaseElement

    {
        public Header(WebDriverManager manager) : base(manager)
        {

        }

        #region Map of Elements

        private readonly By _loginBtn = By.CssSelector("._25d45facb5--container--nWU6f ._25d45facb5--button--jfWOF");
        private readonly By _userAvatar = By.CssSelector("[data-name='UserAvatar']");
        private readonly By _userId = By.CssSelector("._25d45facb5--full-name--K5jY5");

        #endregion

        public void ClickLoginBtn()
        {
            Wrapper.ClickElement(_loginBtn);
        }

        public string GetUserId()
        {
            Wrapper.ClickElement(_userAvatar);
            return Wrapper.FindElement(_userId).Text;
        }
    }
}