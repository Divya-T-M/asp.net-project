using Microsoft.AspNetCore.Mvc.Rendering;

namespace DemoProject.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime JoiningDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<SelectListItem> EmployeeList { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> EmployeeList2 { get; set; } = new List<SelectListItem>();

    }
}
