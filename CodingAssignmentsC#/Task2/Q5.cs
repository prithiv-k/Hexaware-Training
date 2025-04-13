using System;


namespace CodingAssignmentsC_.Task2
{
    class Q5
    {
        public static void Main()
        {
            // Sample data
            string[] customers = { "Alice", "Bob", "Alice", "Charlie", "Bob", "Alice" };
            string[] orders = {
                "Order #101", "Order #102", "Order #103",
                "Order #104", "Order #105", "Order #106"
            };

            Console.Write("Enter customer name to view their orders: ");
            string customerName = Console.ReadLine();

            Console.WriteLine($"\nOrders for {customerName}:");
            bool found = false;

            for (int i = 0; i < customers.Length; i++)
            {
                if (customers[i].Equals(customerName))
                {
                    Console.WriteLine(orders[i]);
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("No orders found for this customer.");
            }
        }
    }
}
