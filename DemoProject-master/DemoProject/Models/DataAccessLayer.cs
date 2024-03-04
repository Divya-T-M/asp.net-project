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
                                employee.CSGhead = Convert.ToString(reader["CSGhead"]);
                                employee.PU = Convert.ToString(reader["PU"]); // Ensure correct case
                                employee.PUMapped = Convert.ToString(reader["PUMapped"]); // Ensure correct case
                                employee.DM = Convert.ToString(reader["DM"]); // Ensure correct case
                                employee.CSG = Convert.ToString(reader["CSG"]); // Ensure correct case
                                employee.State = Convert.ToString(reader["State"]); // Ensure correct case
                                //employee.RevVar = Convert.ToDouble(reader["RevVar"]);
                                //employee.VolVar = Convert.ToDouble(reader["VolVar"]);

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
                                employee.State = Convert.ToString(reader["State"]);
                                employee.RevVar = Convert.ToDouble(reader["RevVar"]);
                                employee.VolVar = Convert.ToDouble(reader["VolVar"]);


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


        public IEnumerable<GraphData> GetGraphDataForChart(DateTime startDate, DateTime endDate)
        {
            List<GraphData> graphData = new List<GraphData>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand command = new SqlCommand("sp_employee_2", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            GraphData data = new GraphData();
                            data.Date = reader.GetDateTime(reader.GetOrdinal("EmpJoiningDate")).ToString("yyyy-MM-dd");
                            data.RevVar = reader.GetDouble(reader.GetOrdinal("RevVar"));
                            data.VolVar = reader.GetDouble(reader.GetOrdinal("VolVar"));
                            graphData.Add(data);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return graphData;
        }



    }
}
