using System;

namespace myDictionary
{
    class Program
    {
        static void Main(string[] args)
        {
            var countries = new Dict<int, string>
            {
                new Item<int, string>(1, "Russia"),
                new Item<int, string>(3, "Great Britain"),
                new Item<int, string>(2, "USA"),
                new Item<int, string>(4, "France"),
                new Item<int, string>(5, "Camboja"),
                new Item<int, string>(6, "India"),
                new Item<int, string>(7, "China"),
                new Item<int, string>(8, "China"),
                new Item<int, string>(9, "France"),
                new Item<int, string>(9, "France"),
                new Item<int, string>(10, "USA"),
                new Item<int, string>(11, "Britain"),
                new Item<int, string>(11, "Britain")
            };

            foreach (var country in countries)
            {
                Console.WriteLine(country);
            }
            Console.WriteLine(countries.Search(12) ?? "Не існує !!!");
            Console.WriteLine(countries.Search(4) ?? "Не існує !!!");

            countries.Remove(4);
            countries.Remove(5);
            foreach (var country in countries)
            {
                Console.WriteLine(country);
            }
        }
    }
}