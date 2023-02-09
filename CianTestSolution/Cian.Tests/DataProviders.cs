using System.Collections;
using System.Collections.Generic;
using Cian.Framework.Data.Models;

namespace Cian.Tests
{
    public class DataProviders
    {
        public static IEnumerable OwnerSaleLivingApartmentAdData
        {
            get
            {
                yield return new OwnerSaleLivingApartmentAd()
                {
                    Address = "проспект Просвещения, 14к4, Санкт-Петербург",
                    CadastralNumber = "47:14:1203001:814",


                    //Email = "adam" + (long)(DateTime.Now - DateTime.MinValue).TotalMilliseconds + "@smith.me",
                };
            }
        }
    }
}