using Cian.Framework.Tools;
using OpenQA.Selenium;

namespace Cian.Framework.PageObjects.Pages.AdsPage.UrbanRealEstate
{
    public class OwnerSaleNewBuildingApartmentPage : UrbanRealEstatePage
    {
        public OwnerSaleNewBuildingApartmentPage(WebDriverManager manager) : base(manager)
        {

        }

        #region Map of Elements

        private readonly By _builderName = By.CssSelector("[name='builderName']");
        private readonly By _deadlineYear = By.CssSelector("//*[@name='deadlineYear']");
        private readonly By _deadlineQuarter = By.CssSelector("//*[@name='deadlineQuarter']");
        private readonly By _deadlineCheckbox = By.CssSelector("//*[@name='deadlineIsComplete']/../../..");

        #endregion

        public void SetAboutBuildingForm(string builderName, string deadlineYear, string deadlineQuarter,
            string checkboxValue, string newBuildingMaterialType, string passengerLiftsCount, string cargoLiftsCount,
            string undergroundParking)
        {
            Wrapper.TypeAndSend(_builderName, builderName);

            Wrapper.ClickElement(_deadlineYear);
            Wrapper.FindElement(By.XPath($"//*[@name='deadlineYear']//*[text()='{deadlineYear}']")).Click();
            Wrapper.ClickElement(_deadlineQuarter);
            Wrapper.FindElement(By.XPath($"//*[@name='deadlineQuarter']//*[text()='{deadlineQuarter}']")).Click();

            if (Wrapper.IsAttributeContainsValue(_deadlineCheckbox, "class", "checked"))
                Wrapper.ClickElement(_deadlineCheckbox);
            switch (checkboxValue)
            {
                case "Yes":
                    Wrapper.ClickElement(_garbageChuteCheckbox);
                    break;
                case "No":
                    break;
            }

            Wrapper.FindElement(By.XPath($"//*[@name='newBuildingMaterialType']/*[text()='{newBuildingMaterialType}']")).Click();

            Wrapper.FindElement(By.XPath($"//*[@name='building.passengerLiftsCount']/*[text()='{passengerLiftsCount}']")).Click();

            Wrapper.FindElement(By.XPath($"//*[@name='building.cargoLiftsCount']/*[text()='{cargoLiftsCount}']")).Click();

            Wrapper.FindElement(By.XPath($"//*[@name='undergroundParking']/*[text()='{undergroundParking}']")).Click();
        }
    }
}