using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingAssignment
{
    class ControlFlowStatementsQ4
    {
        static void Main(string[] args)
        {
            // Sample couriers with proximity (in km) and available load capacity (in kg)
            string[] courierNames = { "CourierA", "CourierB", "CourierC" };
            int[] proximities = { 5, 10, 3 }; // in km
            int[] loadCapacities = { 20, 50, 30 }; // in kg

            // Input: shipment details
            Console.WriteLine("Enter shipment weight (in kg):");
            int shipmentWeight = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter shipment distance (in km):");
            int shipmentDistance = Convert.ToInt32(Console.ReadLine());

            bool assigned = false;

            for (int i = 0; i < courierNames.Length; i++)
            {
                if (proximities[i] <= shipmentDistance && loadCapacities[i] >= shipmentWeight)
                {
                    Console.WriteLine($"Shipment assigned to {courierNames[i]}.");
                    assigned = true;
                    break; // Stop after assigning the first matching courier
                }
            }

            if (!assigned)
            {
                Console.WriteLine("No suitable courier found for this shipment.");
            }
        }
    }
}
