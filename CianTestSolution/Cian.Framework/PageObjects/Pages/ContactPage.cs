using Cian.Framework.Tools;
using OpenQA.Selenium;

namespace Cian.Framework.PageObjects.Pages
{
    public class ContactPage : BasePage
    {
        private string _contactModalHeaderText = "Напишите нам";

        public ContactPage(WebDriverManager manager) : base(manager)
        {
            
        }

        #region Map of Elements

        private readonly By _contactForm = By.CssSelector("[data-testid='questionLayout']");
        private readonly By contactModalHeader = 
            By.CssSelector("._105329b597--color_black_100--kPHhJ._105329b597--lineHeight_7u--BCGBP");

        #endregion

        public void SwitchToContactWindow()
        {
            Wrapper.SwitchWindowLast();
        }

        public bool IsContactPageOpened()
        {
            return Wrapper.IsElementDisplayed(_contactForm) &&
                   Wrapper.IsTextEqual(contactModalHeader, _contactModalHeaderText);
        }

        public void CloseAndSwitchPage()
        {
            Wrapper.CloseCurrentWindow();
            Wrapper.SwitchWindowByNumber(0);
        }
    }
}