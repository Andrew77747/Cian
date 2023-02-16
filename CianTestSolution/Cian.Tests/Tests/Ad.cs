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
        private readonly RentRoomPage _rentRoomPage;
        private readonly LoginModal _loginModal;

        public Ad()
        {
            _header = new Header(Manager);
            _adPage = new AdPage(Manager);
            _saleRoomPage = new SaleRoomPage(Manager);
            _rentRoomPage = new RentRoomPage(Manager);
            _loginModal = new LoginModal(Manager, Settings);
        }

        [SetUp]
        public void StartUp()
        {
            _loginModal.Login();
            _header.ClickPostAdBtn();
            _adPage.ClearAdForm();
        }

        [TearDown]
        public void CleanUp()
        {
            _adPage.ClearAdForm();
        }

        [Test]
        [TestCaseSource(typeof(DataProviders), "OwnerRentRoomAdData")]
        [Description("Проверить, что объявление об аренде комнаты собственником создается")]
        public void CheckRentAdOwnerRoomAd(UrbanRealEstateAdModel data)
        {
            _rentRoomPage.RentAd(AccountType.Owner, RentType.Long, RealEstateType.Living, LivingObjectType.Room);
            _rentRoomPage.EnterAddress(data.Address);
            _rentRoomPage.SetAboutObjectBlock(data.CadastralNumber, data.RoomsForRentCount, data.RoomsType,
                data.RoomArea, data.TotalArea, data.Floor, data.FloorCount, data.TotalRoomCountInFlat, 
                data.Kitchen, data.LoggiasCount, data.BalconiesCount, data.SeparatedWsCount,
                data.CombinedWsCount, data.Repair, data.Pets, data.Children, data.AdvancedOptions);
            _rentRoomPage.SetAboutLivingBuildingForm(data.Name, data.BuildYear, data.HouseType, data.HouseSeries,
                data.CeilingHeight, data.PassengerElevator, data.CargoElevator, data.Ramp, data.GarbageChute, data.Parking);
            _rentRoomPage.SetDescriptionBlock(description: data.Description);
            _rentRoomPage.SetPriceAndDealConditionsLivingUrbanRealEstate_Owner(data.RentPrice, data.CurrencyType, data.BargainCheckbox, 
                data.CommunalPaymentAmount, data.CounterCheckbox, data.BargainPrice, data.BargainConditions, data.Prepayment, 
                data.SelfEmployed, data.OwnerDeposit, data.TenantsType);
        }
    }
}