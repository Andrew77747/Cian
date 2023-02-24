using Cian.Framework.Tools;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace Cian.Framework.PageObjects.Pages
{
    public class ResultPage : BasePage
    {
        public ResultPage(IWebDriverManager manager) : base(manager)
        {
            
        }

        #region Map ofElements

        private readonly By _searchResultHeader =
            By.CssSelector("[data-name='Breadcrumbs'] ._93444fe79c--color_black_100--kPHhJ");
        private readonly By _mainPrice = By.CssSelector("[data-mark='MainPrice']");
        private readonly By _sortBtn = By.CssSelector("[data-mark='SortDropdownButton']");
        private readonly By _searchSaveBtn = By.CssSelector("[data-name='SaveSearchButtonContainer']");
        private readonly By _modalSearchSaveBtn = By.CssSelector("[role='dialog'] ._93444fe79c--button--Cp1dl");
        private readonly By _sortingPreloader = By.XPath("//div[contains(@class, 'preloader')]");
        private readonly By _searchNameInput = By.CssSelector("[name='title']");
        private readonly By _searchEmailInput = By.CssSelector("[name='email']");
        private readonly By _notificationDropdown = By.CssSelector("[aria-haspopup='listbox']");
        private readonly By _successSavingCloseModalBtn = By.CssSelector("[aria-label='Закрыть']");

        #endregion

        public void ClickSearchSaveBtn()
        {
            Wrapper.ClickElement(_searchSaveBtn);
        }

        public void SaveSearchResult(string searchName = null, string notification = null, string email = null,
            string[] checkboxes = null)
        {
            if (searchName is not null)
                Wrapper.ClearTypeAndSend(_searchNameInput, searchName);

            if (notification is not null)
            {
                Wrapper.ClickElement(_notificationDropdown);
                Wrapper.ClickElement(By.XPath($"//*[@role='listbox']//*[text()='{notification}']"));
            }

            if (email is not null)
                Wrapper.ClearTypeAndSend(_searchEmailInput, email);

            if (checkboxes is not null)
            {
                foreach (var checkbox in checkboxes)
                {
                    Wrapper.ClickElement(By.XPath($"//*[@name='{checkbox}']/.."));
                }
            }

            Wrapper.ClickElement(_modalSearchSaveBtn);
            Wrapper.ClickElement(_successSavingCloseModalBtn);
        }

        public string GetSearchResultHeader()
        {
            return Wrapper.FindElement(_searchResultHeader).Text;
        }

        public List<string> GetAllPrices()
        {
            var allPrices = Wrapper.FindElements(_mainPrice);
            var allPricesList = new List<string>();

            foreach (var price in allPrices)
            {
                allPricesList.Add(price.Text.Replace(" ₽", "").Replace(" ", ""));
            }

            return allPricesList;
        }

        public void SelectSortingType(string sortName)
        {
            Wrapper.ClickElement(_sortBtn);

            Wrapper.ClickElement(By.XPath($"//*[contains(@class,'HOb5uf1a1aIy_EPL ') and text()='{sortName}']"));

            Wrapper.WaitForElementNotDisplayed(_sortingPreloader);
        }

        public bool IsSortingAskCorrect(List<string> prices)
        {
            return Wrapper.IsSortingPriceAskRightStringToIntList(prices);//todo проверить, что с обратной сортировкой будет ошибка
        }
    }
}