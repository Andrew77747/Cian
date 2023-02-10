using Cian.Framework.Tools;
using OpenQA.Selenium;

namespace Cian.Framework.PageObjects.Pages.AdsPage.UrbanRealEstate
{
    public class UrbanRealEstatePage : AdPage
    {
        public UrbanRealEstatePage(WebDriverManager manager) : base(manager)
        {
            
        }

        #region Map of Elements
        //TODO Разобраться с селекторами. Сделать их private или protected
        private protected readonly By CadastralNumberInput = By.CssSelector("[name='cadastralNumber']");
        protected readonly By RoomsForSale = By.CssSelector("[name='roomsForCount']");
        protected readonly By CheckBox = By.CssSelector(".cui-checkbox.cui-checkbox_inline");
        protected readonly By TotalAreaApartment = By.CssSelector("[name='urbanTotalArea']");
        protected readonly By Floor = By.CssSelector("[name='floorNumber']");
        protected readonly By FloorCount = By.CssSelector("[name='floorsCount']");
        protected readonly By RoomArea = By.CssSelector("[name='allRoomsArea']");
        protected readonly By LivingArea = By.CssSelector("[name='livingArea']");
        protected readonly By _urbanTotalAreaRoom = By.CssSelector("[name='urbanTotalAreaRoom']");
        protected readonly By KitchenArea = By.CssSelector("[name='kitchenArea']");
        protected readonly By _roomsType = By.CssSelector(".rooms-for-count__type-item");
        protected readonly By _roomArea = By.CssSelector("[name='roomArea']");
        protected readonly By _allRoomArea = By.CssSelector("[name='allRoomsArea']");
        protected readonly By _roomsCount = By.CssSelector("[name='roomsCount']");

        protected readonly By _materialClassAndSeriesDropdown = By.CssSelector(".material-type-and-series");
        protected readonly By _buildingNameInput = By.CssSelector("[name='building.name']");
        protected readonly By _buildYearInput = By.CssSelector("[name='building.buildYear']");
        protected readonly By _ceilingHeightInput = By.CssSelector("[name='building.ceilingHeight']");
        protected readonly By _garbageChuteCheckbox = By.XPath("//*[@name='building.hasGarbageChute']/../../..");
        protected readonly By _moreAboutBuildingParameters = By.XPath("//*[@id='about-building']//*[text()='Больше параметров']");

        protected readonly By _moreRentParameters = By.XPath("//*[@class='cian-af-expander__toggle']//*[text()='Больше параметров']");
        protected readonly By additionalCheckbox = By.XPath(".additional-simple-form .cui-checkbox");



        #endregion

        public void SetAboutLivingBuildingForm(string houseType, string passengerLiftsCount, string cargoLiftsCount, 
            string buildingName, string buildYear, string ceilingHigh, string hasRamp, string checkboxValue, string parkingType)
        {
            Wrapper.ClickElement(_materialClassAndSeriesDropdown);
            Wrapper.FindElement(By.XPath($"//*[@class='material-type-and-series']//li[text()='{houseType}']")).Click();

            Wrapper.FindElement(By.XPath($"//*[@name='building.passengerLiftsCount']/*[text()='{passengerLiftsCount}']")).Click();

            Wrapper.FindElement(By.XPath($"//*[@name='building.cargoLiftsCount']/*[text()='{cargoLiftsCount}']")).Click();

            if(Wrapper.IsElementDisplayed(_moreAboutBuildingParameters))
                Wrapper.ClickElement(_moreAboutBuildingParameters);

            Wrapper.TypeAndSend(_buildingNameInput, buildingName);

            Wrapper.TypeAndSend(_buildYearInput, buildYear);

            Wrapper.TypeAndSend(_ceilingHeightInput, ceilingHigh);

            Wrapper.FindElement(By.XPath($"//*[@name='hasRamp']/*[text()='{hasRamp}']")).Click();

            if(Wrapper.IsAttributeContainsValue(_garbageChuteCheckbox, "class", "checked"))
                Wrapper.ClickElement(_garbageChuteCheckbox);
            switch (checkboxValue)
            {
                case "Yes":
                    Wrapper.ClickElement(_garbageChuteCheckbox);
                    break;
                case "No":
                    break;
            }

            Wrapper.FindElement(By.XPath($"//*[@name='building.parking.type']/*[text()='{parkingType}']")).Click();
        }

