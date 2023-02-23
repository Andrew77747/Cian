using Cian.Framework.Tools;
using OpenQA.Selenium;

namespace Cian.Framework.PageObjects.Pages.PersonalArea
{
    public class MainPersonalAreaPage : BasePage
    {
        public MainPersonalAreaPage(IWebDriverManager manager) : base(manager)
        {
            
        }

        public void ClickSideMenu(string sideMenuName)
        {
            Wrapper.ClickElement(By.XPath($"//li[@data-name='SidebarMenuItem']//*[text()='{sideMenuName}']"));
        }
    }
}