using System.ComponentModel.DataAnnotations;

namespace Employe.Models
{
    public class EmployeSociete
    {
        [Key]
        public int EmployeSocietyId { get; set; }
        public string EmployeSocietyName { get; set; }
        public string Company { get; set; }
       
        public string DateOfJoining { get; set; }
        public string PhotoFileName { get; set; }
       
    }
}
