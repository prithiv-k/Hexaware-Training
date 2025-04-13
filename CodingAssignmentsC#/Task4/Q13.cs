using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingAssignmentsC_.Task4
{
    class Q13
    {
        class Route
        {
            public string Source { get; set; }
            public string Destination { get; set; }
            public int Distance { get; set; }

            public Route(string source, string destination, int distance)
            {
                Source = source.ToLower();
                Destination = destination.ToLower();
                Distance = distance;
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Enter Source Location:");
            string source = Console.ReadLine();

            Console.WriteLine("Enter Destination Location:");
            string destination = Console.ReadLine();

            Console.WriteLine("Enter Parcel Weight (in kg):");
            double weight = Convert.ToDouble(Console.ReadLine());

            double cost = CalculateShippingCost(source, destination, weight);

            if (cost == -1)
            {
                Console.WriteLine("Distance between given locations is not available.");
            }
            else
            {
                Console.WriteLine($"Shipping Cost from {source} to {destination} for {weight} kg: ${cost}");
            }
        }

        static double CalculateShippingCost(string source, string destination, double weight)
        {
            List<Route> routes = new List<Route>
            {
                new Route("Chennai", "Bangalore", 350),
                new Route("Bangalore", "Hyderabad", 570),
                new Route("Chennai", "Hyderabad", 630),
                new Route("Hyderabad", "Bangalore", 570),
                new Route("Bangalore", "Chennai", 350),
                new Route("Hyderabad", "Chennai", 630)
            };

            source = source.ToLower();
            destination = destination.ToLower();

            foreach (var route in routes)
            {
                if (route.Source == source && route.Destination == destination)
                {
                    double ratePerKmPerKg = 5.0;
                    return route.Distance * weight * ratePerKmPerKg;
                }
            }

            return -1;
        }
       }
}
