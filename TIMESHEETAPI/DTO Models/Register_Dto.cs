using System.ComponentModel.DataAnnotations;

namespace TIMESHEETAPI.DTO_Models
{
    public class Register_Dto
    {
        //public int EmployeeID { get; set; }

        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public string UsserName { get; set; } = string.Empty;

        public string Password { get; set; }
    }
}
