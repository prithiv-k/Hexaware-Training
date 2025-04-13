using CodingAssignmentsC_.Entities;
using CodingAssignmentsC_.Util;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceApp.DAO
{
    public class CourierUserService : ICourierUserService<Courier>
    {
        SqlConnection sqlCon = DBConnUtil.GetConnection("appSettings.json");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;

        public string PlaceOrder(Courier courierObj)
        {
            try
            {
                // Generate tracking number using static variable
                courierObj.TrackingNumber = Courier.GenerateTrackingNumber();



                DateOnly dateOnly = DateOnly.FromDateTime(DateTime.Now.AddDays(3)); // Delivery in 3 days


                cmd.Connection = sqlCon;
                StringBuilder queryBuilder = new StringBuilder();
                queryBuilder.Append($"INSERT INTO Courier (CourierID,SenderName, SenderAddress, ReceiverName, ReceiverAddress, Weight, Status, TrackingNumber, DeliveryDate, EmployeeID, ServiceID) ");
                queryBuilder.Append($"VALUES ('{courierObj.CourierID}','{courierObj.SenderName}', '{courierObj.SenderAddress}', '{courierObj.ReceiverName}', '{courierObj.ReceiverAddress}', {courierObj.Weight}, '{courierObj.Status}', '{courierObj.TrackingNumber}', '{courierObj.DeliveryDate}', {courierObj.EmployeeID}, {courierObj.ServiceID})");

                cmd.CommandText = queryBuilder.ToString();
                if (sqlCon.State == System.Data.ConnectionState.Closed)
                {
                    sqlCon.Open();
                }

                cmd.ExecuteNonQuery();
                return courierObj.TrackingNumber;
            }
            catch (SqlException ex)
            {
                return "SQL Error: " + ex.Message;
            }

            finally
            {
                sqlCon.Close();
            }
        }

        // Other methods like GetOrderStatus, CancelOrder, etc., can follow the same pattern
        public string GetOrderStatus(string trackingNumber)
        {
            string status = "Not Found";

            try
            {
                cmd.Connection = sqlCon;
                StringBuilder queryBuilder = new StringBuilder();
                queryBuilder.Append($"SELECT Status FROM Courier WHERE TrackingNumber = '{trackingNumber}'");
                cmd.CommandText = queryBuilder.ToString();

                if (sqlCon.State == System.Data.ConnectionState.Closed)
                {
                    sqlCon.Open();
                }

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        status = Convert.ToString(dr["Status"]);
                    }
                    dr.Close();
                }

                return status;
            }
            catch (SqlException)
            {
                return "Error retrieving status";
            }
            finally
            {
                sqlCon.Close();
            }
        }
        public bool CancelOrder(string trackingNumber)
        {
            try
            {
                cmd.Connection = sqlCon;
                StringBuilder queryBuilder = new StringBuilder();
                queryBuilder.Append($"UPDATE Courier SET Status = 'Cancelled' WHERE TrackingNumber = '{trackingNumber}'");
                cmd.CommandText = queryBuilder.ToString();

                if (sqlCon.State == System.Data.ConnectionState.Closed)
                {
                    sqlCon.Open();
                }

                int rowsAffected = cmd.ExecuteNonQuery(); // returns number of affected rows

                return rowsAffected > 0; // if at least one row is updated, return true
            }
            catch (SqlException)
            {
                return false;
            }
            finally
            {
                sqlCon.Close();
            }
        }
        public List<Courier> GetAssignedOrders(long courierStaffId)
        {
            List<Courier> assignedCouriers = new List<Courier>();

            try
            {
                cmd.Connection = sqlCon;
                StringBuilder queryBuilder = new StringBuilder();
                queryBuilder.Append($"SELECT * FROM Courier WHERE EmployeeID = {courierStaffId}");
                cmd.CommandText = queryBuilder.ToString();

                if (sqlCon.State == System.Data.ConnectionState.Closed)
                {
                    sqlCon.Open();
                }

                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Courier courier = new Courier();
                        courier.CourierID = Convert.ToInt32(dr["CourierID"]);
                        courier.SenderName = Convert.ToString(dr["SenderName"]);
                        courier.SenderAddress = Convert.ToString(dr["SenderAddress"]);
                        courier.ReceiverName = Convert.ToString(dr["ReceiverName"]);
                        courier.ReceiverAddress = Convert.ToString(dr["ReceiverAddress"]);
                        courier.Weight = Convert.ToDouble(dr["Weight"]);
                        courier.Status = Convert.ToString(dr["Status"]);
                        courier.TrackingNumber = Convert.ToString(dr["TrackingNumber"]);
                        courier.DeliveryDate = DateOnly.FromDateTime(Convert.ToDateTime(dr["DeliveryDate"]));
                        courier.EmployeeID = Convert.ToInt32(dr["EmployeeID"]);
                        courier.ServiceID = Convert.ToInt32(dr["ServiceID"]);

                        assignedCouriers.Add(courier);
                    }
                    dr.Close();
                }

                return assignedCouriers;
            }
            catch (SqlException)
            {
                return assignedCouriers;
            }
            finally
            {
                sqlCon.Close();
            }
        }
        public string DeleteCourier(string trackingNumber)
        {
            try
            {
                cmd.Connection = sqlCon;

                StringBuilder queryBuilder = new StringBuilder();
                queryBuilder.Append($"DELETE FROM Courier WHERE TrackingNumber = '{trackingNumber}'");

                cmd.CommandText = queryBuilder.ToString();

                if (sqlCon.State == System.Data.ConnectionState.Closed)
                {
                    sqlCon.Open();
                }

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    return $"Order with Tracking Number {trackingNumber} deleted successfully.";
                }
                else
                {
                    return $"No order found with Tracking Number {trackingNumber}.";
                }
            }
            catch (SqlException ex)
            {
                return "SQL Error: " + ex.Message;
            }
            finally
            {
                sqlCon.Close();
            }
        }

    }
}
