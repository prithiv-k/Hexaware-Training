using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingAssignmentsC_.Task4
{
    class Q14
    {
        static void Main(string[] args)
        {
            string[] addresses = {
                "15 Green Avenue, Bangalore",
                "22 Blue Street, Chennai",
                "16 Green Avenue, Bangalore"
                
            };

            Console.WriteLine("Enter the address to find an exact match:");
            string input = Console.ReadLine().Trim();

            bool matchFound = false;

            foreach (string address in addresses)
            {
                if (string.Equals(input, address, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Exact Match Found: " + address);
                    matchFound = true;
                    break;
                }
            }

            if (!matchFound)
            {
                Console.WriteLine("No exact match found.");
            }
        }
    }
}
