using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TIMESHEETAPI.DataModels
{
    public class Registeration
    {
        [Key]
        [Required]
        public int EmployeeID { get;set; }

        [EmailAddress]
        public string Email { get;set; } = string.Empty;
        public string UsserName { get; set; } = string.Empty;   

        public byte[] PasswordHash { get; set; }    

        public byte[] PasswordSalt { get; set; }

    }
}
