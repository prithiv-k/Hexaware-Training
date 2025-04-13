using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingAssignmentsC_.Task4
{
    class Q11
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter Street:");
            string street = Console.ReadLine();

            Console.WriteLine("Enter City:");
            string city = Console.ReadLine();

            Console.WriteLine("Enter State:");
            string state = Console.ReadLine();

            Console.WriteLine("Enter Zip Code:");
            string zip = Console.ReadLine();

            string formattedAddress = FormatAddress(street, city, state, zip);
            Console.WriteLine("\nFormatted Address:");
            Console.WriteLine(formattedAddress);
        }

        public static string FormatAddress(string street, string city, string state, string zipCode)
        {
            street = CapitalizeEachWord(street);
            city = CapitalizeEachWord(city);
            state = CapitalizeEachWord(state);

            // Format ZIP code to ensure it's 5 digits
            if (zipCode.Length != 6 || !int.TryParse(zipCode, out _))
            {
                zipCode = "Invalid ZIP Code";
            }

            return $"{street}, {city}, {state} - {zipCode}";
        }

        public static string CapitalizeEachWord(string input)
        {
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            return textInfo.ToTitleCase(input.ToLower());
        }
    }
}
