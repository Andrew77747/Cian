using System.Threading;
using Cian.Framework.Data.Announcement;
using Cian.Framework.Data.Models;
using Cian.Framework.PageObjects.Elements;
using Cian.Framework.PageObjects.Pages;
using Cian.Framework.PageObjects.Pages.AdsPage;
using Cian.Framework.PageObjects.Pages.AdsPage.UrbanRealEstate;
using NUnit.Framework;

namespace Cian.Tests.Tests
{
    public class Ad : TestBase
    {
        private readonly Header _header;
        private readonly AdPage _adPage;
        private readonly SaleRoomPage _saleRoomPage;

        public Ad()
        {
            _header = new Header(Manager);
            _adPage = new AdPage(Manager);
            _saleRoomPage = new SaleRoomPage(Manager);
        }

        [Test, TestCaseSource(typeof(DataProviders), "OwnerSaleLivingApartmentAdData")]
        [Description("Проверить, что объявление о продажи квартиры собственником создается")]
        public void CheckSaleAdOwnerApartmentAnnouncement(OwnerSaleLivingApartmentAd data)
        {
            _header.ClickPostAdBtn();
            _adPage.SaleAd(AccountType.Owner, RealEstateType.Living, LivingObjectType.Apartment);
            _saleRoomPage.EnterAddress(data.Address);

            Thread.Sleep(2000);
        }
    }
}