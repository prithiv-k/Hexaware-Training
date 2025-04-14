using CodingAssignmentsC_.Entities;
using CodingAssignmentsC_.Exceptions;
using CodingAssignmentsC_.Util;
using EcommerceApp.DAO;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingAssignmentsC_.Service
{
    public class CourierUserServiceImpl : ICourierUserService<Courier>  
    {
        protected CourierCompany companyObj;

      
        public List<Courier> GetAllCouriers()
        {
            return companyObj.Couriers; 
        }

     
        SqlConnection sqlCon = DBConnUtil.GetConnection("appSettings.json");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;

        public string PlaceOrder(Courier courierObj)
        {
            try
            {
             // DateOnly dateOnly = DateOnly.FromDateTime(DateTime.Now.AddDays(3));


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


        public string GetOrderStatus(string trackingNumber)
        {
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
                    string status = string.Empty;
                    while (dr.Read())
                    {
                        status = Convert.ToString(dr["Status"]);
                    }
                    dr.Close();
                    return status;
                }
                else
                {
                    dr.Close();
                    throw new TrackingNumberNotFoundException();
                }
            }
            catch (TrackingNumberNotFoundException)
            {
                return "Tracking number not found.";
            }
            catch (SqlException)
            {
                return "Error retrieving status.";
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

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    throw new TrackingNumberNotFoundException();
                }

                return true;
            }
            catch (TrackingNumberNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}"); 
                return false;
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error occurred while cancelling the order. Details: {ex.Message}");
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

                if (rowsAffected == 0)
                {
                    throw new TrackingNumberNotFoundException();
                }

                return "Courier deleted successfully.";
            }
            catch (TrackingNumberNotFoundException ex)
            {
                return ex.Message;
            }
            catch (SqlException)
            {
                return "Database error occurred while deleting the courier.";
            }
            finally
            {
                sqlCon.Close();
            }
        }



        public string GetPaymentAmountByTrackingNumber(string trackingNumber)
        {
            try
            {
                cmd.Connection = sqlCon;

                /
                cmd.CommandText = $"SELECT ServiceID FROM Courier WHERE TrackingNumber = '{trackingNumber}'";

                if (sqlCon.State == System.Data.ConnectionState.Closed)
                    sqlCon.Open();

                object serviceIdObj = cmd.ExecuteScalar();

                if (serviceIdObj == null)
                {
                    throw new TrackingNumberNotFoundException();
                }

                int serviceId = Convert.ToInt32(serviceIdObj);

               
                cmd.CommandText = $"SELECT Cost FROM CourierServices WHERE ServiceID = {serviceId}";
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    if (dr["Cost"] != DBNull.Value)
                    {
                        return dr["Cost"].ToString();
                    }
                }

                dr.Close();
                return "No Amount Found";
            }
            catch (TrackingNumberNotFoundException ex)
            {
                return ex.Message;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                return "Error retrieving payment amount.";
            }
            finally
            {
                sqlCon.Close();
            }
        }

    }

}
