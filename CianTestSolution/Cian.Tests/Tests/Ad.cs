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
            _header.ClickLoginBtn();
            _loginModal.Login();
        }

        [Test]
        [Description("Проверить, что объявление об аренде комнаты собственником создается")]
        public void CheckRentAdOwnerRoomAd(
            [ValueSource(typeof(DataProviders), "OwnerRentRoomAdData")] AboutLivingObject objectData,
            [ValueSource(typeof(DataProviders), "AboutBuildingData")] AboutLivingBuilding buildingData)
        {
            Thread.Sleep(2000);
            _header.ClickPostAdBtn();
            _rentRoomPage.RentAd(AccountType.Owner, RentType.Long, RealEstateType.Living, LivingObjectType.Room);
            _rentRoomPage.EnterAddress(objectData.Address);
            Thread.Sleep(1000);
            _rentRoomPage.SetAboutObjectBlock(objectData.CadastralNumber, objectData.RoomsForRentCount, objectData.RoomsType,
                objectData.RoomArea, objectData.TotalArea, objectData.Floor, objectData.FloorCount, 
                objectData.TotalRoomCountInFlat, objectData.Kitchen, objectData.LoggiasCount, objectData.BalconiesCount,
                objectData.SeparatedWsCount, objectData.CombinedWsCount, objectData.Repair, objectData.Pets, 
                objectData.Children, objectData.AdvancedOptions);
            _rentRoomPage.SetAboutLivingBuildingForm(buildingData.Name, buildingData.BuildYear, buildingData.HouseType,
                buildingData.HouseSeries, buildingData.CeilingHeight, buildingData.PassengerElevator,
                buildingData.CargoElevator, buildingData.Ramp, buildingData.GarbageChute, buildingData.Parking);

            _rentRoomPage.SetDescriptionBlock(description: objectData.Description);

            Thread.Sleep(2000);
        }
    }
}