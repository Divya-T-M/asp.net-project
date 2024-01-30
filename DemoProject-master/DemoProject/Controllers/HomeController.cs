using DemoProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using OfficeOpenXml;
using System.Drawing;

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
                string takenBy = "Default User"; // Provide default value for "Taken By"
                DateTime actionDate = DateTime.Now; // Provide default value for "Action Date"
                return DownloadExcel(employeeData1, takenBy, actionDate);
            }

            var employeeData = _dataAccessLayer.GetEmployees(emp.StartDate, emp.EndDate);
            if (employeeData != null)
            {
                ViewBag.EmployeeList = employeeData;
            }

            var employee = BindDropDown();
            var employee2 = BindDropDown2();
            employee.EmployeeList2 = employee2.EmployeeList2;

            return View(employee);
        }

        private IActionResult DownloadExcel(IEnumerable<Employee> employees, string takenBy, DateTime actionDate)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (var package = new ExcelPackage(memoryStream))
                {
                    var worksheet = package.Workbook.Worksheets.Add("Employees");

                    // Merge cells for the title
                    worksheet.Cells["A1:C2"].Merge = true;
                    worksheet.Cells[1, 1].Value = "Demo List";
                    worksheet.Cells[1, 1, 2, 3].Style.Font.Bold = true;

                    // Merge cells for "Taken By" information
                    worksheet.Cells["A4:C4"].Merge = true;
                    worksheet.Cells[4, 1].Value = "Taken By";
                    worksheet.Cells[4, 4].Value = ": " + takenBy;

                    // Merge cells for "Action Date" information
                    worksheet.Cells["A5:C5"].Merge = true;
                    worksheet.Cells[5, 1].Value = "Action Date";
                    worksheet.Cells[5, 4].Value = ": " + actionDate.ToString("dd-MM-yyyy , hh:mm tt");

                    // Add column headers
                    worksheet.Cells["A7"].Value = "ID";
                    worksheet.Cells["B7"].Value = "Name";
                    worksheet.Cells["C7"].Value = "Joining Date";

                    // Apply styling to the header row
                    using (var range = worksheet.Cells["A7:C7"])
                    {
                        range.Style.Font.Bold = true;
                        range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    }

                    int row = 8; // Start from the next row for data
                    foreach (var employee in employees)
                    {
                        worksheet.Cells[row, 1].Value = employee.Id;
                        worksheet.Cells[row, 2].Value = employee.Name;
                        worksheet.Cells[row, 3].Value = employee.JoiningDate.ToString("yyyy-MM-dd");
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
