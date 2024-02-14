using DemoProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using OfficeOpenXml;
using System.Drawing;
using PagedList;

namespace DemoProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataAccessLayer _dataAccessLayer;

        public HomeController(DataAccessLayer dataAccessLayer)
        {
            _dataAccessLayer = dataAccessLayer;
        }

        static HomeController()
        {
            // Set EPPlus license context
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        public IActionResult Index()
        {
            var employee = BindDropDown();
            var employee2 = BindDropDown2();
            employee.EmployeeList2 = employee2.EmployeeList2;
          
            return View(employee);
        }

        [HttpPost]
        public IActionResult Index(Employee emp, string action)
        {
            if (action == "Download")
            {
                var employeeData1 = _dataAccessLayer.GetEmployees(emp.StartDate, emp.EndDate);
                return DownloadExcel(employeeData1, emp.StartDate, emp.EndDate);
            }

            var employeeData = _dataAccessLayer.GetEmployees(emp.StartDate, emp.EndDate);
            if (employeeData != null)
            {
                ViewBag.EmployeeList = employeeData;
               
                    foreach (var empl in ViewBag.EmployeeList)
                    {
                        Console.WriteLine($"ID: {empl.Id}, Name: {empl.Name}, Joining Date: {empl.JoiningDate}, District: {empl.District}, Language: {empl.Language}, PU: {empl.PU}, PUMapped: {empl.PUMapped}, DM: {empl.DM}, CSG: {empl.CSG}, CSG Head: {empl.CSGhead}");
                    }
                
            }
            ViewBag.SelectedStartDate = emp.StartDate;
            ViewBag.SelectedEndDate = emp.EndDate;

            var employee = BindDropDown();
            var employee2 = BindDropDown2();
            var employee3 = BindDropDown3();
            var employee4 = BindDropDown4();
            var employee5 = BindDropDown5();
            var employee6 = BindDropDown6();
            var employee7 = BindDropDown7();
            var employee8 = BindDropDown8();
            employee.EmployeeList2 = employee2.EmployeeList2;
            employee.EmployeeList3 = employee3.EmployeeList3;
            employee.EmployeeList4 = employee4.EmployeeList4;
            employee.EmployeeList5 = employee5.EmployeeList5;
            employee.EmployeeList6 = employee6.EmployeeList6;
            employee.EmployeeList7 = employee7.EmployeeList7;
            employee.EmployeeList8 = employee8.EmployeeList8;

            return View(employee);
        }

        private IActionResult DownloadExcel(IEnumerable<Employee> employees, DateTime StartDate, DateTime EndDate)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (var package = new ExcelPackage(memoryStream))
                {
                    var worksheet = package.Workbook.Worksheets.Add("Employees");


                    // Merge cells for the title
                    worksheet.Cells["A1:C1"].Merge = true;
                    worksheet.Cells[1, 1].Value = "Customer Details";
                    worksheet.Cells[1, 1, 1, 2].Style.Font.Bold = true;

                    worksheet.Cells["D1:E1"].Merge = true;
                    worksheet.Cells[1, 3].Value = StartDate.ToString("yyyy-MM-dd");
                    worksheet.Cells[1, 3, 1, 4].Style.Font.Bold = true;

                    worksheet.Cells["F1:G1"].Merge = true;
                    worksheet.Cells[1, 5].Value = EndDate.ToString("yyyy-MM-dd"); 
                    worksheet.Cells[1, 5, 1, 6].Style.Font.Bold = true; 

                    worksheet.Cells["H1:J1"].Merge = true;
                    worksheet.Cells[1, 7].Value = "Variance";  
                    worksheet.Cells[1, 7, 1, 9].Style.Font.Bold = true;

                    // Add column headers
                    worksheet.Cells["A2"].Value = "ID";
                    worksheet.Cells["B2"].Value = "Name";
                    worksheet.Cells["C2"].Value = "Joining Date";
                    worksheet.Cells["D2"].Value = "District";
                    worksheet.Cells["E2"].Value = "Language";
                    worksheet.Cells["F2"].Value = "PU";
                    worksheet.Cells["G2"].Value = "PU Mapped";
                    worksheet.Cells["H2"].Value = "DM";
                    worksheet.Cells["I2"].Value = "CSG";
                    worksheet.Cells["J2"].Value = "CSG Head";

                    // Apply styling to the header row
                    using (var range = worksheet.Cells["A1:J1"])
                    {
                        range.Style.Font.Bold = true;
                        range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    }
                    using (var range = worksheet.Cells["A2:J2"])
                    {
                        range.Style.Font.Bold = true;
                        range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    }

                    int row = 3; // Start from the next row for data
                    foreach (var employee in employees)
                    {
                        worksheet.Cells[row, 1].Value = employee.Id;
                        worksheet.Cells[row, 2].Value = employee.Name;
                        worksheet.Cells[row, 3].Value = employee.JoiningDate.ToString("yyyy-MM-dd");
                        worksheet.Cells[row, 4].Value = employee.District;
                        worksheet.Cells[row, 5].Value = employee.Language;
                        worksheet.Cells[row, 6].Value = employee.PU;
                        worksheet.Cells[row, 7].Value = employee.PUMapped;
                        worksheet.Cells[row, 8].Value = employee.DM;
                        worksheet.Cells[row, 9].Value = employee.CSG;
                        worksheet.Cells[row, 10].Value = employee.CSGhead;
                        row++;
                    }

                    worksheet.Cells.AutoFitColumns();

                    package.Save();
                }

                return File(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "employees.xlsx");
            }
        }



        private Employee BindDropDown()
        {
            Employee employee = new Employee();
            employee.EmployeeList = new List<SelectListItem>();
            var data = _dataAccessLayer.GetAllEmployees();

            employee.EmployeeList.Add(new SelectListItem
            {
                Text = "--Select Start Year--",
                Value = ""
            });

            foreach (var item in data)
            {
                employee.EmployeeList.Add(new SelectListItem
                {
                    Text = item.JoiningDate.ToString(),
                    Value = item.JoiningDate.ToString()
                });
            }
            return employee;
        }

        private Employee BindDropDown2()
        {
            Employee employee = new Employee();
            employee.EmployeeList2 = new List<SelectListItem>();
            var data = _dataAccessLayer.GetAllEmployees();

            employee.EmployeeList2.Add(new SelectListItem
            {
                Text = "--Select End Year--",
                Value = ""
            });

            foreach (var item in data)
            {
                employee.EmployeeList2.Add(new SelectListItem
                {
                    Text = item.JoiningDate.ToString(),
                    Value = item.JoiningDate.ToString()
                });
            }
            return employee;
        }
        private Employee BindDropDown3()
        {
            Employee employee = new Employee();
            employee.EmployeeList3 = new List<SelectListItem>();
            var data = _dataAccessLayer.GetDistinctEmployeeNames();

            employee.EmployeeList3.Add(new SelectListItem
            {
                Text = "--Select Employee--",
                Value = ""
            });

            foreach (var name in data)
            {
                employee.EmployeeList3.Add(new SelectListItem
                {
                    Text = name,
                    Value = name
                });
            }
            return employee;
        }
        private Employee BindDropDown4()
        {
            Employee employee = new Employee();
            employee.EmployeeList4 = new List<SelectListItem>();
            var data = _dataAccessLayer.GetDistinctEmployeeDistrict();

            employee.EmployeeList4.Add(new SelectListItem
            {
                Text = "--Select District--",
                Value = ""
            });

            foreach (var name in data)
            {
                employee.EmployeeList4.Add(new SelectListItem
                {
                    Text = name,
                    Value = name
                });
            }
            return employee;
        }

        private Employee BindDropDown5()
        {
            Employee employee = new Employee();
            employee.EmployeeList5 = new List<SelectListItem>();
            var data = _dataAccessLayer.GetDistinctPU();

            employee.EmployeeList5.Add(new SelectListItem
            {
                Text = "--Select PU--",
                Value = ""
            });

            foreach (var name in data)
            {
                employee.EmployeeList5.Add(new SelectListItem
                {
                    Text = name,
                    Value = name
                });
            }
            return employee;
        }

        private Employee BindDropDown6()
        {
            Employee employee = new Employee();
            employee.EmployeeList6 = new List<SelectListItem>();
            var data = _dataAccessLayer.GetDistinctPuMapped();

            employee.EmployeeList6.Add(new SelectListItem
            {
                Text = "--Select PUMapped--",
                Value = ""
            });

            foreach (var name in data)
            {
                employee.EmployeeList6.Add(new SelectListItem
                {
                    Text = name,
                    Value = name
                });
            }
            return employee;
        }

        private Employee BindDropDown7()
        {
            Employee employee = new Employee();
            employee.EmployeeList7 = new List<SelectListItem>();
            var data = _dataAccessLayer.GetDistinctDM();

            employee.EmployeeList7.Add(new SelectListItem
            {
                Text = "--Select DM--",
                Value = ""
            });

            foreach (var name in data)
            {
                employee.EmployeeList7.Add(new SelectListItem
                {
                    Text = name,
                    Value = name
                });
            }
            return employee;
        }

        private Employee BindDropDown8()
        {
            Employee employee = new Employee();
            employee.EmployeeList8 = new List<SelectListItem>();
            var data = _dataAccessLayer.GetDistinctCSG();

            employee.EmployeeList4.Add(new SelectListItem
            {
                Text = "--Select CSG--",
                Value = ""
            });

            foreach (var name in data)
            {
                employee.EmployeeList8.Add(new SelectListItem
                {
                    Text = name,
                    Value = name
                });
            }
            return employee;
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
