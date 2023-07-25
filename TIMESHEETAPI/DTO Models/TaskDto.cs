using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TIMESHEETAPI.DTO_Models
{
	public class TaskUpdateDto
	{
		

		public string Description { get; set; }
		[Column(TypeName = "date")]

		public DateTime Task_date { get; set; }

	
		public int Hours { get; set; }

	/*	public int EmployeeID { get; set; }*/

		public int ActivityID { get; set; }

		public int ProjectID { get; set; }

	}
}
