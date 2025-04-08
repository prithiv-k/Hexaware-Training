using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingAssignment
{
    class Task2Q6
    {
        static void Main()
        {
            int currentLocation = 0;
            int destination = 100;
            Random random = new Random();
            Console.WriteLine("Tracking courier manually (press any key to update location)...\n");
            while (currentLocation < destination)
            {
                Console.Write("Press any key to get the courier's current location...");
                Console.ReadKey();

                int step = random.Next(1, 11); // move between 1 and 11
                currentLocation += step;

                if (currentLocation > destination)
                {
                    currentLocation = destination;
                }

                Console.WriteLine($"\nCourier is at location: {currentLocation}\n");
            }

            Console.WriteLine("Courier has reached the destination!");
        }
    }
}
