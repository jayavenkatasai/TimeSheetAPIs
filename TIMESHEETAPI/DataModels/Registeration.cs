using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public int RoleID { get; set; }
		[ForeignKey("RoleID")]
		public virtual RolesModel Roles { get; set; }

	}
}
