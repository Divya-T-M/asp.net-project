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
            }
            ViewBag.SelectedStartDate = emp.StartDate;
            ViewBag.SelectedEndDate = emp.EndDate;

            var employee = BindDropDown();
            var employee2 = BindDropDown2();
            var employee3 = BindDropDown3();
            var employee4 = BindDropDown4();
            employee.EmployeeList2 = employee2.EmployeeList2;
            employee.EmployeeList3 = employee3.EmployeeList3;
            employee.EmployeeList4 = employee4.EmployeeList4;

            return View(employee);
        }

        //[HttpPost]
        //public IActionResult SearchEmployee(string employeeName)
        //{
        //    var employeeData = _dataAccessLayer.SearchEmployeesByName(employeeName);
        //    ViewBag.ShowSearchDropdown = true; // Ensure the search dropdown is shown after searching
        //    return PartialView("_EmployeeListPartial", employeeData);
        //}

        private IActionResult DownloadExcel(IEnumerable<Employee> employees, DateTime StartDate, DateTime EndDate)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (var package = new ExcelPackage(memoryStream))
                {
                    var worksheet = package.Workbook.Worksheets.Add("Employees");

                    // Merge cells for the title
                    worksheet.Cells["A1:B1"].Merge = true;
                    worksheet.Cells[1, 1].Value = StartDate.ToString("yyyy-MM-dd");
                    worksheet.Cells[1, 1, 1, 2].Style.Font.Bold = true;

                    worksheet.Cells["C1:D1"].Merge = true;
                    worksheet.Cells[1, 3].Value = EndDate.ToString("yyyy-MM-dd");
                    worksheet.Cells[1, 3, 1, 4].Style.Font.Bold = true;
                    // Add column headers
                    worksheet.Cells["A2"].Value = "ID";
                    worksheet.Cells["B2"].Value = "Name";
                    worksheet.Cells["C2"].Value = "Joining Date";
                    worksheet.Cells["D2"].Value = "District";

                    // Apply styling to the header row
                    using (var range = worksheet.Cells["A1:D1"])
                    {
                        range.Style.Font.Bold = true;
                        range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    }
                    using (var range = worksheet.Cells["A2:D2"])
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
