using System.ComponentModel.DataAnnotations;

namespace SOLID.Models
{
    public class Employee
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double HourlyRate { get; set; }
        public string Category { get; set; }
    }
}
