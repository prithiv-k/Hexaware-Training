﻿using CodingAssignmentsC_.Entities;
using CodingAssignmentsC_.Util;
using Microsoft.Data.SqlClient;
using System;
using System.Text;

namespace CodingAssignmentsC_.Dao
{
    public class GenericService<T> : ICourierAdminService<T>
    {
        SqlConnection sqlCon = DBConnUtil.GetConnection("appSettings.json");
        SqlCommand cmd = new SqlCommand();

        public int AddCourierStaff(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity), "Entity object cannot be null");
                }

                // Handling logic specific to Employee type
                if (typeof(T) == typeof(Employee))
                {
                    Employee employee = entity as Employee;
                    if (employee != null)
                    {
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

                        // Retrieve the auto-generated ID using SCOPE_IDENTITY()
                        queryBuilder.Clear();
                        queryBuilder.Append("SELECT SCOPE_IDENTITY()");
                        cmd.CommandText = queryBuilder.ToString();

                        int newEmployeeId = Convert.ToInt32(cmd.ExecuteScalar());

                        return newEmployeeId;
                    }
                }

                return -1; // Default return value in case no specific logic matches
            }
            catch (SqlException)
            {
                // Handle SQL errors
                return -1; // Return an error code
            }
            finally
            {
                sqlCon.Close();
            }
        }
    }
}
