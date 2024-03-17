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
            var employee3 = BindDropDown3();
            var employee4 = BindDropDown4();
            var employee5 = BindDropDown5();
            var employee6 = BindDropDown6();
            var employee7 = BindDropDown7();
            var employee8 = BindDropDown8();
            var employee9 = BindDropDown9();
            var employee10 = BindDropDown10();
            var employee11 = BindDropDown11();
            employee.EmployeeList2 = employee2.EmployeeList2;
            employee.EmployeeList3 = employee3.EmployeeList3;
            employee.EmployeeList4 = employee4.EmployeeList4;
            employee.EmployeeList5 = employee5.EmployeeList5;
            employee.EmployeeList6 = employee6.EmployeeList6;
            employee.EmployeeList7 = employee7.EmployeeList7;
            employee.EmployeeList8 = employee8.EmployeeList8;
            employee.EmployeeList9 = employee9.EmployeeList9;
            employee.EmployeeList10 = employee10.EmployeeList10;
            employee.EmployeeList11 = employee11.EmployeeList11;


            return View(employee);
        }

        [HttpPost]
        public IActionResult Index(Employee emp, string action)
        {
            bool blnRevenue = false;
            bool blnVolume = false;

            var employee = BindDropDown();
            var employee2 = BindDropDown2();
            var employee3 = BindDropDown3();
            var employee4 = BindDropDown4();
            var employee5 = BindDropDown5();
            var employee6 = BindDropDown6();
            var employee7 = BindDropDown7();
            var employee8 = BindDropDown8();
            var employee9 = BindDropDown9();
            var employee10 = BindDropDown10();
            var employee11 = BindDropDown11();

            employee.EmployeeList2 = employee2.EmployeeList2;
            employee.EmployeeList3 = employee3.EmployeeList3;
            employee.EmployeeList4 = employee4.EmployeeList4;
            employee.EmployeeList5 = employee5.EmployeeList5;
            employee.EmployeeList6 = employee6.EmployeeList6;
            employee.EmployeeList7 = employee7.EmployeeList7;
            employee.EmployeeList8 = employee8.EmployeeList8;
            employee.EmployeeList9 = employee9.EmployeeList9;
            employee.EmployeeList10 = employee10.EmployeeList10;
            employee.EmployeeList11 = employee11.EmployeeList11;

            var employeeData = _dataAccessLayer.GetEmployees(emp.StartDate, emp.EndDate);

            if (action == "Download")
            {
                if (employeeData != null)
                {
                    if (!string.IsNullOrEmpty(emp.Name))
                    {
                        employeeData = employeeData.Where(e => e.Name == emp.Name).ToList();
                    }
                    if (!string.IsNullOrEmpty(emp.District))
                    {
                        employeeData = employeeData.Where(e => e.District == emp.District).ToList();
                    }
                    if (!string.IsNullOrEmpty(emp.PU))
                    {
                        employeeData = employeeData.Where(e => e.PU == emp.PU).ToList();
                    }
                    if (!string.IsNullOrEmpty(emp.State))
                    {
                        employeeData = employeeData.Where(e => e.CSGhead == emp.State).ToList();
                    }
                    if (!string.IsNullOrEmpty(emp.PUMapped))
                    {
                        employeeData = employeeData.Where(e => e.PUMapped == emp.PUMapped).ToList();
                    }
                    if (!string.IsNullOrEmpty(emp.DM))
                    {
                        employeeData = employeeData.Where(e => e.DM == emp.DM).ToList();
                    }
                    if (!string.IsNullOrEmpty(emp.CSG))
                    {
                        employeeData = employeeData.Where(e => e.CSG == emp.CSG).ToList();
                    }
                    if (!string.IsNullOrEmpty(emp.Language))
                    {
                        employeeData = employeeData.Where(e => e.Language == emp.Language).ToList();
                    }
                }

                return DownloadExcel(employeeData, emp.StartDate, emp.EndDate);
            }
            else if (action == "RevDownload")
            {
                if (employeeData != null)
                {
                    if (!string.IsNullOrEmpty(emp.Name))
                    {
                        employeeData = employeeData.Where(e => e.Name == emp.Name).ToList();
                    }
                    if (!string.IsNullOrEmpty(emp.District))
                    {
                        employeeData = employeeData.Where(e => e.District == emp.District).ToList();
                    }
                    if (!string.IsNullOrEmpty(emp.PU))
                    {
                        employeeData = employeeData.Where(e => e.PU == emp.PU).ToList();
                    }
                    if (!string.IsNullOrEmpty(emp.State))
                    {
                        employeeData = employeeData.Where(e => e.CSGhead == emp.State).ToList();
                    }
                    if (!string.IsNullOrEmpty(emp.PUMapped))
                    {
                        employeeData = employeeData.Where(e => e.PUMapped == emp.PUMapped).ToList();
                    }
                    if (!string.IsNullOrEmpty(emp.DM))
                    {
                        employeeData = employeeData.Where(e => e.DM == emp.DM).ToList();
                    }
                    if (!string.IsNullOrEmpty(emp.CSG))
                    {
                        employeeData = employeeData.Where(e => e.CSG == emp.CSG).ToList();
                    }
                    if (!string.IsNullOrEmpty(emp.Language))
                    {
                        employeeData = employeeData.Where(e => e.Language == emp.Language).ToList();
                    }
                }
                return DownloadRevenue(employeeData, emp.StartDate, emp.EndDate);
            }
            else if (action == "VolDownload")
            {
                if (employeeData != null)
                {
                    if (!string.IsNullOrEmpty(emp.Name))
                    {
                        employeeData = employeeData.Where(e => e.Name == emp.Name).ToList();
                    }
                    if (!string.IsNullOrEmpty(emp.District))
                    {
                        employeeData = employeeData.Where(e => e.District == emp.District).ToList();
                    }
                    if (!string.IsNullOrEmpty(emp.PU))
                    {
                        employeeData = employeeData.Where(e => e.PU == emp.PU).ToList();
                    }
                    if (!string.IsNullOrEmpty(emp.State))
                    {
                        employeeData = employeeData.Where(e => e.CSGhead == emp.State).ToList();
                    }
                    if (!string.IsNullOrEmpty(emp.PUMapped))
                    {
                        employeeData = employeeData.Where(e => e.PUMapped == emp.PUMapped).ToList();
                    }
                    if (!string.IsNullOrEmpty(emp.DM))
                    {
                        employeeData = employeeData.Where(e => e.DM == emp.DM).ToList();
                    }
                    if (!string.IsNullOrEmpty(emp.CSG))
                    {
                        employeeData = employeeData.Where(e => e.CSG == emp.CSG).ToList();
                    }
                    if (!string.IsNullOrEmpty(emp.Language))
                    {
                        employeeData = employeeData.Where(e => e.Language == emp.Language).ToList();
                    }
                }
                return DownloadVolume(employeeData, emp.StartDate, emp.EndDate);
            }
            else if (action == "Graph")
            {
                var selectedDates = Request.Form["Date"]; 
                if (!string.IsNullOrEmpty(selectedDates))
                {
                    return RedirectToAction("Graph", new { dates = selectedDates });
                }
                else
                {   
                    TempData["ErrorMessage"] = "Please select at least one date.";
                    return RedirectToAction("Index");
                }
            }
            else if (action == "Revenue")
            {
                blnRevenue = true;
            }
            else if (action == "Volume")
            {
                blnVolume = true;
            }

            if (employeeData != null)
            {
                if (!string.IsNullOrEmpty(emp.Name))
                {
                    employeeData = employeeData.Where(e => e.Name == emp.Name).ToList();
                }
                if (!string.IsNullOrEmpty(emp.District))
                {
                    employeeData = employeeData.Where(e => e.District == emp.District).ToList();
                }
                if (!string.IsNullOrEmpty(emp.PU))
                {
                    employeeData = employeeData.Where(e => e.PU == emp.PU).ToList();
                }
                if (!string.IsNullOrEmpty(emp.State))
                {
                    employeeData = employeeData.Where(e => e.CSGhead == emp.State).ToList();
                }
                if (!string.IsNullOrEmpty(emp.PUMapped))
                {
                    employeeData = employeeData.Where(e => e.PUMapped == emp.PUMapped).ToList();
                }
                if (!string.IsNullOrEmpty(emp.DM))
                {
                    employeeData = employeeData.Where(e => e.DM == emp.DM).ToList();
                }
                if (!string.IsNullOrEmpty(emp.CSG))
                {
                    employeeData = employeeData.Where(e => e.CSG == emp.CSG).ToList();
                }
                if (!string.IsNullOrEmpty(emp.Language))
                {
                    employeeData = employeeData.Where(e => e.Language == emp.Language).ToList();
                }
                ViewBag.EmployeeList = employeeData;
            }
            ViewBag.blnRevenue = blnRevenue;
            ViewBag.blnVolume = blnVolume;
            ViewBag.SelectedStartDate = emp.StartDate;
            ViewBag.SelectedEndDate = emp.EndDate;

            if (!string.IsNullOrEmpty(emp.State))
            {
                var districts = _dataAccessLayer.GetDistrictsByState(emp.State);
                var districtListItems = districts.Select(d => new SelectListItem { Text = d, Value = d }).ToList();
                districtListItems.Insert(0, new SelectListItem { Text = "--Select District--", Value = "" });

                employee.EmployeeList4 = districtListItems;
            }
            if (!string.IsNullOrEmpty(emp.CSGhead))
            {
                var CSG = _dataAccessLayer.GetCSGbyCSGhead(emp.CSGhead);
                var CSGListItems = CSG.Select(d => new SelectListItem { Text = d, Value = d }).ToList();
                CSGListItems.Insert(0, new SelectListItem { Text = "--Select CSG--", Value = "" });

                employee.EmployeeList8 = CSGListItems;
            }

            if (!string.IsNullOrEmpty(emp.PU))
            {
                var PUMapped = _dataAccessLayer.GetPUMappedbyPU(emp.PU);
                var PUMappedListItems = PUMapped.Select(d => new SelectListItem { Text = d, Value = d }).ToList();
                PUMappedListItems.Insert(0, new SelectListItem { Text = "--Select PUMapped--", Value = "" });

                employee.EmployeeList8 = PUMappedListItems;
            }
            //var employeet = _dataAccessLayer.GetAllEmployees().ToList();
            //int totalRecords = employeet.Count();
            //int pageSize = 5;
            //int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
            //int recSkip = (currentPage - 1) * pageSize;
            //var data = employeet.Skip(recSkip).Take(pageSize).ToList();

           
            //emp.CurrentPage = currentPage;
            //emp.PageSize = pageSize;
            //emp.TotalPages = totalPages;
           
            return View(employee);
        }

        //private IEnumerable<Employee> FilterEmployeeData(Employee emp, IEnumerable<Employee> employeeData)
        //{
        //    if (!string.IsNullOrEmpty(emp.Name))
        //    {
        //        employeeData = employeeData.Where(e => e.Name == emp.Name).ToList();
        //    }
        //    if (!string.IsNullOrEmpty(emp.District))
        //    {
        //        employeeData = employeeData.Where(e => e.District == emp.District).ToList();
        //    }
        //    if (!string.IsNullOrEmpty(emp.PU))
        //    {
        //        employeeData = employeeData.Where(e => e.PU == emp.PU).ToList();
        //    }
        //    if (!string.IsNullOrEmpty(emp.State))
        //    {
        //        employeeData = employeeData.Where(e => e.CSGhead == emp.State).ToList();
        //    }
        //    if (!string.IsNullOrEmpty(emp.PUMapped))
        //    {
        //        employeeData = employeeData.Where(e => e.PUMapped == emp.PUMapped).ToList();
        //    }
        //    if (!string.IsNullOrEmpty(emp.DM))
        //    {
        //        employeeData = employeeData.Where(e => e.DM == emp.DM).ToList();
        //    }
        //    if (!string.IsNullOrEmpty(emp.CSG))
        //    {
        //        employeeData = employeeData.Where(e => e.CSG == emp.CSG).ToList();
        //    }
        //    if (!string.IsNullOrEmpty(emp.Language))
        //    {
        //        employeeData = employeeData.Where(e => e.Language == emp.Language).ToList();
        //    }
        //    return employeeData;
        //}

       [HttpGet]
        public IActionResult GetDistrictsByState(Employee emp)
        {
            var districts = _dataAccessLayer.GetDistrictsByState(emp.State);
            var districtListItems = districts.Select(d => new SelectListItem { Text = d, Value = d }).ToList();
            districtListItems.Insert(0, new SelectListItem { Text = "--Select District--", Value = "" });

            return Json(districtListItems);
        }

        [HttpGet]
        public IActionResult GetCSGbyCSGhead(Employee emp)
        {
            var CSG = _dataAccessLayer.GetCSGbyCSGhead(emp.CSGhead);
            var CSGListItems = CSG.Select(d => new SelectListItem { Text = d, Value = d }).ToList();
            CSGListItems.Insert(0, new SelectListItem { Text = "--Select CSG--", Value = "" });

            return Json(CSGListItems);
        }

        [HttpGet]
        public IActionResult GetPUMappedbyPU(Employee emp)
        {
            var PUMapped = _dataAccessLayer.GetPUMappedbyPU(emp.PU);
            var PUMappedListItems = PUMapped.Select(d => new SelectListItem { Text = d, Value = d }).ToList();
            PUMappedListItems.Insert(0, new SelectListItem { Text = "--Select PUMapped--", Value = "" });        
            return Json(PUMappedListItems);
        }
        //[HttpPost]
        public IActionResult Graph(string[] dates)
        {
            List<DateTime> selectedDates = new List<DateTime>();

            foreach (string dateString in dates)
            {
                DateTime parsedDate;
                if (DateTime.TryParse(dateString, out parsedDate))
                {
                    selectedDates.Add(parsedDate);
                }
                else
                {
                    // Handle invalid date string
                    // For example:
                    Console.WriteLine($"Invalid date string: {dateString}");
                }
            }
            var graphData = _dataAccessLayer.GetGraphDataForChart(selectedDates);
            return View(graphData);
        }


        private IActionResult DownloadExcel(IEnumerable<Employee> employees, DateTime StartDate, DateTime EndDate)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (var package = new ExcelPackage(memoryStream))
                {
                    var worksheet = package.Workbook.Worksheets.Add("Employees");


                    // Merge cells for the title
                    worksheet.Cells["A1:D1"].Merge = true;
                    worksheet.Cells["A1"].Value = "Customer Details";
                    worksheet.Cells["A1:D1"].Style.Font.Bold = true;

                    worksheet.Cells["E1:F1"].Merge = true;
                    worksheet.Cells["E1"].Value = StartDate.ToString("yyyy-MM-dd");
                    worksheet.Cells["E1:F1"].Style.Font.Bold = true;

                    worksheet.Cells["G1:H1"].Merge = true;
                    worksheet.Cells["G1"].Value = EndDate.ToString("yyyy-MM-dd");
                    worksheet.Cells["G1:H1"].Style.Font.Bold = true;

                    worksheet.Cells["I1:K1"].Merge = true;
                    worksheet.Cells["I1"].Value = "Stater";
                    worksheet.Cells["I1:K1"].Style.Font.Bold = true;

                    worksheet.Cells["L1:M1"].Merge = true;
                    worksheet.Cells["L1"].Value = "Variance";
                    worksheet.Cells["L1:M1"].Style.Font.Bold = true;


                    // Add column headers
                    worksheet.Cells["A2"].Value = "ID";
                    worksheet.Cells["B2"].Value = "Name";
                    worksheet.Cells["C2"].Value = "Joining Date";
                    worksheet.Cells["D2"].Value = "State";
                    worksheet.Cells["E2"].Value = "District";
                    worksheet.Cells["F2"].Value = "Language";
                    worksheet.Cells["G2"].Value = "PU";
                    worksheet.Cells["H2"].Value = "PU Mapped";
                    worksheet.Cells["I2"].Value = "DM";
                    worksheet.Cells["J2"].Value = "CSG Head";
                    worksheet.Cells["K2"].Value = "CSG";
                    worksheet.Cells["L2"].Value = "RevVar";
                    worksheet.Cells["M2"].Value = "VolVar";


                    // Apply styling to the header row
                    using (var range = worksheet.Cells["A1:M1"])
                    {
                        range.Style.Font.Bold = true;
                        range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    }
                    using (var range = worksheet.Cells["A2:M2"])
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
                        worksheet.Cells[row, 4].Value = employee.State;
                        worksheet.Cells[row, 5].Value = employee.District;
                        worksheet.Cells[row, 6].Value = employee.Language;
                        worksheet.Cells[row, 7].Value = employee.PU;
                        worksheet.Cells[row, 8].Value = employee.PUMapped;
                        worksheet.Cells[row, 9].Value = employee.DM;
                        worksheet.Cells[row, 10].Value = employee.CSGhead;
                        worksheet.Cells[row, 11].Value = employee.CSG;
                        worksheet.Cells[row, 12].Value = employee.RevVar;
                        worksheet.Cells[row, 13].Value = employee.VolVar;
                        row++;
                    }

                    worksheet.Cells.AutoFitColumns();

                    package.Save();
                }

                return File(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "employees.xlsx");
            }
        }

        private IActionResult DownloadRevenue(IEnumerable<Employee> employees, DateTime StartDate, DateTime EndDate)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (var package = new ExcelPackage(memoryStream))
                {
                    var worksheet = package.Workbook.Worksheets.Add("Employees");


                    // Merge cells for the title
                    worksheet.Cells["A1:D1"].Merge = true;
                    worksheet.Cells["A1"].Value = "Customer Details";
                    worksheet.Cells["A1:D1"].Style.Font.Bold = true;

                    worksheet.Cells["E1:F1"].Merge = true;
                    worksheet.Cells["E1"].Value = StartDate.ToString("yyyy-MM-dd");
                    worksheet.Cells["E1:F1"].Style.Font.Bold = true;

                    worksheet.Cells["G1:H1"].Merge = true;
                    worksheet.Cells["G1"].Value = EndDate.ToString("yyyy-MM-dd");
                    worksheet.Cells["G1:H1"].Style.Font.Bold = true;

                    worksheet.Cells["I1:K1"].Merge = true;
                    worksheet.Cells["I1"].Value = "Stater";
                    worksheet.Cells["I1:K1"].Style.Font.Bold = true;

                    worksheet.Cells["L1:M1"].Merge = true;
                    worksheet.Cells["L1"].Value = "Variance";
                    worksheet.Cells["L1:M1"].Style.Font.Bold = true;


                    // Add column headers
                    worksheet.Cells["A2"].Value = "ID";
                    worksheet.Cells["B2"].Value = "Name";
                    worksheet.Cells["C2"].Value = "Joining Date";
                    worksheet.Cells["D2"].Value = "State";
                    worksheet.Cells["E2"].Value = "District";
                    worksheet.Cells["F2"].Value = "Language";
                    worksheet.Cells["G2"].Value = "PU";
                    worksheet.Cells["H2"].Value = "PU Mapped";
                    worksheet.Cells["I2"].Value = "DM";
                    worksheet.Cells["J2"].Value = "CSG Head";
                    worksheet.Cells["K2"].Value = "CSG";
                    worksheet.Cells["L2"].Value = "RevVar";



                    // Apply styling to the header row
                    using (var range = worksheet.Cells["A1:M1"])
                    {
                        range.Style.Font.Bold = true;
                        range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    }
                    using (var range = worksheet.Cells["A2:M2"])
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
                        worksheet.Cells[row, 4].Value = employee.State;
                        worksheet.Cells[row, 5].Value = employee.District;
                        worksheet.Cells[row, 6].Value = employee.Language;
                        worksheet.Cells[row, 7].Value = employee.PU;
                        worksheet.Cells[row, 8].Value = employee.PUMapped;
                        worksheet.Cells[row, 9].Value = employee.DM;
                        worksheet.Cells[row, 10].Value = employee.CSGhead;
                        worksheet.Cells[row, 11].Value = employee.CSG;
                        worksheet.Cells[row, 12].Value = employee.RevVar;

                        row++;
                    }

                    worksheet.Cells.AutoFitColumns();

                    package.Save();
                }

                return File(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Revenue.xlsx");
            }
        }

        private IActionResult DownloadVolume(IEnumerable<Employee> employees, DateTime StartDate, DateTime EndDate)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (var package = new ExcelPackage(memoryStream))
                {
                    var worksheet = package.Workbook.Worksheets.Add("Employees");


                    // Merge cells for the title
                    worksheet.Cells["A1:D1"].Merge = true;
                    worksheet.Cells["A1"].Value = "Customer Details";
                    worksheet.Cells["A1:D1"].Style.Font.Bold = true;

                    worksheet.Cells["E1:F1"].Merge = true;
                    worksheet.Cells["E1"].Value = StartDate.ToString("yyyy-MM-dd");
                    worksheet.Cells["E1:F1"].Style.Font.Bold = true;

                    worksheet.Cells["G1:H1"].Merge = true;
                    worksheet.Cells["G1"].Value = EndDate.ToString("yyyy-MM-dd");
                    worksheet.Cells["G1:H1"].Style.Font.Bold = true;

                    worksheet.Cells["I1:K1"].Merge = true;
                    worksheet.Cells["I1"].Value = "Stater";
                    worksheet.Cells["I1:K1"].Style.Font.Bold = true;

                    worksheet.Cells["L1:M1"].Merge = true;
                    worksheet.Cells["L1"].Value = "Variance";
                    worksheet.Cells["L1:M1"].Style.Font.Bold = true;


                    // Add column headers
                    worksheet.Cells["A2"].Value = "ID";
                    worksheet.Cells["B2"].Value = "Name";
                    worksheet.Cells["C2"].Value = "Joining Date";
                    worksheet.Cells["D2"].Value = "State";
                    worksheet.Cells["E2"].Value = "District";
                    worksheet.Cells["F2"].Value = "Language";
                    worksheet.Cells["G2"].Value = "PU";
                    worksheet.Cells["H2"].Value = "PU Mapped";
                    worksheet.Cells["I2"].Value = "DM";
                    worksheet.Cells["J2"].Value = "CSG Head";
                    worksheet.Cells["K2"].Value = "CSG";
                    worksheet.Cells["L2"].Value = "VolVar";



                    // Apply styling to the header row
                    using (var range = worksheet.Cells["A1:M1"])
                    {
                        range.Style.Font.Bold = true;
                        range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    }
                    using (var range = worksheet.Cells["A2:M2"])
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
                        worksheet.Cells[row, 4].Value = employee.State;
                        worksheet.Cells[row, 5].Value = employee.District;
                        worksheet.Cells[row, 6].Value = employee.Language;
                        worksheet.Cells[row, 7].Value = employee.PU;
                        worksheet.Cells[row, 8].Value = employee.PUMapped;
                        worksheet.Cells[row, 9].Value = employee.DM;
                        worksheet.Cells[row, 10].Value = employee.CSGhead;
                        worksheet.Cells[row, 11].Value = employee.CSG;
                        worksheet.Cells[row, 12].Value = employee.VolVar;

                        row++;
                    }

                    worksheet.Cells.AutoFitColumns();

                    package.Save();
                }

                return File(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Volume.xlsx");
            }
        }


        private Employee BindDropDown()
        {
            Employee employee = new Employee();
            employee.EmployeeList = new List<SelectListItem>();
            var data = _dataAccessLayer.GetAllEmployees().Select(e => e.JoiningDate).Distinct()
                                                   .OrderBy(d => d);

            employee.EmployeeList.Add(new SelectListItem
            {
                Text = "--Select Start Year--",
                Value = ""
            });

            foreach (var item in data)
            {
                employee.EmployeeList.Add(new SelectListItem
                {
                    Text = item.ToString(),
                    Value = item.ToString()
                });
            }
            return employee;
        }

        private Employee BindDropDown2()
        {
            Employee employee = new Employee();
            employee.EmployeeList2 = new List<SelectListItem>();
            var data = _dataAccessLayer.GetAllEmployees().Select(e => e.JoiningDate).Distinct()
                                                   .OrderBy(d => d);

            employee.EmployeeList2.Add(new SelectListItem
            {
                Text = "--Select End Year--",
                Value = ""
            });

            foreach (var item in data)
            {
                employee.EmployeeList2.Add(new SelectListItem
                {
                    Text = item.ToString(),
                    Value = item.ToString()
                });
            }
            return employee;
        }
        private Employee BindDropDown3()
        {
            Employee employee = new Employee();
            employee.EmployeeList3 = new List<SelectListItem>();

            var data = _dataAccessLayer.GetAllEmployees()
                                                   .Select(e => e.Name)
                                                   .Distinct()
                                                   .OrderBy(d => d);

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

            var data = _dataAccessLayer.GetAllEmployees()
                                                   .Select(e => e.District)
                                                   .Distinct()
                                                   .OrderBy(d => d);

            employee.EmployeeList4.Add(new SelectListItem
            {
                Text = "--Select District--",
                Value = ""
            });

            foreach (var item in data)
            {
                employee.EmployeeList4.Add(new SelectListItem
                {
                    Text = item,
                    Value = item
                });
            }
            return employee;
        }

        private Employee BindDropDown5()
        {
            Employee employee = new Employee();
            employee.EmployeeList5 = new List<SelectListItem>();
            var data = _dataAccessLayer.GetAllEmployees()
                                                   .Select(e => e.PU)
                                                   .Distinct()
                                                   .OrderBy(d => d);

            employee.EmployeeList5.Add(new SelectListItem
            {
                Text = "--Select PU--",
                Value = ""
            });

            foreach (var item in data)
            {
                employee.EmployeeList5.Add(new SelectListItem
                {
                    Text = item,
                    Value = item
                });
            }
            return employee;
        }

        private Employee BindDropDown6()
        {
            Employee employee = new Employee();
            employee.EmployeeList6 = new List<SelectListItem>();
            var data = _dataAccessLayer.GetAllEmployees()
                                                   .Select(e => e.PUMapped)
                                                   .Distinct()
                                                   .OrderBy(d => d);

            employee.EmployeeList6.Add(new SelectListItem
            {
                Text = "--Select PUMapped--",
                Value = ""
            });

            foreach (var item in data)
            {
                employee.EmployeeList6.Add(new SelectListItem
                {
                    Text = item,
                    Value = item
                });
            }
            return employee;
        }

        private Employee BindDropDown7()
        {
            Employee employee = new Employee();
            employee.EmployeeList7 = new List<SelectListItem>();
            var data = _dataAccessLayer.GetAllEmployees()
                                                   .Select(e => e.DM)
                                                   .Distinct()
                                                   .OrderBy(d => d);

            employee.EmployeeList7.Add(new SelectListItem
            {
                Text = "--Select DM--",
                Value = ""
            });

            foreach (var item in data)
            {
                employee.EmployeeList7.Add(new SelectListItem
                {
                    Text = item,
                    Value = item
                });
            }
            return employee;
        }

        private Employee BindDropDown8()
        {
            Employee employee = new Employee();
            employee.EmployeeList8 = new List<SelectListItem>();
            var data = _dataAccessLayer.GetCSG();

            employee.EmployeeList8.Add(new SelectListItem
            {
                Text = "--Select CSG--",
                Value = ""
            });

            foreach (var item in data)
            {
                employee.EmployeeList8.Add(new SelectListItem
                {
                    Text = item,
                    Value = item
                });
            }
            return employee;
        }
        private Employee BindDropDown9()
        {
            Employee employee = new Employee();
            employee.EmployeeList9 = new List<SelectListItem>();
            var data = _dataAccessLayer.GetCSGhead();

            employee.EmployeeList9.Add(new SelectListItem
            {
                Text = "--Select CSGHead--",
                Value = ""
            });

            foreach (var item in data)
            {
                employee.EmployeeList9.Add(new SelectListItem
                {
                    Text = item,
                    Value = item
                });
            }
            return employee;
        }
        private Employee BindDropDown10()
        {
            Employee employee = new Employee();
            employee.EmployeeList10 = new List<SelectListItem>();
            var data = _dataAccessLayer.GetAllEmployees()
                                                   .Select(e => e.State)
                                                   .Distinct()
                                                   .OrderBy(d => d);

            employee.EmployeeList10.Add(new SelectListItem
            {
                Text = "--Select State--",
                Value = ""
            });

            foreach (var item in data)
            {
                employee.EmployeeList10.Add(new SelectListItem
                {
                    Text = item,
                    Value = item
                });
            }
            return employee;
        }
        private Employee BindDropDown11()
        {
            Employee employee = new Employee();
            employee.EmployeeList11 = new List<SelectListItem>();
            var data = _dataAccessLayer.GetJoiningDate();

            employee.EmployeeList11.Add(new SelectListItem
            {
                Text = "--Select Date--",
                Value = ""
            });

            foreach (var item in data)
            {
                employee.EmployeeList11.Add(new SelectListItem
                {
                    Text = item.ToString(),
                    Value = item.ToString()
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
