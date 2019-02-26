using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBackEndLayer.Helpers
{
    class ProvincesModel
    {
        public static List<string> PopulateProvinces()
        {
            var provinces = new List<string>();
            provinces.Add("Alberta");
            provinces.Add("British Columbia");
            provinces.Add("Manitoba");
            provinces.Add("New Brunswick");
            provinces.Add("Newfoundland and Labrador");
            provinces.Add("Nova Scotia");
            provinces.Add("Ontario");
            provinces.Add("Prince Edward Island");
            provinces.Add("Saskatchewan");
            provinces.Add("Quebec");

            return provinces;
        }
    }
}