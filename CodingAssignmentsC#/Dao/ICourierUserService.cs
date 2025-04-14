using CodingAssignmentsC_.Entities;
using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    namespace EcommerceApp.DAO
    {
        public interface ICourierUserService<T>
        {
            string PlaceOrder(T courierObj);

            string GetOrderStatus(string trackingNumber);

            bool CancelOrder(string trackingNumber);

            List<T> GetAssignedOrders(long courierStaffId);

           string DeleteCourier(String trackingNumber);

           string GetPaymentAmountByTrackingNumber(string trackingNumber);


    }
}
