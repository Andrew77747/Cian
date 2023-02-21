using System.Collections.Generic;
using NUnit.Framework;

namespace Cian.Framework.Data.Models
{
    public class SearchModel
    {
        public List<string> OfferTypeCheckboxes { get; set; }
        public List<string> RoomsCount { get; set; }
        public List<string> ApartmentTypeCheckboxes { get; set; }
        public string PriceFrom { get; set; }
        public string PriceTill { get; set; }
        public string Address { get; set; }
    }
}