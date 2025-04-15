using CodingAssignmentsC_.Dao;
using CodingAssignmentsC_.Entities;
using CodingAssignmentsC_.Service;
using CodingAssignmentsC_.Util;
using Microsoft.Data.SqlClient;
using System;
using System.Text;

namespace CodingAssignmentsC_.Services
{
    public class CourierAdminServiceImpl : CourierUserServiceImpl, ICourierAdminService<Employee>
    {
        SqlConnection sqlCon = DBConnUtil.GetConnection("appSettings.json");
        SqlCommand cmd = new SqlCommand();

        public int AddCourierStaff(Employee employee)
        {
            try
            {
                if (employee == null)
                {
                    throw new ArgumentNullException(nameof(employee), "Employee object cannot be null");
                }

                cmd.Connection = sqlCon;

                StringBuilder queryBuilder = new StringBuilder();
                queryBuilder.Append("INSERT INTO Employee (Name, Email, ContactNumber, Role, Salary) ");
                queryBuilder.Append($"VALUES ('{employee.Name}', '{employee.Email}', '{employee.ContactNumber}', '{employee.Role}', {employee.Salary})");

                cmd.CommandText = queryBuilder.ToString();

                if (sqlCon.State == System.Data.ConnectionState.Closed)
                {
                    sqlCon.Open();
                }

                cmd.ExecuteNonQuery();

                // Retrive the auto generated Emp Id using SCOPE_IDENTITY()
                queryBuilder.Clear();
                queryBuilder.Append("SELECT SCOPE_IDENTITY()");
                cmd.CommandText = queryBuilder.ToString();

                int newEmployeeId = Convert.ToInt32(cmd.ExecuteScalar());

                return newEmployeeId;
            }
            catch (SqlException)
            {
               
                return -1;
            }
            finally
            {
                sqlCon.Close();
            }
        }
    }
}
