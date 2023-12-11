using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


public delegate void ViewCountHandler(int apartmentId);

namespace RentApparts.Classes
{
    
    public partial class RegionApps : Appartment
    {
        public event ViewCountHandler ViewCountUpdated;
        private int viewCount;       
        public int ViewCount
        {
            get { return viewCount; }
            private set
            {
                viewCount = value;
                ViewCountUpdated?.Invoke(ApartmentId);
            }
        }
        public void IncrementViewCount()
        {
            ViewCount++;
        }

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

        public static void WriteApartmentsToXml(RegionApps apartments, string filePath)
        {
            try
            {

                XDocument doc;

                // Попробовать загрузить существующий XML-файл
                if (File.Exists(filePath))
                {
                    doc = XDocument.Load(filePath);

                    // Проверить, что корневой элемент существует и является "Apartments"
                    if (doc.Root == null || doc.Root.Name != "Apartments")
                    {
                        Console.WriteLine("Invalid XML file format. Unable to append data.");
                        return;
                    }
                }
                else
                {
                    // Если файл не существует, создать новый документ
                    doc = new XDocument(new XElement("Apartments"));
                }

                // Добавить новые данные

                doc.Root.Add(new XElement("Apartment",
                    new XElement("ApartmentId", apartments.ApartmentId),
                    new XElement("OwnerName", apartments.OwnerName),
                    new XElement("Price", apartments.Price),
                    new XElement("RegionName", apartments.RegionName)
                ));


                // Сохранить документ в файл
                doc.Save(filePath);
                Console.WriteLine($"Apartments data has been successfully written to {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to XML file: {ex.Message}");
            }
        }
        public static List<RegionApps> SearchByOwner(List<RegionApps> apartments, string ownerName)
        {
            return apartments.Where(apartment => apartment.OwnerName.Equals(ownerName, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public static List<RegionApps> FilterByRegion(List<RegionApps> apartments, string region)
        {
            return apartments.Where(apartment => apartment.RegionName.Equals(region, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public static List<RegionApps> FilterByPriceRange(List<RegionApps> apartments, decimal minPrice, decimal maxPrice)
        {
            return apartments.Where(apartment => apartment.Price >= minPrice && apartment.Price <= maxPrice).ToList();
        }
    }
}
