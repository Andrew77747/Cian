using Cian.Framework.Tools;
using OpenQA.Selenium;

namespace Cian.Framework.PageObjects.Pages.AdsPage
{
    public class AdPage : BasePage
    {
        public AdPage(WebDriverManager manager) : base(manager)
        {
            
        }

        #region MapOfElements

        private readonly By _addressInput = By.CssSelector("[name='geo.userInput']");
        private readonly By _addressDropdownString = By.CssSelector("[ng-class*='typehead-item-displayName']");
        private readonly By _addressDropdown = By.CssSelector(".cui-typeahead-menu.cui-typeahead-menu__has-matches");
        private readonly By _clearFormBtn = By.CssSelector("button[type='reset']");
        private readonly By _saleType = By.XPath("//*[@data-mark-model-name='adType.dealType']/*[text()='Продажа']");
        private readonly By _rentType = By.XPath("//*[@data-mark-model-name='adType.dealType']/*[text()='Аренда']");
        private readonly By _youTubeInput = By.CssSelector("[name='youtube']");
        private readonly By _addVideoBtn = By.CssSelector(".video__button");
        private readonly By _adTitle = By.CssSelector("[name='title']");
        private readonly By _adDescription = By.CssSelector("[wrap='soft']");
        private readonly By _shortFormSwitcher = By.CssSelector(".short-form-switcher");
        private readonly By _fullFormSwitcher = By.CssSelector(".short-form-switcher__full-form");

        #endregion

        public void ClickClearFormBtn()
        {
            Wrapper.ClickElement(_clearFormBtn);
        }

        public void SwitchFormIfExists()
        {
            if (Wrapper.IsElementDisplayed(_shortFormSwitcher))
            {
                Wrapper.FindElement(_fullFormSwitcher).Click();
            }
        }

        public void SetAdMainBlock(string accountType, string dealType, string realEstateType, 
            string objectType, string rentType = null)
        {
            Wrapper.FindElement(By.XPath($"//*[@items='adTypeCtrl.accountTypes']/*[text()='{accountType}']")).Click();
            Wrapper.FindElement(By.XPath($"//*[@data-mark-model-name='adType.dealType']/*[text()='{dealType}']")).Click();

            switch (dealType)
            {
                case "Аренда":
                    Wrapper.FindElement(By.XPath($"//*[@data-mark-model-name='adType.rentDuration']/*[text()='{rentType}']")).Click();
                    Wrapper.FindElement(By.XPath(
                        $"//*[@data-mark-model-name='adType.propertyType']/*[text()='{realEstateType}']")).Click();

                    if (rentType == "Посуточно")
                        Wrapper.FindElement(By.XPath(
                            $"//*[@data-mark-model-name='adType.objectType']/*[text()='{objectType}']")).Click();
                    else
                        Wrapper.FindElement(By.XPath($"//*[@ng-bind='objectType.name' and text() ='{objectType}']")).Click();
                    break;

                case "Продажа":
                    Wrapper.FindElement(By.XPath(
                        $"//*[@data-mark-model-name='adType.propertyType']/*[text()='{realEstateType}']")).Click();
                    Wrapper.FindElement(By.XPath($"//*[@ng-bind='objectType.name' and text() ='{objectType}']")).Click();
                    break;
            }

        }



        public void SaleAd(string accountType, string realEstateType, string objectType)
        {
            Wrapper.FindElement(By.XPath($"//*[@items='adTypeCtrl.accountTypes']/*[text()='{accountType}']")).Click();

            Wrapper.FindElement(_saleType).Click();

            Wrapper.FindElement(By.XPath($"//*[@data-mark-model-name='adType.propertyType']/*[text()='{realEstateType}']")).Click();

            Wrapper.FindElement(By.XPath($"//*[@ng-bind='objectType.name' and text() ='{objectType}']")).Click();
        }

        public void RentAd(string accountType, string rentType, string realEstateType, string objectType)
        {
            Wrapper.FindElement(By.XPath($"//*[@items='adTypeCtrl.accountTypes']/*[text()='{accountType}']")).Click();

            Wrapper.FindElement(_rentType).Click();
                                                                                                                     
            Wrapper.FindElement(By.XPath($"//*[@data-mark-model-name='adType.rentDuration']/*[text()='{rentType}']")).Click();

            Wrapper.FindElement(By.XPath($"//*[@data-mark-model-name='adType.propertyType']/*[text()='{realEstateType}']")).Click();

            if(rentType == "Посуточно")
                Wrapper.FindElement(By.XPath($"//*[@data-mark-model-name='adType.objectType']/*[text()='{objectType}']")).Click();
            else
                Wrapper.FindElement(By.XPath($"//*[@ng-bind='objectType.name' and text() ='{objectType}']")).Click();
        }

        public void EnterAddress(string address)
        {
            Wrapper.ClearTypeAndSend(_addressInput, address);

            Wrapper.WaitElementDisplayed(_addressDropdown);

            var dropdownAddressString = Wrapper.FindElements(_addressDropdownString);

            foreach (var addressString in dropdownAddressString)
            {
                if (addressString.Text.Contains(address))
                    addressString.Click();
                break;
            }
        }

        public void SetDescriptionBlock(string description, string youTubeLink = null, string title = null)
        {
            //Добавить загрузку фоток
            if (youTubeLink != null)
            {
                Wrapper.TypeAndSend(_youTubeInput, youTubeLink);
                Wrapper.ClickElement(_addVideoBtn);
            }
            
            if(title != null)
                Wrapper.ClearTypeAndSend(_adTitle, title);

            Wrapper.TypeAndSend(_adDescription, description);
        }

        public void SetPriceAndDealConditions()
        {

        }
    }
}