        public void PetsAllowed(string petsAllowed)
        {
            Wrapper.FindElement(By.XPath($"//*[@data-mark-model-name='ad.petsAllowed']/*[text()='{petsAllowed}']")).Click();
        }

        public void ChildrenAllowed(string childrenAllowed)
        {
            Wrapper.FindElement(By.XPath($"//*[@data-mark-model-name='ad.childrenAllowed']/*[text()='{childrenAllowed}']")).Click();
        }

        public void SetRentBlock(string[] additionally, string petsAllowed = null, string childrenAllowed = null)
        {
            Wrapper.ClickElement(_moreRentParameters);

            var additionalCheckboxes = Wrapper.FindElements(additionalCheckbox);

            foreach (var checkbox in additionalCheckboxes)
            {
                if(Wrapper.IsAttributeContainsValue(checkbox, "class", "checked"))
                    checkbox.Click();
            }

            foreach (var checkbox in additionally)
            {
                Wrapper.FindElement(By.XPath($"//*[@class='additional-simple-form']//*[text()='{checkbox}']")).Click();
            }
        }

        //

        public void SetCadastralNumber(string cadastralNumber)
        {
            Wrapper.TypeAndSend(CadastralNumberInput, cadastralNumber);
        }

        public void SetRoomsForSaleCount(string roomsForSale)
        {
            var roomsForSaleDropdown = Wrapper.FindElement(RoomsForSale);
            roomsForSaleDropdown.Click();
            roomsForSaleDropdown.FindElement(By.XPath($"//*[name='RoomsForSale']/*[text()='{roomsForSale}']")).Click();
        }

        public void SetRoomsType(string roomsType)
        {
            var roomsTypeList = Wrapper.FindElements(_roomsType);

            switch (roomsType)
            {
                case "Смежная":
                    roomsTypeList[0].Click();
                    break;
                case "Изолированная":
                    roomsTypeList[1].Click();
                    break;
            }
        }

        public void SetRoomArea(string roomArea)
        {
            Wrapper.TypeAndSend(_roomArea, roomArea);
        }

        public void SetAllRoomArea(string allRoomsArea)
        {
            Wrapper.TypeAndSend(_allRoomArea, allRoomsArea);
        }

        public void SetFloorCount(string floor, string floorCount)
        {
            Wrapper.TypeAndSend(Floor, floor);

            Wrapper.TypeAndSend(FloorCount, floorCount);
        }

        public void SetRoomsCount(string roomsCount)
        {
            var roomsCountDropdown = Wrapper.FindElement(_roomsCount);
            roomsCountDropdown.Click();
            roomsCountDropdown.FindElement(By.XPath($"//*[text()='{roomsCount}']")).Click();
        }

        public void SetUrbanTotalAreaRoom(string urbanTotalAreaRoom)
        {
            Wrapper.TypeAndSend(_urbanTotalAreaRoom, urbanTotalAreaRoom);
        }

        public void SetKitchenArea(string kitchenArea)
        {
            Wrapper.TypeAndSend(KitchenArea, kitchenArea);
        }

        public void SetLoggiasCount(string loggiasCount)
        {
            Wrapper.FindElement(By.XPath($"//*[@name='loggiasCount']/*[text()='{loggiasCount}']")).Click();
        }

        public void SetBalconiesCount(string balconiesCount)
        {
            Wrapper.FindElement(By.XPath($"//*[@name='balconiesCount']/*[text()='{balconiesCount}']")).Click();
        }

        public void SetSeparateWcsCount(string separateWcsCount)
        {
            Wrapper.FindElement(By.XPath($"//*[@name='separateWcsCount']/*[text()='{separateWcsCount}']")).Click();
        }

        public void SetCombinedWcsCount(string combinedWcsCount)
        {
            Wrapper.FindElement(By.XPath($"//*[@name='combinedWcsCount']/*[text()='{combinedWcsCount}']")).Click();
        }

        public void SetRepairType(string repairType)
        {
            Wrapper.FindElement(By.XPath($"//*[@name='repairType']/*[text()='{repairType}']")).Click();
        }

        public void SetHasPhone(string hasPhone)
        {
            Wrapper.FindElement(By.XPath($"//*[@name='hasPhone']/*[text()='{hasPhone}']")).Click();
        }
    }
}