﻿using System;


namespace CodingAssignment

{
    class ControlFlowStatementsQ2
    {
        //. Implement a switch-case statement to categorize parcels based on their weight into "Light,""Medium," or "Heavy." 
       public static void Main(string[] args)
        {
            Console.Write("Enter the Weight of Product in KG: ");
            int Weight=Convert.ToInt32(Console.ReadLine());
            String Catagory;
            if (Weight < 5)
            {
                Catagory = "Light";
            }
            else if (Weight > 15)
            {
                Catagory = "Heavy";
            }
            else
            {
                Catagory = "Medium";
            }
            switch (Catagory)
            {
                case "Light":
                    Console.WriteLine("This is Light Weight Product");
                    break;
                case "Medium":
                    Console.WriteLine("This is Medium Weight Product");
                    break;
                case "Heavy":
                    Console.WriteLine("This is Heavy Weight Product");
                    break;
                default:
                    Console.WriteLine("Invalid Product");
                    break;
            }
        }
    }
}
