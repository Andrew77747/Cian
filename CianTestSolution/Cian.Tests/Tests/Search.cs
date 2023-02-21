using Cian.Framework.Data.Models;
using Cian.Framework.Data.RealEstateMainSearch;
using Cian.Framework.PageObjects.Elements.RealEstateSearch;
using NUnit.Framework;
using System.Threading;
using Cian.Framework.PageObjects.Pages;

namespace Cian.Tests.Tests
{
    public class Search : TestBase
    {
        private MainSearch _mainSearch;
        private ResultPage _resultPage;
        private string _searchResult = "Купить 1, 2, 3, 4-комнатную квартиру, квартиру свободной планировки," +
                                       " квартиру-студию в Санкт-Петербурге рядом с метро Лесная";

        public Search()
        {
            _mainSearch = new MainSearch(Manager);
            _resultPage = new ResultPage(Manager);
        }

        [Test, Description("Проверить, что поиск продажи и аренды квартиры успешно выполняется")]
        [TestCaseSource(typeof(DataProviders), "BuyApartmentSearchData")]
        public void CheckApartmentSearch(SearchModel data)
        {
            _mainSearch.BuyOrRentApartmentSearch(TabMenuNames.Buy, data.OfferTypeCheckboxes, data.RoomsCount,
                data.ApartmentTypeCheckboxes, data.PriceFrom, data.PriceTill, data.Address);
            Assert.That(_searchResult.Equals(_resultPage.GetSearchResultHeader()), "Result headers are not equals");
        }
    }
}