using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RentApparts.Classes
{

    public partial class RegionApps : Appartment
    {
        // Константа
        public const int MaxApartments = 100;

        // Read-only поле
        public readonly DateTime CreationDate;

        // Статическое поле
        private static int apartmentCount;

        // Статический метод
        public static int GetApartmentCount(){
            return apartmentCount;
        }

        // Конструктор
        public RegionApps()
        {
            CreationDate = DateTime.Now;
            apartmentCount++;
        }

        // Реализация виртуального метода базового класса
        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Region: {Region}");
        }
    }
}
