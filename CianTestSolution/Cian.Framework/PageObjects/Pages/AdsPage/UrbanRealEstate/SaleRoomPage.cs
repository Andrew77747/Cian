using Cian.Framework.Tools;
using OpenQA.Selenium;

namespace Cian.Framework.PageObjects.Pages.AdsPage.UrbanRealEstate
{
    public class SaleRoomPage : UrbanRealEstatePage
    {
        public SaleRoomPage(WebDriverManager manager) : base(manager)
        {

        }

        #region Map of Elements

        private readonly By _cadastralNumberInput = By.CssSelector("[name='cadastralNumber']");
        private readonly By _roomsCountDropdown = By.CssSelector("[name='roomsCount']");
        private readonly By _checkBox = By.CssSelector(".cui-checkbox.cui-checkbox_inline");
        private readonly By _totalAreaApartment = By.CssSelector("[name='urbanTotalArea']");
        private readonly By _floor = By.CssSelector("[name='floorNumber']");
        private readonly By _floorCount = By.CssSelector("[name='floorsCount']");
        private readonly By _roomArea = By.CssSelector("[name='allRoomsArea']");
        private readonly By _livingArea = By.CssSelector("[name='livingArea']");
        private readonly By _kitchenArea = By.CssSelector("[name='kitchenArea']");

        #endregion
        //TODO этот метод можно удалить
        public void SetSaleApartmentForm(string cadastralNumber, string housingType, string countNumber,
            string totalArea, string floor, string floorCount, string roomArea, string livingArea,
            string kitchenArea, string loggiasCount, string combinedWcsCount, string repairType, string hasPhone,
            params string[] checkBoxesNames)
        {
            //Wrapper.TypeAndSend(CadastralNumberInput, cadastralNumber);

            Wrapper.FindElement(By.XPath($"//*[@name='housingType']/*[text()='{housingType}']")).Click();

            //Wrapper.ClickElement(RoomsCountDropdown);
            //Wrapper.FindElement(By.XPath($"//*[name='roomsForCount']/*[text()='{countNumber}']")).Click();

            var checkBoxes = Wrapper.FindElements(CheckBox);
            foreach (var checkBox in checkBoxes)
            {
                if (Wrapper.IsAttributeContainsValue(checkBox, "class", "cui-checkbox_checked"))
                {
                    checkBox.Click();
                }
            }

            foreach (var checkBoxName in checkBoxesNames)
            {
                switch (checkBoxName)
                {
                    case "Пентхаус":
                        checkBoxes[0].Click();
                        break;
                    case "Смежная":
                        checkBoxes[1].Click();
                        break;
                    case "Изолированная":
                        checkBoxes[2].Click();
                        break;
                    case "Во двор":
                        checkBoxes[3].Click();
                        break;
                    case "На улицу":
                        checkBoxes[4].Click();
                        break;
                }
            }

            Wrapper.TypeAndSend(UrbanTotalArea, totalArea);

            //Wrapper.TypeAndSend(Floor, floor);

            //Wrapper.TypeAndSend(FloorCount, floorCount);

            //Wrapper.TypeAndSend(RoomArea, roomArea);

            //Wrapper.TypeAndSend(LivingArea, livingArea);

            //Wrapper.TypeAndSend(KitchenArea, kitchenArea);

            //Wrapper.FindElement(By.XPath($"//*[@name='loggiasCount']/*[text()='{loggiasCount}']")).Click();

            //Wrapper.FindElement(By.XPath($"//*[@name='combinedWcsCount']/*[text()='{combinedWcsCount}']")).Click();

            //Wrapper.FindElement(By.XPath($"//*[@name='repairType']/*[text()='{repairType}']")).Click();

            //Wrapper.FindElement(By.XPath($"//*[@name='hasPhone']/*[text()='{hasPhone}']")).Click();
        }
    }
}