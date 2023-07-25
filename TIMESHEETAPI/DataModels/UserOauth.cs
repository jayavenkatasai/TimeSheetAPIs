using System.ComponentModel.DataAnnotations.Schema;

namespace TIMESHEETAPI.DataModels
{
	public class UserOauth
	{
		public int Id { get; set; }
		public byte[] PasswordHash { get; set; }

		public byte[] PasswordSalt { get; set; }
		
		public int EmployeeID { get; set; }

		[ForeignKey("EmployeeID")]
		public  virtual Registeration Registerations { get; set; }	
	}
}
