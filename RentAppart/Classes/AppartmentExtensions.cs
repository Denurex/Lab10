using RentApparts.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentAppart.Classes
{
    public static class ApartmentExtensions
    {
        public static bool IsExpensive(this RegionApps apartment, decimal threshold)
        {
            return apartment.Price > threshold;
        }

        public static int CompareByPrice(this RegionApps apartment, RegionApps otherApartment)
        {
            return apartment.Price.CompareTo(otherApartment.Price);
        }
    }
}
