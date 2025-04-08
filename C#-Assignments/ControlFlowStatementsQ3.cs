using System;


namespace CodingAssignment
{
    class ControlFlowStatementsQ3
    {
        static void Main(string[] args)
        {
            // Predefined credentials
            string employeeUsername = "employee";
            string employeePassword = "emp123";

            string customerUsername = "customer";
            string customerPassword = "cust123";

            Console.WriteLine("Login as: (1) Employee or (2) Customer");
            int userType = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Username: ");
            string username = Console.ReadLine();

            Console.Write("Enter Password: ");
            string password = Console.ReadLine();

            if (userType == 1) // Employee Login
            {
                if (username == employeeUsername && password == employeePassword)
                {
                    Console.WriteLine("Employee login successful!");
                }
                else
                {
                    Console.WriteLine("Invalid employee credentials.");
                }
            }
            else if (userType == 2) // Customer Login
            {
                if (username == customerUsername && password == customerPassword)
                {
                    Console.WriteLine("Customer login successful!");
                }
                else
                {
                    Console.WriteLine("Invalid customer credentials.");
                }
            }
            else
            {
                Console.WriteLine("Invalid user type selected.");
            }
        }
    }
}
