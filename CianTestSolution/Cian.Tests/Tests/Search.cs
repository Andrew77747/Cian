using System.Threading;
using System.Xml.Linq;
using Cian.Framework.Data.Models;
using Cian.Framework.Data.RealEstateMainSearch;
using Cian.Framework.PageObjects.Elements.RealEstateSearch;
using NUnit.Framework;

namespace Cian.Tests.Tests
{
    public class Search : TestBase
    {
        private MainSearch _mainSearch;

        public Search()
        {
            _mainSearch = new MainSearch(Manager);
        }

        [Test, Description("Проверить, что поиск продажи и аренды квартиры успешно выполняется")]
        [TestCaseSource(typeof(DataProviders), "BuyApartmentSearchData")]
        public void CheckApartmentSearch(SearchModel data)
        {
            _mainSearch.BuyOrRentApartmentSearch(TabMenuNames.Buy, data.OfferTypeCheckboxes, data.RoomsCount,
                data.ApartmentTypeCheckboxes, data.PriceFrom, data.PriceTill, data.Address);

            Thread.Sleep(2000);
        }
    }
}