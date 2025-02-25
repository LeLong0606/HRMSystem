namespace HRMSystem.Models
{
    public class EmploymentContract
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Salary { get; set; }

        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; } = null!;
    }

}
