using Cian.Framework.Tools;
using OpenQA.Selenium;

namespace Cian.Framework.PageObjects.Elements
{
    public class BaseElement
    {
        protected SeleniumWrapper Wrapper;

        public BaseElement(IWebDriverManager manager)
        {
            Wrapper = new SeleniumWrapper(manager.GetDriver(), manager.GetWaiter());
        }

        public bool IsElementActive(IWebElement selector)
        {
            return Wrapper.IsAttributeContainsValue(selector, "class", "active");
        }
    }
}