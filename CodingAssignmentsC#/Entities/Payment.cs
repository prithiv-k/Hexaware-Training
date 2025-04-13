using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingAssignmentsC_.Entities
{
    public class Payment
    {
        public int PaymentID { get; set; }
        public int? CourierID { get; set; }
        public int LocaationID { get; set; }
        public double? Amount { get; set; } 
        public DateTime? PaymentDate { get; set; }

    }
}
