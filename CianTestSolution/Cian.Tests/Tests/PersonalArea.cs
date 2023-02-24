using System.Threading;
using Cian.Framework.Data;
using Cian.Framework.Data.Models;
using Cian.Framework.Data.PersonalArea;
using Cian.Framework.Data.RealEstateMainSearch;
using Cian.Framework.PageObjects.Elements;
using Cian.Framework.PageObjects.Elements.RealEstateSearch;
using Cian.Framework.PageObjects.Pages;
using Cian.Framework.PageObjects.Pages.PersonalArea;
using NUnit.Framework;

namespace Cian.Tests.Tests
{
    public class PersonalArea : TestBase
    {
        private readonly Header _header;
        private readonly MainSearch _mainSearch;
        private readonly ResultPage _resultPage;
        private readonly LoginModal _loginModal;
        private readonly MainPersonalAreaPage _mainPersonalAreaPage;
        private readonly SavedSearchesPage _savedSearchesPage;

        public PersonalArea()
        {
            _header = new Header(Manager);
            _mainSearch = new MainSearch(Manager);
            _loginModal = new LoginModal(Manager, Settings);
            _resultPage = new ResultPage(Manager);
            _mainPersonalAreaPage = new MainPersonalAreaPage(Manager);
            _savedSearchesPage = new SavedSearchesPage(Manager);
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
            var resultHeader = _resultPage.GetSearchResultHeader();
            _resultPage.ClickSearchSaveBtn();
            _resultPage.SaveSearchResult(notification: "Каждый день");
            _header.ClickUserAvatar();
            _header.ClickUserMenuItem(UserMenuItem.PersonalArea);
            _header.ClickPersonalAreaLink();
            _mainPersonalAreaPage.ClickSideMenu(SideMenu.SavedSearches);
            Assert.That(_savedSearchesPage.GetSearchCardsTitles().Contains(resultHeader));
            _savedSearchesPage.DeleteAllSavedSearches();
        }
    }
}