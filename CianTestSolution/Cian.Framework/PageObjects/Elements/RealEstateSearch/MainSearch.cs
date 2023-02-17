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

        #endregion

        public void SetPrice(string priceFrom, string priceTill)
        {
            Wrapper.ClickElement(_price);
            Wrapper.TypeAndSend(_priceFrom, priceFrom);
            Wrapper.TypeAndSend(_priceTill, priceTill);
            Wrapper.ClickElement(_price);
        }

        public void SetRoomsCount(string roomsCount, string[] apartmentTypeCheckboxes)
        {
            Wrapper.ClickElement(_roomsCount);

            Wrapper.ClickElement(By.XPath($"//*[@class='_025a50318d--button--i1_mM' and text()='{roomsCount}']"));

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

        public void SetOfferTypeSearch(string type, string[] offerTypeCheckboxes)
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
        }
    }
}