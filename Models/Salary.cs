namespace HRMSystem.Models
{
    public class Salary
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime EffectiveDate { get; set; }

        public int EmployeeId { get; set; } // Khóa ngoại
        public virtual Employee Employee { get; set; } = null!;
    }
}
