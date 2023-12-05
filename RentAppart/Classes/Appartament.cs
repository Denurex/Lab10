using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentApparts.Classes
{
    // Базовый класс Apartment
    public class Appartment
    {
        public int ApartmentId { get; set; }
        public string OwnerName { get; set; }
        public decimal Price { get; set; }

        // Виртуальный метод для отображения информации о квартире
        public virtual void DisplayInfo()
        {
            Console.WriteLine($"Apartment ID: {ApartmentId}");
            Console.WriteLine($"Owner: {OwnerName}");
            Console.WriteLine($"Price: ${Price}");
        }

        // Еще один метод базового класса
        public void Reserve()
        {
            Console.WriteLine("The apartment has been reserved.");
        }

        // Статический метод базового класса
        public static void StaticMethod()
        {
            Console.WriteLine("This is a static method of the Apartment class.");
        }
    }
}
