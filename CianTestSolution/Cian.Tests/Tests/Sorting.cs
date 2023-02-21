using Cian.Framework.Data.Models;
using Cian.Framework.Data.RealEstateMainSearch;
using Cian.Framework.PageObjects.Elements.RealEstateSearch;
using Cian.Framework.PageObjects.Pages;
using NUnit.Framework;

namespace Cian.Tests.Tests
{
    public class Sorting : TestBase
    {
        private ResultPage _resultPage;
        private MainSearch _mainSearch;

        public Sorting()
        {
            _resultPage = new ResultPage(Manager);
            _mainSearch = new MainSearch(Manager);
        }

        [Test, Description("Проверить сортировку цену по возрастанию")]
        [TestCaseSource(typeof(DataProviders), "BuyApartmentSearchData")]
        public void CheckSortingPriceAsc(SearchModel data)
        {
            _mainSearch.BuyOrRentApartmentSearch(TabMenuNames.Buy, data.OfferTypeCheckboxes, data.RoomsCount,
                data.ApartmentTypeCheckboxes, data.PriceFrom, data.PriceTill, data.Address);
            var x =_resultPage.GetAllPrices();
        }
    }
}