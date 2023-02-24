using System.Collections.Generic;
using Cian.Framework.Tools;
using OpenQA.Selenium;

namespace Cian.Framework.PageObjects.Pages.PersonalArea
{
    public class SavedSearchesPage : MainPersonalAreaPage
    {
        public SavedSearchesPage(IWebDriverManager manager) : base(manager)
        {
            
        }

        #region Map of Elements

        private readonly By _searchCardTitle = By.CssSelector("[data-name='SearchCardsItem'] a");
        private readonly By _searchCardRemoveBtn = By.CssSelector("._2184ee19b6--remove--TCkKw");
        private readonly By _deleteSavedSearchBtn = By.XPath("//*[@type='button' and text()='Удалить']");

        #endregion

        public List<string> GetSearchCardsTitles()
        {
            var cardTitles = Wrapper.FindElements(_searchCardTitle);
            var cardTitlesText = new List<string>();

            foreach (var titles in cardTitles)
            {
                cardTitlesText.Add(titles.Text);
            }

            return cardTitlesText;
        }

        public void DeleteAllSavedSearches()
        {
            var deleteCardsBtn = Wrapper.FindElements(_searchCardRemoveBtn);

            foreach (var btn in deleteCardsBtn)
            {
                btn.Click();
                Wrapper.ClickElement(_deleteSavedSearchBtn);
            }
        }
    }
}