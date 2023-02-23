using System.Threading;
using Cian.Framework.Data;
using Cian.Framework.Data.Models;
using Cian.Framework.Data.RealEstateMainSearch;
using Cian.Framework.PageObjects.Elements;
using Cian.Framework.PageObjects.Elements.RealEstateSearch;
using Cian.Framework.PageObjects.Pages;
using NUnit.Framework;

namespace Cian.Tests.Tests
{
    public class PersonalArea : TestBase
    {
        private readonly Header _header;
        private readonly MainSearch _mainSearch;
        private readonly ResultPage _resultPage;
        private readonly LoginModal _loginModal;

        public PersonalArea()
        {
            _header = new Header(Manager);
            _mainSearch = new MainSearch(Manager);
            _loginModal = new LoginModal(Manager, Settings);
            _resultPage = new ResultPage(Manager);
        }

        [SetUp]
        public void StartUp()
        {
            _loginModal.Login();
        }

        [Test, Description("Проверить, что параметры поиска сохраняются в личном кабинете")]
        [TestCaseSource(typeof(DataProviders), "BuyApartmentSearchData")]
        public void CheckSavingSearchParameters(SearchModel data)
        {
            _mainSearch.BuyOrRentApartmentSearch(TabMenuNames.Buy, data.OfferTypeCheckboxes, data.RoomsCount,
                data.ApartmentTypeCheckboxes, data.PriceFrom, data.PriceTill, data.Address);
            _resultPage.ClickSearchSaveBtn();
            _header.GetUserId();
            _header.ClickPersonalAreaLink();

            Thread.Sleep(2000);
        }
    }
}