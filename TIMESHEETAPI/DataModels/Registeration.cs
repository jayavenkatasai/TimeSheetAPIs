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
		public bool IsVerified { get; set; }

		public string? VerificationToken { get; set; }
        public bool IsOtpVerified { get;set; }
        public string? OtpVerificationToken { get; set; }
 
        public string? PasswordToken { get; set; }



    }
}
