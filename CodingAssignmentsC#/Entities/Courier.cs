using System;

namespace CodingAssignmentsC_.Entities
{
    public class Courier
    {
        public int CourierID {  get; set; }
        public string? SenderName { get; set; }
        public string? SenderAddress { get; set; }
        public string? ReceiverName { get; set; }
        public string? ReceiverAddress { get; set; }
        public double? Weight { get; set; }
        public string? Status { get;  set; }
        public string? TrackingNumber {  get; set; }    
        public DateOnly? DeliveryDate { get; set; }
        public int ? EmployeeID { get; set; }
        public int? ServiceID {  get; set; }

     

    }

}