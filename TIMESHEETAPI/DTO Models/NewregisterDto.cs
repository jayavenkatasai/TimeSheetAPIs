using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace TIMESHEETAPI.DTO_Models
{
    public class NewregisterDto
    {
        [Required]
        public string username { get; set; }
        [Required,EmailAddress]
        
       public string email { get;set; }
        [Required,MinLength(8)]
        public string Password { get; set; }
        [Required,Compare("Password")]
        public string ConfirmPassword { get; set;}

    }
}
