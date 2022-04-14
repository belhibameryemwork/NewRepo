using System.ComponentModel.DataAnnotations;

namespace Employe.Models
{
    public class EmployeSecurity
    {
        [Key]
        public int EmployeSecurityId { get; set; }
        public string EmployeSecurityName { get; set; }
        public string Position { get; set; }

        public string DateOfJoining { get; set; }
        public string PhotoFileName { get; set; }
    }
}
