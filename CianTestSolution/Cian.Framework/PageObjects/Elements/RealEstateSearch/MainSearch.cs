using System.Collections.Generic;
using System.Threading;
using Cian.Framework.Data.RealEstateMainSearch;
using Cian.Framework.Tools;
using OpenQA.Selenium;

namespace Cian.Framework.PageObjects.Elements.RealEstateSearch
{
    public class MainSearch : BaseElement
    {
        public MainSearch(WebDriverManager manager) : base(manager)
        {
            
        }

        #region Map of Elements

        private readonly By _offerType = By.CssSelector("[data-mark='FilterOfferType']");
        private readonly By _roomsCount = By.CssSelector("[data-mark='FilterRoomsCount']");
        private readonly By _price = By.CssSelector("[data-mark='FilterPrice']");
        private readonly By _priceFrom = By.CssSelector("[placeholder='от']");
        private readonly By _priceTill = By.CssSelector("[placeholder='до']");
        private readonly By _addressInput = By.CssSelector("#geo-suggest-input");
        private readonly By _searchBtn = By.CssSelector("[data-mark='FiltersSearchButton']");
        private readonly By _addressString = By.XPath("//*[@data-group='addresses']//*[@class='_025a50318d--trunc_1--W9PIS']");
        private readonly By _addressDropdown = By.XPath("//*[@data-group='addresses']");

        #endregion

        public void SetPrice(string priceFrom, string priceTill)
        {
            Wrapper.ClickElement(_price);
            Wrapper.TypeAndSend(_priceFrom, priceFrom);
            Wrapper.TypeAndSend(_priceTill, priceTill);
            Wrapper.ClickElement(_price);
        }

        public void SetRoomsCount(string roomsCount, List<string> apartmentTypeCheckboxes)
        {
            Wrapper.ClickElement(_roomsCount);

            Wrapper.ClickElement(By.XPath($"//*[contains(@class, '_025a50318d--button--i1_mM') and text()='{roomsCount}']"));

            foreach (var checkbox in apartmentTypeCheckboxes)
            {
                var element =
                    Wrapper.FindElement(By.XPath($"//*[@class='_025a50318d--label--UE3eS' and text()='{checkbox}']"));

                if (Wrapper.IsAttributeNotContainsValue(element, "class", "checked"))
                {
                    element.Click();
                }
            }

            Wrapper.ClickElement(_roomsCount);
        }

        public void SetOfferTypeSearch(string type, List<string> offerTypeCheckboxes)
        {
            Wrapper.ClickElement(_offerType);

            Wrapper.ClickElement(By.XPath($"//*[contains(@class, 'control-button-group')]//*[text()='{type}']"));

            foreach (var checkbox in offerTypeCheckboxes)
            {
                var element =
                    Wrapper.FindElement(By.XPath($"//*[@class='_025a50318d--label--UE3eS' and text()='{checkbox}']"));

                if (Wrapper.IsAttributeNotContainsValue(element, "class", "checked"))
                {
                    element.Click();
                }
            }

            Wrapper.ClickElement(_offerType);
        }

        public void ClickTabMenu(string tabMenuName)
        {
            Wrapper.ClickElement(By.XPath($"//*[@data-mark='FiltersTabs']//*[text()='{tabMenuName}']"));
        }

        public void SetAddress(string address)
        {
            Wrapper.TypeAndSend(_addressInput, address);
            Wrapper.WaitElementDisplayed(_addressDropdown);
            Wrapper.PutEnter(_addressInput);
        }

        public void ClickSearchBtn()
        {
            Wrapper.ClickElement(_searchBtn);
        }

        //Основные метода поиска
        public void BuyOrRentApartmentSearch(string tabMenuName, List<string> offerTypeCheckboxes, string roomsCount, 
            List<string> apartmentTypeCheckboxes, string priceFrom, string priceTill, string address)
        {
            ClickTabMenu(tabMenuName);
            SetOfferTypeSearch("Жилая", offerTypeCheckboxes);
            SetRoomsCount(roomsCount, apartmentTypeCheckboxes);//Можно выбрать сразу несколько вариантов комнат - исправить
            SetPrice(priceFrom, priceTill);
            SetAddress(address);
            ClickSearchBtn();
        }

        public void BuyOrRentRoomOrHouseSearch(string[] offerTypeCheckboxes, string roomsCount, string[] apartmentTypeCheckboxes,
            string priceFrom, string priceTill, string address)
        {

        }

        public void BuyOrRentCommercialSearch(string[] offerTypeCheckboxes, string roomsCount, string[] apartmentTypeCheckboxes,
            string priceFrom, string priceTill, string address)
        {

        }

        public void DailyRentSearch(string[] offerTypeCheckboxes, string roomsCount, string[] apartmentTypeCheckboxes,
            string priceFrom, string priceTill, string address)
        {

        }

        public void EstimationSearch(string[] offerTypeCheckboxes, string roomsCount, string[] apartmentTypeCheckboxes,
            string priceFrom, string priceTill, string address)
        {

        }

        public void MortgageSearch(string[] offerTypeCheckboxes, string roomsCount, string[] apartmentTypeCheckboxes,
            string priceFrom, string priceTill, string address)
        {

        }

        public void RealtorSearch(string[] offerTypeCheckboxes, string roomsCount, string[] apartmentTypeCheckboxes,
            string priceFrom, string priceTill, string address)
        {

        }

        public void NewBuildingSearch(string[] offerTypeCheckboxes, string roomsCount, string[] apartmentTypeCheckboxes,
            string priceFrom, string priceTill, string address)
        {

        }
    }
}