using System.Diagnostics.Contracts;

namespace HRMSystem.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; } = null!;

        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; } = null!;

        public int? ContractId { get; set; }
        public virtual EmploymentContract? Contract { get; set; }

        public virtual Salary? Salary { get; set; } // Sửa lại quan hệ với Salary

        public DateTime HireDate { get; set; }

        public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
        public virtual ICollection<Leave> Leaves { get; set; } = new List<Leave>();
    }
}
