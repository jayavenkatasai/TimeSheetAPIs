using System.ComponentModel.DataAnnotations;

namespace TIMESHEETAPI.DataModels
{
	public class RolesModel
	{
		[Key]
		public int RoleID { get; set; }
		public string RoleName { get; set; }
	}
}
