using System.Collections;
using System.Collections.Generic;
using Cian.Framework.Data.Models;

namespace Cian.Tests
{
    public class DataProviders
    {
        public static IEnumerable OwnerRentRoomAdData
        {
            get
            {
                yield return new AboutLivingObject()
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
                    AdvancedOptions = new List<string>()
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
                    Description = "Сдается отличная комната"
                };
                //Email = "adam" + (long)(DateTime.Now - DateTime.MinValue).TotalMilliseconds + "@smith.me",
            }
        }

        public static IEnumerable AboutBuildingData
        {
            get
            {
                yield return new AboutLivingBuilding()
                {
                    Name = "Алые паруса",
                    BuildYear = "2020",
                    HouseType = "Кирпичный",
                    HouseSeries = "i-80",
                    CeilingHeight = "3",
                    PassengerElevator = "1",
                    CargoElevator = "нет",
                    Ramp = "Нет",
                    GarbageChute = "Есть",
                    Parking = "Наземная"
                };
            }
        }
    }
}
