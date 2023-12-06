using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


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

        public static List<RegionApps> ReadApartmentsFromXml(string filePath)
        {
            try
            {
                XDocument doc = XDocument.Load(filePath);

                var apartments = from apt in doc.Descendants("Apartment")
                                 select new RegionApps
                                 {
                                     ApartmentId = int.Parse(apt.Element("ApartmentId").Value),
                                     OwnerName = apt.Element("OwnerName").Value,
                                     Price = decimal.Parse(apt.Element("Price").Value),
                                     RegionName = apt.Element("RegionName").Value
                                 };

                return apartments.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading XML file: {ex.Message}");
                return new List<RegionApps>();
            }
        }

    }
}
