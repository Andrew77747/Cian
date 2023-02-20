using System.Collections;
using System.Collections.Generic;
using Cian.Framework.Data.Announcement;
using Cian.Framework.Data.Models;

namespace Cian.Tests
{
    public class DataProviders
    {
        public static IEnumerable OwnerRentRoomAdData
        {
            get
            {
                yield return new UrbanRealEstateAdModel()
                {
                    Address = "проспект Просвещения, 14к4, Санкт-Петербург",
                    CadastralNumber = "47:14:1203001:814",
                    RoomsForRentCount = "1",
                    RoomsType = "Изолированная",
                    RoomArea = "20",
                    TotalArea = "80",
                    Floor = "3",
                    FloorCount = "9",
                    TotalRoomCountInFlat = "3",
                    Kitchen = "10",
                    LoggiasCount = "1",
                    BalconiesCount = "Нет",
                    SeparatedWsCount = "1",
                    CombinedWsCount = "Нет",
                    Repair = "Косметический",
                    Pets = "Нет",
                    Children = "Да",
                    AdvancedOptions = new List<string>
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
                    },
                    Name = "Алые паруса",
                    BuildYear = "2020",
                    HouseType = "Кирпичный",
                    HouseSeries = "i-80",
                    CeilingHeight = "3",
                    PassengerElevator = "1",
                    CargoElevator = "нет",
                    Ramp = "Нет",
                    GarbageChute = "Есть",
                    Parking = "Наземная",
                    Description = "Сдается отличная комната",
                    RentPrice = "20000",
                    CurrencyType = "₽",
                    BargainCheckbox = "Yes",
                    CommunalPaymentAmount = "2000",
                    CounterCheckbox = "Yes",
                    BargainPrice = "18000",
                    BargainConditions = "Порядочной семье торг",
                    Prepayment = "1 месяц",
                    SelfEmployed = "Не указывать",
                    OwnerDeposit = "20000",
                    TenantsType = "семья"
                };
                //Email = "adam" + (long)(DateTime.Now - DateTime.MinValue).TotalMilliseconds + "@smith.me",
            }
        }

        public static IEnumerable BuyApartmentSearchData
        {
            get
            {
                yield return new SearchModel()
                {
                    OfferTypeCheckboxes = new List<string>
                    {
                        "Квартира в новостройке",
                        "Квартира во вторичке"
                    },
                    RoomsCount = "2",
                    ApartmentTypeCheckboxes = new List<string>
                    {
                        "Студия",
                        "Свободная планировка"
                    },
                    PriceFrom = "3000000",
                    PriceTill = "10000000",
                    Address = "метро Лесная, Санкт-Петербург"
                };
            }
        }
    }
}
