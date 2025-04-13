using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingAssignmentsC_.Task4
{
    class Q9
    {
        static void Main(string[] args)
        {
            // Initialize the tracking array with tracking number and status
            string[,] trackingData = {
                { "P123", "In Transit" },
                { "P124", "Out for Delivery" },
                { "P125", "Delivered" },
                { "P126", "In Transit" },
                { "P127", "Delivered" }
            };

            Console.WriteLine("Enter the tracking number (P123 to P127):");
            string inputTrackingNumber = Console.ReadLine();

            bool found = false;

            for (int i = 0; i < trackingData.GetLength(0); i++)
            {
                if (trackingData[i, 0] == inputTrackingNumber)
                {
                    string status = trackingData[i, 1];

                    switch (status)
                    {
                        case "In Transit":
                            Console.WriteLine("Parcel is currently in transit.");
                            break;
                        case "Out for Delivery":
                            Console.WriteLine("Parcel is out for delivery.");
                            break;
                        case "Delivered":
                            Console.WriteLine("Parcel has been delivered.");
                            break;
                        default:
                            Console.WriteLine("Status unknown.");
                            break;
                    }

                    found = true;
                    break;
                }
            }

            if (!found)
            {
                Console.WriteLine("Tracking number not found.");
            }
        }
    }
}
