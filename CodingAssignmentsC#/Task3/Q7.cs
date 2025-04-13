using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingAssignmentsC_.Task3
{
    class Q7
    {
        static void Main()
        {
            string[] trackingHistory = new string[5];

            // Simulate tracking history updates
            trackingHistory[0] = "Warehouse";
            trackingHistory[1] = "Dispatched";
            trackingHistory[2] = "City Hub";
            trackingHistory[3] = "Out for Delivery";
            trackingHistory[4] = "Delivered";

            Console.WriteLine("Tracking History:");
            foreach (string location in trackingHistory)
            {
                Console.WriteLine(location);
            }
        }
    }
}
