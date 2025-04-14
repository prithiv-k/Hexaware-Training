using CodingAssignmentsC_.Entities;
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

                       
                        queryBuilder.Clear();
                        queryBuilder.Append("SELECT SCOPE_IDENTITY()");
                        cmd.CommandText = queryBuilder.ToString();

                        int newEmployeeId = Convert.ToInt32(cmd.ExecuteScalar());

                        return newEmployeeId;
                    }
                }

                return -1; 
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
