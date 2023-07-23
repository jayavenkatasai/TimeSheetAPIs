using System.ComponentModel.DataAnnotations;

namespace TIMESHEETAPI.DataModels
{
    public class NewRegisterationEmployee
    {
        public int Id { get; set; }
        [EmailAddress]
        public string? email { get; set; }
        public string UsserName { get; set; } = string.Empty;

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public string? VerificationToken { get; set; }

        public DateTime Verifiedat { get; set; }

        public string? PasswordresetToken{ get; set;}
        public string? passwordTokenExpires { get; set;}
    }
}
