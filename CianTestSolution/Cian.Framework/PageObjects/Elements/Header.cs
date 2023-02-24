using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using Cian.Framework.Data.Submenu;
using Cian.Framework.Tools;
using OpenQA.Selenium;

namespace Cian.Framework.PageObjects.Elements
{
    public enum TopMenu
    {
        Rent,
        Sale,
        NewBuildings,
        HouseAndGrounds,
        Commercial,
        Mortgage,
        Services
    }

    public enum UserMenuItem
    {
        PersonalArea,
        PromoCode,
        AddAd,
        Mortgage,
        Help,
        Exit
    }

    public class Header : BaseElement

    {
        public Header(WebDriverManager manager) : base(manager)
        {

        }

        #region Map of Elements

        private readonly By _loginBtn = By.CssSelector("._25d45facb5--container--nWU6f ._25d45facb5--button--jfWOF");
        private readonly By _userAvatar = By.CssSelector("[data-name='UserAvatar']");
        private readonly By _userId = By.CssSelector("._25d45facb5--full-name--K5jY5");
        private readonly By _logo = By.CssSelector("[data-name='Logo']");
        private readonly By _menuItem = By.CssSelector("[data-name='NavBar'] li");
        private readonly By _subMenuItem = By.CssSelector("[data-name='DropdownMainMenu'] ul li");
        private readonly By _postAdBtn = By.CssSelector("._25d45facb5--place--MmHod ._25d45facb5--button--Cp1dl");
        private readonly By _userMenuItem = By.CssSelector("._25d45facb5--menu-list--MPsCw li");
        private readonly By _userMenuDropdown = By.CssSelector("._25d45facb5--container--X_76T");
        private readonly By _personalAreaLink = By.LinkText("Личный кабинет");

        #endregion

        public void ClickUserMenuItem(UserMenuItem item)
        {
            Wrapper.WaitElementDisplayed(_userMenuDropdown);

            var userMenuItemsList = Wrapper.FindElements(_userMenuItem);

            switch (item)
            {
                case UserMenuItem.PersonalArea:
                    userMenuItemsList[0].Click();
                    break;
                case UserMenuItem.PromoCode:
                    userMenuItemsList[1].Click();
                    break;
                case UserMenuItem.AddAd:
                    userMenuItemsList[2].Click();
                    break;
                case UserMenuItem.Mortgage:
                    userMenuItemsList[3].Click();
                    break;
                case UserMenuItem.Help:
                    userMenuItemsList[4].Click();
                    break;
                case UserMenuItem.Exit:
                    userMenuItemsList[4].Click();
                    break;
            }
        }

        public void ClickPersonalAreaLink()
        {
            Wrapper.ClickElement(_personalAreaLink);
        }

        public void ClickPostAdBtn()
        {
            Wrapper.ClickElement(_postAdBtn);
        }

        public void ClickLoginBtn()
        {
            Wrapper.ClickElement(_loginBtn);
        }

        public string GetUserId()
        {
            ClickUserAvatar();
            return Wrapper.FindElement(_userId).Text;
        }

        public void ClickUserAvatar()
        {
            Wrapper.ClickElement(_userAvatar);
        }

        public void ClickTopMenuItem(TopMenu item)
        {
            var topMenuItems = Wrapper.FindElements(_menuItem);

            switch (item)
            {
                case TopMenu.Rent:
                    topMenuItems[0].Click();
                    break;
                case TopMenu.Sale:
                    topMenuItems[1].Click();
                    break;
                case TopMenu.NewBuildings:
                    topMenuItems[2].Click();
                    break;
                case TopMenu.HouseAndGrounds:
                    topMenuItems[3].Click();
                    break;
                case TopMenu.Commercial:
                    topMenuItems[4].Click();
                    break;
                case TopMenu.Mortgage:
                    topMenuItems[5].Click();
                    break;
                case TopMenu.Services:
                    topMenuItems[6].Click();
                    break;
            }
        }

        public bool IsTopMenuElementActive(TopMenu item)
        {
            var topMenuItems = Wrapper.FindElements(_menuItem);

            bool result = false;

            switch (item)
            {
                case TopMenu.Rent:
                    if (IsElementActive(topMenuItems[0]))
                        result = true;
                    break;
                case TopMenu.Sale:
                    if (IsElementActive(topMenuItems[1]))
                        result = true;
                    break;
                case TopMenu.NewBuildings:
                    //if (IsElementActive(topMenuItems[2]))
                    if (Wrapper.IsAttributeContainsValue(topMenuItems[2], "class", "active")) //Оставить как? 
                        result = true;
                    break;
                case TopMenu.HouseAndGrounds:
                    if (IsElementActive(topMenuItems[3]))
                        result = true;
                    break;
                case TopMenu.Commercial:
                    if (IsElementActive(topMenuItems[4]))
                        result = true;
                    break;
                case TopMenu.Mortgage:
                    if (IsElementActive(topMenuItems[5]))
                        result = true;
                    break;
                case TopMenu.Services:
                    if (IsElementActive(topMenuItems[6]))
                        result = true;
                    break;
            }

            return result;
        }

        public void HoverOnTopMenu(TopMenu item)
        {
            var topMenuItems = Wrapper.FindElements(_menuItem);

            switch (item)
            {
                case TopMenu.Rent:
                    Wrapper.HoverMouseOnElement(topMenuItems[0]);
                    Thread.Sleep(1000);
                    break;
                case TopMenu.Sale:
                    Wrapper.HoverMouseOnElement(topMenuItems[1]);
                    break;
                case TopMenu.NewBuildings:
                    Wrapper.HoverMouseOnElement(topMenuItems[2]);
                    break;
                case TopMenu.HouseAndGrounds:
                    Wrapper.HoverMouseOnElement(topMenuItems[3]);
                    break;
                case TopMenu.Commercial:
                    Wrapper.HoverMouseOnElement(topMenuItems[4]);
                    break;
                case TopMenu.Mortgage:
                    Wrapper.HoverMouseOnElement(topMenuItems[5]);
                    break;
                case TopMenu.Services:
                    Wrapper.HoverMouseOnElement(topMenuItems[6]);
                    break;
            }
        }

        public void ClickSubMenu(TopMenu item, string subMenuName)
        {
            HoverOnTopMenu(item);
            Wrapper.FindElement(By.XPath($"//*[contains(@class,'_25d45facb5--content--IHuY4') and text()='{subMenuName}']")).Click();
        }

        public List<string> GetSubMenuNames(TopMenu name)
        {
            HoverOnTopMenu(name);

            var subMenuItems = Wrapper.FindElements(_subMenuItem);

            List<string> subMenuNames = new List<string>();

            foreach (var item in subMenuItems)
            {
                subMenuNames.Add(item.FindElement(By.CssSelector("a")).Text.Replace("\r\nНовое", ""));
            }

            return subMenuNames;
        }

        //public List<string> GetSubMenuNames(TopMenu name)
        //{
        //    HoverOnTopMenu(name);

        //    var subMenuItems = Wrapper.FindElements(_subMenuItem);

        //    List<string> subMenuNames = new List<string>();
        //    List<string> subMenuNames2 = new List<string>();//

        //    foreach (var item in subMenuItems)
        //    {
        //        subMenuNames.Add(item.FindElement(By.CssSelector("a")).Text.Replace("\r\nНовое", ""));
        //    }

        //    foreach (var x in subMenuNames)
        //    {

        //        subMenuNames2.Add(x.Replace("\r\nНовое", ""));
        //    }

        //    return subMenuNames2;
        //}
    }
}