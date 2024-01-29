using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;

namespace DemoProject.Models
{
    public class DataAccessLayer
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DataAccessLayer(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("connectionString");
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            List<Employee> lstEmployee = new List<Employee>();
            string query = "SELECT * FROM EMPLOYEE";

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Employee employee = new Employee();

                                employee.Id = Convert.ToInt32(reader["EmpId"]);
                                employee.Name = Convert.ToString(reader["EmpName"]);
                                employee.JoiningDate = Convert.ToDateTime(reader["EmpJoiningDate"]);

                                lstEmployee.Add(employee);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // Consider logging the exception
            }

            return lstEmployee;
        }

        public IEnumerable<Employee> GetEmployees(DateTime startDate, DateTime endDate)
        {
            List<Employee> lstEmployee = new List<Employee>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand("sp_employee", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@StartDate", startDate));
                        command.Parameters.Add(new SqlParameter("@EndDate", endDate));

                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Employee employee = new Employee();

                                employee.Id = Convert.ToInt32(reader["EmpId"]);
                                employee.Name = Convert.ToString(reader["EmpName"]);
                                employee.JoiningDate = Convert.ToDateTime(reader["EmpJoiningDate"]);

                                lstEmployee.Add(employee);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // Consider logging the exception
            }

            return lstEmployee;
        }
    }
}
