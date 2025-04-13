using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingAssignmentsC_.Task4
{
    class Q12
    {
        static void Main(string[] args)
        {
            // Sample input values
            string customerName = "John Doe";
            string orderNumber = "ORD123456";
            string deliveryAddress = "123 Main Street, Springfield";
            DateTime expectedDeliveryDate = DateTime.Today.AddDays(5); // 5 days from today

            // Generate and display the email
            string email = GenerateConfirmationEmail(customerName, orderNumber, deliveryAddress, expectedDeliveryDate);
            Console.WriteLine(email);
        }

        public static string GenerateConfirmationEmail(string name, string orderNo, string address, DateTime deliveryDate)
        {
            string emailBody = $@"
Hello {name},

Thank you for placing your order with us!

Here are your order details:

Order Number   : {orderNo}
Delivery Address: {address}
Expected Delivery Date: {deliveryDate.ToString("dddd, MMMM dd, yyyy")}

We hope to deliver your order on time. You will receive updates as your parcel progresses.

Thank you for choosing our service!

Warm regards,
Courier Express Team
";

            return emailBody;
        }
    }
}
