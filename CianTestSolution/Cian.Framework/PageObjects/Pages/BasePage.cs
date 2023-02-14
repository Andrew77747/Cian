using Cian.Framework.PageObjects.Elements;
using Cian.Framework.Tools;
using OpenQA.Selenium;

namespace Cian.Framework.PageObjects.Pages
{
    public class BasePage : BaseElement
    {
        public BasePage(IWebDriverManager manager) : base(manager)
        {

        }

        #region Map of Elements

        private readonly By _closeCookiesBtn = By.CssSelector("._25d45facb5--button--Cp1dl._25d45facb5--button--IqIpq");

        #endregion

        public void CloseCookies()
        {
            if (Wrapper.IsElementDisplayed(_closeCookiesBtn)) 
                Wrapper.ClickElement(_closeCookiesBtn);
        }
    }
}