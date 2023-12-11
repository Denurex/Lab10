using RentApparts.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentAppart
{
    internal class Program
    {
        //static string currentDirectory = Environment.CurrentDirectory;
        static string str = AppDomain.CurrentDomain.BaseDirectory;
        static string filePath = str.Replace("bin\\Debug\\", "apartments.xml");
        static List<RegionApps> allApartments = RegionApps.ReadApartmentsFromXml(filePath);
        static void Main(string[] args)
        {
            

            while (true)
            {
                Console.WriteLine("========== Rent Apartments Program ==========");
                Console.WriteLine("1. Show all apartments");
                Console.WriteLine("2. Search apartments by owner");
                Console.WriteLine("3. Filter apartments by region");
                Console.WriteLine("4. Filter apartments by price range");
                Console.WriteLine("5. View a specific apartment");
                Console.WriteLine("6. Add an apartment for sale");
                Console.WriteLine("7. Exit");
                Console.WriteLine("=============================================");

                Console.Write("Enter your choice (1-6): ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowApartments(allApartments);
                        break;
                    case "2":
                        SearchApartmentsByOwner(allApartments);
                        break;
                    case "3":
                        FilterApartmentsByRegion(allApartments);
                        break;
                    case "4":
                        FilterApartmentsByPriceRange(allApartments);
                        break;
                    case "5":
                        ViewSpecificApartment(allApartments);
                        break;
                    case "6":
                        AddApartmentForSale();
                        break;
                    case "7":
                        Console.WriteLine("Exiting the program. Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a valid option.");
                        break;
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        static void ShowApartments(List<RegionApps> apartments)
        {
            Console.WriteLine("========== All Apartments ==========");
            foreach (var apartment in apartments)
            {
                apartment.DisplayInfo();
                Console.WriteLine("-------------------------------");
            }
        }
        static void SearchApartmentsByOwner(List<RegionApps> apartments)
        {
            Console.Write("Enter owner's name to search: ");
            string ownerName = Console.ReadLine();

            List<RegionApps> result = RegionApps.SearchByOwner(apartments, ownerName);

            if (result.Count > 0)
            {
                Console.WriteLine($"Apartments owned by {ownerName}:");
                foreach (var apartment in result)
                {
                    apartment.DisplayInfo();
                    Console.WriteLine("-------------------------------");
                }
            }
            else
            {
                Console.WriteLine($"No apartments found for owner {ownerName}.");
            }
        }
        static void ViewSpecificApartment(List<RegionApps> apartments)
        {
            Console.Write("Enter ApartmentId to view: ");
            if (!int.TryParse(Console.ReadLine(), out int apartmentId))
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
                return;
            }

            var specificApartment = apartments.FirstOrDefault(apartment => apartment.ApartmentId == apartmentId);

            if (specificApartment != null)
            {
                specificApartment.DisplayInfo();
                specificApartment.IncrementViewCount(); // Увеличиваем счетчик просмотров с использованием делегата
                Console.WriteLine($"Number of views: {specificApartment.ViewCount}");
            }
            else
            {
                Console.WriteLine($"No apartment found with ApartmentId {apartmentId}.");
            }
        }

        static void FilterApartmentsByRegion(List<RegionApps> apartments)
        {
            Console.Write("Enter region to filter: ");
            string region = Console.ReadLine();

            List<RegionApps> result = RegionApps.FilterByRegion(apartments, region);

            if (result.Count > 0)
            {
                Console.WriteLine($"Apartments in {region} region:");
                foreach (var apartment in result)
                {
                    apartment.DisplayInfo();
                    Console.WriteLine("-------------------------------");
                }
            }
            else
            {
                Console.WriteLine($"No apartments found in region {region}.");
            }
        }

        static void FilterApartmentsByPriceRange(List<RegionApps> apartments)
        {
            Console.Write("Enter minimum price: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal minPrice))
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
                return;
            }

            Console.Write("Enter maximum price: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal maxPrice))
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
                return;
            }

            List<RegionApps> result = RegionApps.FilterByPriceRange(apartments, minPrice, maxPrice);

            if (result.Count > 0)
            {
                Console.WriteLine($"Apartments in the price range ${minPrice} - ${maxPrice}:");
                foreach (var apartment in result)
                {
                    apartment.DisplayInfo();
                    Console.WriteLine("-------------------------------");
                }
            }
            else
            {
                Console.WriteLine($"No apartments found in the price range ${minPrice} - ${maxPrice}.");
            }
        }

        static void AddApartmentForSale()
        {
            Console.WriteLine("Enter details for the new apartment:");

            Console.Write("Owner's Name: ");
            string ownerName = Console.ReadLine();

            Console.Write("Price: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal price))
            {
                Console.WriteLine("Invalid input. Please enter a valid number for the price.");
                return;
            }

            Console.Write("Region: ");
            string region = Console.ReadLine();

            // Генерация нового ApartmentId (может быть логика, основанная на существующих квартирах)
            int newApartmentId = allApartments.Count + 1;

            // Создание новой квартиры и добавление ее в список
            RegionApps newApartment = new RegionApps
            {
                ApartmentId = newApartmentId,
                OwnerName = ownerName,
                Price = price,
                RegionName = region
            };
            allApartments.Add(newApartment);
            RegionApps.WriteApartmentsToXml(newApartment, filePath);

            Console.WriteLine($"Apartment {newApartmentId} has been added for sale.");
        }
    }
}

