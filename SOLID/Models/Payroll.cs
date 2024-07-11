using System.ComponentModel.DataAnnotations;

namespace SOLID.Models
{
    public class Payroll
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime? Checkin { get; set; }
        public DateTime? Checkout { get; set; }
        
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
