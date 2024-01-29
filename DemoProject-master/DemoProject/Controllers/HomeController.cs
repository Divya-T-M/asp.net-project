using DemoProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using OfficeOpenXml;

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
                return DownloadExcel(employeeData1);
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

        private IActionResult DownloadExcel(IEnumerable<Employee> employees)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (var package = new ExcelPackage(memoryStream))
                {
                    var worksheet = package.Workbook.Worksheets.Add("Employees");

                    worksheet.Cells[1, 1].Value = "ID";
                    worksheet.Cells[1, 2].Value = "Name";
                    worksheet.Cells[1, 3].Value = "Joining Date";

                    int row = 2;
                    foreach (var employee in employees)
                    {
                        worksheet.Cells[row, 1].Value = employee.Id;
                        worksheet.Cells[row, 2].Value = employee.Name;
                        worksheet.Cells[row, 3].Value = employee.JoiningDate.ToString("yyyy-MM-dd");
                        row++;
                    }

                    worksheet.Cells.AutoFitColumns(0);

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
