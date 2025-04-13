using System;


namespace CodingAssignmentsC_.Task3
{
    class Q8
    {
        static void Main(string[] args)
        {
            // Array of courier names
            string[] couriers = { "Ravi", "Anu", "Mani", "Priya" };

            // Distances from the new order location in kilometers
            int[] distances = { 12, 5, 8, 6 };

            // Availability of each courier (true means available)
            bool[] isAvailable = { true, false, true, true };

            string nearestCourier = FindNearestAvailableCourier(couriers, distances, isAvailable);

            if (nearestCourier != null)
            {
                Console.WriteLine("Nearest available courier is: " + nearestCourier);
            }
            else
            {
                Console.WriteLine("No available couriers at the moment.");
            }
        }

        static string FindNearestAvailableCourier(string[] names, int[] distances, bool[] availability)
        {
            int minDistance = int.MaxValue;
            string nearestCourier = null;

            for (int i = 0; i < names.Length; i++)
            {
                if (availability[i] && distances[i] < minDistance)
                {
                    minDistance = distances[i];
                    nearestCourier = names[i];
                }
            }

            return nearestCourier;
        }
    }
}
