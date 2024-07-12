using SOLID.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace SOLID.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
    }
}
