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
                                employee.District = Convert.ToString(reader["EmpDistrict"]);
                                employee.Language = Convert.ToString(reader["Emplanguage"]); // Ensure correct case
                                employee.PU = Convert.ToString(reader["PU"]); // Ensure correct case
                                employee.PUMapped = Convert.ToString(reader["PUMapped"]); // Ensure correct case
                                employee.DM = Convert.ToString(reader["DM"]); // Ensure correct case
                                employee.CSG = Convert.ToString(reader["CSG"]); // Ensure correct case
                                employee.CSGhead = Convert.ToString(reader["CSGhead"]); // Ensure correct case

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
                                employee.District = Convert.ToString(reader["EmpDistrict"]);
                                employee.Language = Convert.ToString(reader["Emplanguage"]);
                                employee.PU = Convert.ToString(reader["PU"]);
                                employee.PUMapped = Convert.ToString(reader["PUMapped"]);
                                employee.DM = Convert.ToString(reader["DM"]);
                                employee.CSG = Convert.ToString(reader["CSG"]);
                                employee.CSGhead = Convert.ToString(reader["CSGhead"]);


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
        public IEnumerable<string> GetDistinctEmployeeNames()
        {
            List<string> employeeNames = new List<string>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT DISTINCT EmpName FROM Employee";

                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            employeeNames.Add(Convert.ToString(reader["EmpName"]));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // Consider logging the exception
            }

            return employeeNames;
        }
        public IEnumerable<string> GetDistinctEmployeeDistrict()
        {
            List<string> employeeDistrict = new List<string>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT DISTINCT EmpDistrict FROM Employee";

                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            employeeDistrict.Add(Convert.ToString(reader["EmpDistrict"]));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // Consider logging the exception
            }

            return employeeDistrict;
        }

        public IEnumerable<string> GetDistinctPU()
        {
            List<string> employeePU = new List<string>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT DISTINCT PU FROM Employee";

                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            employeePU.Add(Convert.ToString(reader["PU"]));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // Consider logging the exception
            }

            return employeePU;
        }

        public IEnumerable<string> GetDistinctPuMapped()
        {
            List<string> employeePUMapped = new List<string>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT DISTINCT PUMapped FROM Employee";

                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            employeePUMapped.Add(Convert.ToString(reader["PUMapped"]));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // Consider logging the exception
            }

            return employeePUMapped;
        }

        public IEnumerable<string> GetDistinctDM()
        {
            List<string> employeeDM = new List<string>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT DISTINCT DM FROM Employee";

                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            employeeDM.Add(Convert.ToString(reader["DM"]));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // Consider logging the exception
            }

            return employeeDM;
        }
        public IEnumerable<string> GetDistinctCSG()
        {
            List<string> employeeCSG = new List<string>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT DISTINCT CSG FROM Employee";

                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            employeeCSG.Add(Convert.ToString(reader["CSG"]));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // Consider logging the exception
            }

            return employeeCSG;
        }

    }
}
