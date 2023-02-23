using Cian.Framework.Tools;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace Cian.Framework.PageObjects.Pages.AdsPage.UrbanRealEstate
{
    public class UrbanRealEstatePage : AdPage
    {
        public UrbanRealEstatePage(IWebDriverManager manager) : base(manager)
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

        private readonly By _currencyType = By.CssSelector("[name='bargainTerms.currency']");
        private readonly By _priceInput = By.CssSelector("[name='bargainTerms.price']");
        private readonly By _taxInput = By.CssSelector("[name='vatType']");
        private readonly By _dealTermsCheckbox = By.CssSelector(".price-and-deal-terms-lease-assignment-checkbox .cui-checkbox");
        private readonly By _rentPriceInput = By.CssSelector("[name='bargainTerms.price']");
        private readonly By _priceCurrencySelect = By.CssSelector("[items='priceCurrencySelect.currencies']");
        private readonly By _communalPaymentsInput = By.CssSelector("[name='bargainTerms.utilitiesTerms.price']");
        private readonly By _prepaymentDropdown = By.CssSelector("[name='bargainTerms.prepayType']");
        private readonly By _counterCheckbox = By.CssSelector(".utilities-terms-input__checkbox .cui-checkbox");
        private readonly By _ownerDepositInput = By.CssSelector("[name='bargainTerms.deposit']");
        private readonly By _bargainCheckbox = By.CssSelector("[ng-if='priceRent.config.showBargain()'] .cui-checkbox__label");
        private readonly By _bargainPriceInput = By.CssSelector("[name='bargain.bargainPrice']");
        private readonly By _bargainConditionsTextArea = By.CssSelector("[name='bargain.bargainConditions'] .cui-textarea__textarea");
        private readonly By _clientPercentInput = By.CssSelector("[name='bargainTerms.clientFee']");
        private readonly By _anotherAgencyPercentInput = By.CssSelector("[name='bargainTerms.agentFee']");
        private readonly By _withoutClientPercentCheckbox = By.XPath("//*[@name='bargainTerms.clientFee']/../following-sibling::div");
        private readonly By _withoutAnotherAgencyPercentCheckbox = By.XPath("//*[@name='bargainTerms.agentFee']/../following-sibling::div");


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
            if (Wrapper.IsElementDisplayed(_moreRentParameters))
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

        //
        //public void SetPriceAndDealConditionsLivingUrbanRealEstate_Owner(string rentPrice = null, string currencyType = null,
        //    string bargainCheckbox = null, string communalPaymentAmount = null, string counterCheckbox = null, string bargainPrice = null,
        //    string bargainConditions = null, string prepayment = null, string selfEmployed = null, string ownerDeposit = null,
        //    string tenantsType = null)
        //{
        //if (rentPrice != null)
        //    Wrapper.TypeAndSend(_rentPriceInput, rentPrice);

        //if (currencyType != null)
        //{
        //    Wrapper.ClickElement(_currencyType);
        //    Wrapper.FindElement(By.XPath($"//*[contains(@class, 'cui-dropdown__item') and text()='{currencyType}']")).Click();
        //}

        //if (bargainCheckbox != null)
        //{
        //    switch (bargainCheckbox)
        //    {
        //        case "Yes":
        //            if (Wrapper.IsAttributeNotContainsValue(_bargainCheckbox, "class", "checked"))
        //                Wrapper.ClickElement(_bargainCheckbox);
        //            break;
        //        case "No":
        //            if (Wrapper.IsAttributeContainsValue(_bargainCheckbox, "class", "checked"))
        //                Wrapper.ClickElement(_bargainCheckbox);
        //            break;
        //    }
        //}

        //if (communalPaymentAmount != null)
        //    Wrapper.TypeAndSend(_communalPaymentsInput, communalPaymentAmount);

        //if (counterCheckbox != null)
        //{
        //    switch (counterCheckbox)
        //    {
        //        case "Yes":
        //            if (Wrapper.IsAttributeNotContainsValue(_counterCheckbox, "class", "checked"))
        //                Wrapper.ClickElement(_counterCheckbox);
        //            break;
        //        case "No":
        //            if (Wrapper.IsAttributeContainsValue(_counterCheckbox, "class", "checked"))
        //                Wrapper.ClickElement(_counterCheckbox);
        //            break;
        //    }
        //}

        //if (bargainPrice != null)
        //    Wrapper.TypeAndSend(_bargainPriceInput, bargainPrice);

        //if (bargainConditions != null)
        //    Wrapper.TypeAndSend(_bargainConditionsTextArea, bargainConditions);

        //if (prepayment != null)
        //{
        //    Wrapper.ClickElement(_prepaymentDropdown);
        //    Wrapper.ClickElement(By.XPath(
        //        $"//*[contains(@data-mark, 'dropdown|ad.bargainTerms.prepayMonths')]//*[text()='{prepayment}']"));
        //}

        //if (selfEmployed != null)
        //    Wrapper.ClickElement(By.XPath($"//*[@data-mark-model-name='selfEmployed.isEnabled']/*[text()='{selfEmployed}']"));

        //if (ownerDeposit != null)
        //    Wrapper.TypeAndSend(_ownerDepositInput, ownerDeposit);

        //if (tenantsType != null)
        //    Wrapper.ClickElement(By.XPath($"//*[@name='bargainTerms.tenantsType']/*[text()='{tenantsType}']"));
        //}

        //
        public void SetPrice(string rentPrice)
        {
            Wrapper.TypeAndSend(_rentPriceInput, rentPrice);
        }

        public void SetCurrencyType(string currencyType)
        {
            Wrapper.ClickElement(_currencyType);
            Wrapper.FindElement(By.XPath($"//*[contains(@class, 'cui-dropdown__item') and text()='{currencyType}']")).Click();
        }

        public void SetBargainCheckbox(string bargainCheckbox)
        {
            switch (bargainCheckbox)
            {
                case "Yes":
                    if (Wrapper.IsAttributeNotContainsValue(_bargainCheckbox, "class", "checked"))
                        Wrapper.ClickElement(_bargainCheckbox);
                    break;
                case "No":
                    if (Wrapper.IsAttributeContainsValue(_bargainCheckbox, "class", "checked"))
                        Wrapper.ClickElement(_bargainCheckbox);
                    break;
            }
        }

        public void SetCommunalPaymentAmount(string communalPaymentAmount)
        {
            Wrapper.TypeAndSend(_communalPaymentsInput, communalPaymentAmount);
        }

        public void SetCounterCheckbox(string counterCheckbox)
        {
            switch (counterCheckbox)
            {
                case "Yes":
                    if (Wrapper.IsAttributeNotContainsValue(_counterCheckbox, "class", "checked"))
                        Wrapper.ClickElement(_counterCheckbox);
                    break;
                case "No":
                    if (Wrapper.IsAttributeContainsValue(_counterCheckbox, "class", "checked"))
                        Wrapper.ClickElement(_counterCheckbox);
                    break;
            }
        }

        public void SetBargainPrice(string bargainPrice)
        {
            Wrapper.TypeAndSend(_bargainPriceInput, bargainPrice);
        }

        public void SetBargainConditions(string bargainConditions)
        {
            Wrapper.TypeAndSend(_bargainConditionsTextArea, bargainConditions);
        }

        public void SetPrepayment(string prepayment)
        {
            Wrapper.ClickElement(_prepaymentDropdown);
            Wrapper.ClickElement(By.XPath(
                $"//*[contains(@data-mark, 'dropdown|ad.bargainTerms.prepayMonths')]//*[text()='{prepayment}']"));
        }

        public void SetSelfEmployed(string selfEmployed)
        {
            Wrapper.ClickElement(By.XPath($"//*[@data-mark-model-name='selfEmployed.isEnabled']/*[text()='{selfEmployed}']"));
        }

        public void SetOwnerDeposit(string ownerDeposit)
        {
            Wrapper.TypeAndSend(_ownerDepositInput, ownerDeposit);
        }

        public void SetTenantsType(string tenantsType)
        {
            Wrapper.ClickElement(By.XPath($"//*[@name='bargainTerms.tenantsType']/*[text()='{tenantsType}']"));
        }

        public void SetPercentFromClient(string withoutPercentCheckbox, string percent = null)
        {
            if (Wrapper.IsElementDisplayed(_clientPercentInput))
            {
                if (withoutPercentCheckbox == "No")
                    Wrapper.TypeAndSend(_clientPercentInput, percent);
                else
                {
                    Wrapper.ClickElement(_withoutClientPercentCheckbox);
                }
            }
        }

        public void SetPercentFromAnotherAgency(string withoutPercentCheckbox, string percent = null)
        {
            if (Wrapper.IsElementDisplayed(_anotherAgencyPercentInput))
            {
                if (withoutPercentCheckbox == "No")
                    Wrapper.TypeAndSend(_anotherAgencyPercentInput, percent);
                else
                {
                    Wrapper.ClickElement(_withoutAnotherAgencyPercentCheckbox);
                }
            }
        }
    }
}