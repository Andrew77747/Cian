using System.Collections.Generic;
using Cian.Framework.Tools;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Cian.Framework.PageObjects.Pages
{
    public class ResultPage : BasePage
    {
        public ResultPage(WebDriverManager manager) : base(manager)
        {
            
        }

        #region Map ofElements

        private readonly By _searchResultHeader =
            By.CssSelector("[data-name='Breadcrumbs'] ._93444fe79c--color_black_100--kPHhJ");
        private readonly By _mainPrice = By.CssSelector("[data-mark='MainPrice']");

        #endregion

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
    }
}