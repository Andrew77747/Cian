using System.Collections.Generic;
using Cian.Framework.Data.Announcement;
using Cian.Framework.Data.Models;
using Cian.Framework.Tools;
using NUnit.Framework;

namespace Cian.Framework.PageObjects.Pages.AdsPage.UrbanRealEstate
{
    public class RentRoomPage : UrbanRealEstatePage
    {
        public RentRoomPage(IWebDriverManager manager) : base(manager)
        {

        }

        public void SetAboutObjectBlock(string cadastralNumber, string roomsForRent, string roomsType, string roomArea,
            string totalArea, string floor, string floorCount, string roomsCount, string kitchenArea, string loggiasCount,
            string balconiesCount, string separateWcsCount, string combinedWcsCount, string repairType, string petsAllowed,
            string childrenAllowed, List<string> additionalCheckboxes)
        {
            SwitchFormIfExists();

            SetCadastralNumber(cadastralNumber);
            SetRoomsForRentCount(roomsForRent);
            SetRoomsType(roomsType);
            SetRoomArea(roomArea);
            SetTotalArea(totalArea);
            SetFloorCount(floor, floorCount);
            SetRoomsTotalCount(roomsCount);
            SetKitchenArea(kitchenArea);
            SetLoggiasCount(loggiasCount);
            SetBalconiesCount(balconiesCount);
            SetSeparateWcsCount(separateWcsCount);
            SetCombinedWcsCount(combinedWcsCount);
            SetRepairType(repairType);
            SetPetsAllowed(petsAllowed);
            SetChildrenAllowed(childrenAllowed);
            SetAdvancedBlock(additionalCheckboxes);
        }

        public void SetPriceAndDealConditionsLivingUrbanRealEstate_Owner(string rentPrice, string currencyType, string bargainCheckbox,
            string communalPaymentAmount, string counterCheckbox, string bargainPrice, string bargainConditions, string prepayment,
            string selfEmployed, string ownerDeposit, string tenantsType)
        {
            SetPrice(rentPrice);
            SetCurrencyType(currencyType);
            SetBargainCheckbox(bargainCheckbox);
            SetCommunalPaymentAmount(communalPaymentAmount);
            SetCounterCheckbox(counterCheckbox);
            SetBargainPrice(bargainPrice);
            SetBargainConditions(bargainConditions);
            SetPrepayment(prepayment);
            SetSelfEmployed(selfEmployed);
            SetOwnerDeposit(ownerDeposit);
            SetTenantsType(tenantsType);
        }

        public void SetPriceAndDealConditionsLivingUrbanRealEstate_Agent(string rentPrice, string currencyType, string bargainCheckbox,
            string communalPaymentAmount, string counterCheckbox, string bargainPrice, string bargainConditions, string prepayment,
            string ownerDeposit, string tenantsType, string withoutClientPercentCheckbox, string withoutAnotherAgencyPercentCheckbox, 
            string clientPercent = null, string anotherAgencyPercent = null)
        {
            SetPrice(rentPrice);
            SetCurrencyType(currencyType);
            SetBargainCheckbox(bargainCheckbox);
            SetCommunalPaymentAmount(communalPaymentAmount);
            SetCounterCheckbox(counterCheckbox);
            SetBargainPrice(bargainPrice);
            SetBargainConditions(bargainConditions);
            SetPrepayment(prepayment);
            SetOwnerDeposit(ownerDeposit);
            SetTenantsType(tenantsType);
            SetPercentFromClient(withoutClientPercentCheckbox, clientPercent);
            SetPercentFromAnotherAgency(withoutAnotherAgencyPercentCheckbox, anotherAgencyPercent);
        }

        public List<string> GetAdvancedOptions()
        {
            List<string> advancedOptions = new List<string>()
            {
                AdvancedOptions.Bath,
                AdvancedOptions.Phone,
                AdvancedOptions.Internet,
                AdvancedOptions.RoomsFurniture,
                AdvancedOptions.SeparateWs,
                AdvancedOptions.TV,
                AdvancedOptions.Fridge,
                AdvancedOptions.WindowsToStreet,
                AdvancedOptions.Loggia
            };

            return advancedOptions;
        }
    }
}