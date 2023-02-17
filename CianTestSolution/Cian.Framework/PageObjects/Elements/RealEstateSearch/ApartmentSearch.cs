using Cian.Framework.Data.RealEstateMainSearch;
using Cian.Framework.Tools;
using OpenQA.Selenium;

namespace Cian.Framework.PageObjects.Elements.RealEstateSearch
{
    public class ApartmentSearch : MainSearch
    {
        public ApartmentSearch(WebDriverManager manager) : base(manager)
        {

        }

        #region Map of Elements

        

        private readonly By _offerTypeCheckBox = By.CssSelector("._025a50318d--checkbox--DGtQp");

        #endregion

        public void BuyOrRentApartmentSearch(string[] offerTypeCheckboxes, string roomsCount, string[] apartmentTypeCheckboxes,
            string priceFrom, string priceTill, string address)
        {
            ClickTabMenu(TabMenuNames.Buy);
            SetOfferTypeSearch("Жилая", offerTypeCheckboxes);
            SetRoomsCount(roomsCount, apartmentTypeCheckboxes);
            SetPrice(priceFrom, priceTill);
            SetAddress(address);
        }
    }
}