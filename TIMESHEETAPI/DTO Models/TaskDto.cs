using System.ComponentModel.DataAnnotations;

namespace TIMESHEETAPI.DTO_Models
{
	public class TaskDto
	{
		

		public string Description { get; set; }
	
		public DateTime Task_date { get; set; }

	
		public int Hours { get; set; }

		public int EmployeeID { get; set; }

		public int ActivityID { get; set; }

		public int ProjectID { get; set; }

	}
}
