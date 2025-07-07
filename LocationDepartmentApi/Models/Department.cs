using System;

namespace LocationDepartmentApi.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public int LocationId { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentDescription { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
