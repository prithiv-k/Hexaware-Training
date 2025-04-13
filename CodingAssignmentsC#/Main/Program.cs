using CodingAssignmentsC_.Dao;
using CodingAssignmentsC_.Entities;
using EcommerceApp.DAO;
using System;
using System.Collections.Generic;

namespace CodingAssignmentsC_.Main
{
    public class Program
    {
        static void Main()
        {
            CourierUserService courierUserService = new CourierUserService();
            Courier courier = new Courier();

            // Example for placing an order
            courier.CourierID = 400;
            courier.SenderName = "Prithiv";
            courier.SenderAddress = "123 Main St";
            courier.ReceiverName = "Jane Smith";
            courier.ReceiverAddress = "456 Elm St";
            courier.Weight = 2.5;
            courier.Status = "Deleivered";
            courier.TrackingNumber = "TRK400";
            courier.DeliveryDate = null;

            courier.EmployeeID = 2  ; // Example Employee ID
            courier.ServiceID = 5; // Example Service ID

            var trackingNumber = courierUserService.PlaceOrder(courier);
            Console.WriteLine(trackingNumber != "Error placing order" ? $"Order placed successfully. Tracking Number: {trackingNumber}" : "Error placing order.");
            Console.WriteLine();
            Console.WriteLine();

            // Example for getting order status
            Console.Write("Enter the tracking number to get order status: ");
            string trackingNumberInput = Console.ReadLine();
            string orderStatus = courierUserService.GetOrderStatus(trackingNumberInput);
            Console.WriteLine($"Order status: {orderStatus}");
            Console.WriteLine(); Console.WriteLine();

            // Example for cancelling an order
            Console.Write("Enter the tracking number to cancel the order :");
            string cancelTrackingNumber = Console.ReadLine();
            bool isCancelled = courierUserService.CancelOrder(cancelTrackingNumber);
            Console.WriteLine(isCancelled ? "Order cancelled successfully." : "Error cancelling the order.");
            Console.WriteLine(); Console.WriteLine();

            // Example for getting assigned orders
            Console.Write("Enter the courier staff ID to get assigned orders: ");
            long courierStaffId = Convert.ToInt64(Console.ReadLine());
            List<Courier> assignedOrders = courierUserService.GetAssignedOrders(courierStaffId);
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("Assigned Orders:");
            foreach (var assignedOrder in assignedOrders)
            {
                Console.WriteLine($"Tracking Number: {assignedOrder.TrackingNumber}, Sender: {assignedOrder.SenderName}, Receiver: {assignedOrder.ReceiverName}, Status: {assignedOrder.Status}");
            }
            Console.WriteLine(); Console.WriteLine();
            Console.Write("Enter TrakingNumber to DeleteCourier: ");
            String DeleteCour=Console.ReadLine();
            string result = courierUserService.DeleteCourier(trackingNumber);
            Console.WriteLine(result);

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
