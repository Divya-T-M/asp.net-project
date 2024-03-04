using Microsoft.AspNetCore.Mvc.Rendering;

namespace DemoProject.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public string Language { get; set; } = string.Empty;
        public string PU { get; set; } = string.Empty;
        public string PUMapped { get; set; } = string.Empty;
        public string DM { get; set; } = string.Empty;
        public string CSG { get; set; } = string.Empty;
        public string CSGhead { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public double RevVar { get; set; }
        public double VolVar { get; set; }

        public DateTime JoiningDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<SelectListItem> EmployeeList { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> EmployeeList2 { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> EmployeeList3 { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> EmployeeList4 { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> EmployeeList5 { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> EmployeeList6 { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> EmployeeList7 { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> EmployeeList8 { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> EmployeeList9 { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> EmployeeList10 { get; set; } = new List<SelectListItem>();

    }
}
