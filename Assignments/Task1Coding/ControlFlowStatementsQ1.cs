    using System;


namespace Task1Coding
{
    class ControlFlowStatementsQ1
    {
       public static void Main(String[] args)
        {
            //1. Write a program that checks whether a given order is delivered or not based on its status (e.g.,"Processing," "Delivered," "Cancelled"). Use if-else statements for this.
            Console.Write("Enter the Status (ie) Processing,Deleivered,Cancelled: ");
            string Status = Console.ReadLine().ToLower();
            if (Status.Equals("processing"))
            {
                Console.WriteLine("The order is being processed.");
            }
            else if (Status.Equals("deleivered"))
            {
                Console.WriteLine("The order is Deleivered.");

            }
            else
            {
                Console.WriteLine("The order is cancelled.");
            }


        }
    }
}
    
