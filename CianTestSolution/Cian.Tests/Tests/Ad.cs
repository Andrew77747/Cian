using System.Threading;
using Cian.Framework.Data.Announcement;
using Cian.Framework.Data.Models;
using Cian.Framework.PageObjects.Elements;
using Cian.Framework.PageObjects.Pages;
using Cian.Framework.PageObjects.Pages.AdsPage;
using NUnit.Framework;

namespace Cian.Tests.Tests
{
    public class Ad : TestBase
    {
        private readonly Header _header;
        private readonly AdPage _adPage;
        private readonly OwnerSaleLivingApartmentAdPage _ownerSaleLivingApartmentAdPage;

        public Ad()
        {
            _header = new Header(Manager);
            _adPage = new AdPage(Manager);
            _ownerSaleLivingApartmentAdPage = new OwnerSaleLivingApartmentAdPage(Manager);
        }

        [Test, TestCaseSource(typeof(DataProviders), "OwnerSaleLivingApartmentAdData")]
        [Description("Проверить, что объявление о продажи квартиры собственником создается")]
        public void CheckSaleAdOwnerApartmentAnnouncement(OwnerSaleLivingApartmentAd data)
        {
            _header.ClickPostAdBtn();
            _adPage.SaleAd(AccountType.Owner, RealEstateType.Living, LivingObjectType.Apartment);
            _ownerSaleLivingApartmentAdPage.EnterAddress(data.Address);

            Thread.Sleep(2000);
        }
    }
}