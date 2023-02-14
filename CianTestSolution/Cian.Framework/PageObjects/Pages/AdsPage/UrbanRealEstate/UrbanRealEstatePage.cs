using System.Collections.Generic;
using System.Threading;
using Cian.Framework.Tools;
using NUnit.Framework;
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
        protected readonly By RoomsCount = By.CssSelector("[name='roomsForCount']");
        protected readonly By CheckBox = By.CssSelector(".cui-checkbox.cui-checkbox_inline");
        protected readonly By UrbanTotalArea = By.CssSelector("[name='urbanTotalArea']");
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
        protected readonly By _roomsForRentTotalCount = By.CssSelector("[name='roomsForRentTotalCount']");
        protected readonly By _houseSeries = By.CssSelector("[name='building.series']");

        protected readonly By _typeHouseDropdown = By.XPath("//*[@name='building.materialType']");
        protected readonly By _buildingNameInput = By.CssSelector("[name='building.name']");
        protected readonly By _buildYearInput = By.CssSelector("[name='building.buildYear']");
        protected readonly By _ceilingHeightInput = By.CssSelector("[name='building.ceilingHeight']");
        protected readonly By _garbageChuteCheckbox = By.XPath("//*[@name='building.hasGarbageChute']/../../..");
        protected readonly By _moreAboutBuildingParameters = By.XPath("//*[@id='about-building']//*[text()='Больше параметров']");

        protected readonly By _moreRentParameters = By.XPath("//*[@class='cian-af-expander__toggle']//*[text()='Больше параметров']");
        protected readonly By additionalCheckbox = By.CssSelector(".additional-simple-form .cui-checkbox");



        #endregion

        public void SetAboutLivingBuildingForm(string buildingName, string buildYear, string houseType, string houseSeries,
            string ceilingHigh, string passengerLiftsCount, string cargoLiftsCount, string hasRamp, 
            string garbageChuteCheckbox, string parkingType)
        {
            Wrapper.TypeAndSend(_buildingNameInput, buildingName);

            Wrapper.TypeAndSend(_buildYearInput, buildYear);

            var typeHouseDropdown = Wrapper.FindElement(_typeHouseDropdown);
            typeHouseDropdown.Click();
            typeHouseDropdown.FindElement(By.XPath($"//li[text()='{houseType}']")).Click();

            Wrapper.TypeAndSend(_houseSeries, houseSeries);

            if (Wrapper.IsElementDisplayed(_moreAboutBuildingParameters))
                Wrapper.ClickElement(_moreAboutBuildingParameters);

            Wrapper.TypeAndSend(_ceilingHeightInput, ceilingHigh);

            var tabElement = Wrapper.FindElement(By.XPath(
                $"//*[@name='building.passengerLiftsCount']/*[text()='{passengerLiftsCount}']"));
            if (Wrapper.IsAttributeNotContainsValue(tabElement, "class", "active"))
            {
                tabElement.Click();
            }

            tabElement = Wrapper.FindElement(By.XPath($"//*[@name='building.cargoLiftsCount']/*[text()='{cargoLiftsCount}']"));
            if (Wrapper.IsAttributeNotContainsValue(tabElement, "class", "active"))
            {
                tabElement.Click();
            }

            Wrapper.FindElement(By.XPath($"//*[@name='hasRamp']/*[text()='{hasRamp}']")).Click();

            if (Wrapper.IsAttributeContainsValue(_garbageChuteCheckbox, "class", "checked"))
                Wrapper.ClickElement(_garbageChuteCheckbox);
            switch (garbageChuteCheckbox)
            {
                case "Есть":
                    Wrapper.ClickElement(_garbageChuteCheckbox);
                    break;
                case "Нет":
                    break;
            }

            tabElement = Wrapper.FindElement(By.XPath($"//*[@name='building.parking.type']/*[text()='{parkingType}']"));
            if (Wrapper.IsAttributeNotContainsValue(tabElement, "class", "active"))
            {
                tabElement.Click();
            }
        }

        public void SetAdvancedBlock(List<string> additionally)
        {
            Wrapper.ClickElement(_moreRentParameters);

            var additionalCheckboxes = Wrapper.FindElements(additionalCheckbox);

            foreach (var checkbox in additionalCheckboxes)
            {
                if (Wrapper.IsAttributeContainsValue(checkbox, "class", "checked"))
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

        public void SetRoomsForRentCount(string roomsForSale)
        {
            var roomsForSaleDropdown = Wrapper.FindElement(RoomsCount);
            roomsForSaleDropdown.Click();
            roomsForSaleDropdown.FindElement(By.XPath($"//*[contains(@data-mark, 'roomsForSaleCount')]" +
                                                      $"/*[text()='{roomsForSale}']")).Click();
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

        public void SetTotalArea(string totalArea)
        {
            Wrapper.TypeAndSend(UrbanTotalArea, totalArea);
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

        public void SetRoomsTotalCount(string roomsCount)
        {
            var roomsCountDropdown = Wrapper.FindElement(_roomsForRentTotalCount);
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
            var tabElement = Wrapper.FindElement(By.XPath($"//*[@name='loggiasCount']/*[text()='{loggiasCount}']"));
            if (Wrapper.IsAttributeNotContainsValue(tabElement, "class", "active"))
            {
                tabElement.Click();
            }
        }

        public void SetBalconiesCount(string balconiesCount)
        {
            var tabElement = Wrapper.FindElement(By.XPath($"//*[@name='balconiesCount']/*[text()='{balconiesCount}']"));
            if (Wrapper.IsAttributeNotContainsValue(tabElement, "class", "active"))
            {
                tabElement.Click();
            }
        }

        public void SetSeparateWcsCount(string separateWcsCount)
        {
            var tabElement = Wrapper.FindElement(By.XPath($"//*[@name='separateWcsCount']/*[text()='{separateWcsCount}']"));
            if (Wrapper.IsAttributeNotContainsValue(tabElement, "class", "active"))
            {
                tabElement.Click();
            }
        }

        public void SetCombinedWcsCount(string combinedWcsCount)
        {
            var tabElement = Wrapper.FindElement(By.XPath($"//*[@name='combinedWcsCount']/*[text()='{combinedWcsCount}']"));
            if (Wrapper.IsAttributeNotContainsValue(tabElement, "class", "active"))
            {
                tabElement.Click();
            }
        }

        public void SetRepairType(string repairType)
        {
            var tabElement = Wrapper.FindElement(By.XPath($"//*[@name='repairType']/*[text()='{repairType}']"));
            if (Wrapper.IsAttributeNotContainsValue(tabElement, "class", "active"))
            {
                tabElement.Click();
            }
        }

        public void SetHasPhone(string hasPhone)
        {
            Wrapper.FindElement(By.XPath($"//*[@name='hasPhone']/*[text()='{hasPhone}']")).Click();
        }

        public void SetPetsAllowed(string petsAllowed)
        {
            var tabElement = Wrapper.FindElement(By.XPath($"//*[@name='petsAllowed']/*[text()='{petsAllowed}']"));
            if (Wrapper.IsAttributeNotContainsValue(tabElement, "class", "active"))
            {
                tabElement.Click();
            }
        }
        public void SetChildrenAllowed(string childrenAllowed)
        {
            var tabElement = Wrapper.FindElement(By.XPath($"//*[@name='childrenAllowed']/*[text()='{childrenAllowed}']"));
            if (Wrapper.IsAttributeNotContainsValue(tabElement, "class", "active"))
            {
                tabElement.Click();
            }
        }
    }
}