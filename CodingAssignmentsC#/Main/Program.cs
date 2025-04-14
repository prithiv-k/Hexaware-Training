using CodingAssignmentsC_.Dao;
using CodingAssignmentsC_.Entities;
using CodingAssignmentsC_.Exceptions;
using CodingAssignmentsC_.Service;
using EcommerceApp.DAO;
using System;
using System.Collections.Generic;

namespace CodingAssignmentsC_.Main
{
    public class Program
    {
        static void Main()
        {
            CourierUserServiceImpl courierUserService = new CourierUserServiceImpl();

            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\n=== Courier Management System ===");
                Console.WriteLine("1. Place Order");
                Console.WriteLine("2. Add the new Employee");
                Console.WriteLine("3. Get Order Status");
                Console.WriteLine("4. Cancel Order");
                Console.WriteLine("5. Get Assigned Orders");
                Console.WriteLine("6. Delete Order");
                Console.WriteLine("7. GetPayment Of the Order");
                Console.WriteLine("8. Exit");
                Console.Write("Enter your choice (1-8): ");

                int choice = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();

                switch (choice)
                {
                   
                    case 1:
                        Courier courier = new Courier();
                        courier.CourierID = 15;

                        Console.Write("Enter the SenderName: ");
                        courier.SenderName = Console.ReadLine();
                        Console.Write("Enter the SenderAddress: ");
                        courier.SenderAddress = Console.ReadLine();
                        Console.Write("Enter the ReceiverName: ");
                        courier.ReceiverName = Console.ReadLine();
                        Console.Write("Enter the ReceiverAddress: ");
                        courier.ReceiverAddress = Console.ReadLine();
                        Console.Write("Enter the Weight: ");
                        courier.Weight = Convert.ToDouble(Console.ReadLine());
                        Console.Write("Enter the Status: ");
                        courier.Status = Console.ReadLine();
                        Console.Write("Enter the Tracking Number: ");
                        courier.TrackingNumber = Console.ReadLine();
                        Console.Write("Enter the Delivery Date (dd-mm-yyyy): ");
                        courier.DeliveryDate = DateOnly.Parse(Console.ReadLine());

                        try
                        {
                            Console.Write("Enter the EmployeeID: ");
                            courier.EmployeeID = Convert.ToInt32(Console.ReadLine());

                            // Validate Employee ID
                            if (courier.EmployeeID <= 0) 
                            {
                                throw new InvalidEmployeeIdException("Employee ID must be greater than 0.");
                            }

                            Console.Write("Enter the ServiceID: ");
                            courier.ServiceID = Convert.ToInt32(Console.ReadLine());

                            var trackingNumber = courierUserService.PlaceOrder(courier);
                            Console.WriteLine(trackingNumber != "Error placing order"
                                ? $"Order placed successfully. Tracking Number: {trackingNumber}"
                                : "Error placing order.");
                        }
                        catch (InvalidEmployeeIdException ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Invalid input. Please enter numbers where required.");
                        }
                        break;

                      

                    case 3:
                        Console.Write("Enter the tracking number to get order status: ");
                        string trackingNumberInput = Console.ReadLine();
                        string orderStatus = courierUserService.GetOrderStatus(trackingNumberInput);
                        Console.WriteLine($"Order status: {orderStatus}");
                        break;

                    case 4:
                        Console.Write("Enter the tracking number to cancel the order: ");
                        string cancelTrackingNumber = Console.ReadLine();
                        bool isCancelled = courierUserService.CancelOrder(cancelTrackingNumber);
                        Console.WriteLine(isCancelled ? "Order cancelled successfully." : "Error cancelling the order.");
                        break;

                    case 5:
                        Console.Write("Enter the Employee ID to get assigned orders: ");
                        long courierStaffId = Convert.ToInt64(Console.ReadLine());

                        try
                        {
                            if (courierStaffId <= 0)
                            {
                                throw new InvalidEmployeeIdException();
                            }

                            List<Courier> assignedOrders = courierUserService.GetAssignedOrders(courierStaffId);
                             
                            Console.WriteLine("\nAssigned Orders:");
                            foreach (var assignedOrder in assignedOrders)
                            {
                                Console.WriteLine($"Tracking Number: {assignedOrder.TrackingNumber}, Sender: {assignedOrder.SenderName}, Receiver: {assignedOrder.ReceiverName}, Status: {assignedOrder.Status}");
                            }
                        }
                        catch (InvalidEmployeeIdException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;


                    case 6:
                        Console.Write("Enter Tracking Number to delete courier: ");
                        string deleteTracking = Console.ReadLine();
                        string result = courierUserService.DeleteCourier(deleteTracking);
                        Console.WriteLine(result);
                        break;
                   
                    case 2:
                        Employee newEmp = new Employee();

                        Console.Write("Enter Employee Name: ");
                        newEmp.Name = Console.ReadLine();

                        Console.Write("Enter Email: ");
                        newEmp.Email = Console.ReadLine();

                        Console.Write("Enter Contact Number: ");
                        newEmp.ContactNumber = Console.ReadLine();

                        Console.Write("Enter Role: ");
                        newEmp.Role = Console.ReadLine();

                        Console.Write("Enter Salary: ");
                        newEmp.Salary = Convert.ToDouble(Console.ReadLine());

                        // Create instance of the GenericService
                        GenericService<Employee> employeeService = new GenericService<Employee>();

                        int newEmpId = employeeService.AddCourierStaff(newEmp);

                        if (newEmpId > 0)
                            Console.WriteLine($"Employee added successfully with ID: {newEmpId}");
                        else
                            Console.WriteLine("Failed to add employee.");
                        break;

                    case 7:
                        Console.Write("Enter Tracking Number: ");
                        string TrackingNumber = Console.ReadLine();

                        CourierUserServiceImpl service = new CourierUserServiceImpl();
                        string paymentDetails = service.GetPaymentAmountByTrackingNumber(TrackingNumber);

                        if (paymentDetails.Contains("Payment Amount"))
                        {
                            Console.WriteLine(paymentDetails);  
                        }
                        else
                        {
                            Console.WriteLine(paymentDetails); 
                        }
                        break;  

                 

                


                    case 8:
                        Console.WriteLine("Exiting the application...");
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please enter a valid option.");
                        break;
                }
            }

            Console.WriteLine("Press any key to close...");
            Console.ReadKey();
        }
    }
}
