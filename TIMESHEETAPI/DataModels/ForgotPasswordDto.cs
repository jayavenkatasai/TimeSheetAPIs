using System.ComponentModel.DataAnnotations;

namespace TIMESHEETAPI.DataModels
{
    public class ForgotPasswordDto
    {
        public string Email { get; set; }
        public string Token { get; set; }   
        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmPassword { get; set; }

    }
}
