using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace CodingAssignmentsC_.Entities
{
    public class CourierCompanyCollection
    {
        private List<Courier> courierList;

        public CourierCompanyCollection()
        {
            this.courierList = new List<Courier>(); 
        }

        public List<Courier> GetCourierList()
        {
            return courierList;
        }

        public void AddCourier(Courier courier)
        {
            courierList.Add(courier);
        }

        public void RemoveCourier(string trackingNumber)
        {
            courierList.RemoveAll(c => c.TrackingNumber == trackingNumber); 
        }
    }
}
