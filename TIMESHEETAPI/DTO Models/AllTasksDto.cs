using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace TIMESHEETAPI.DTO_Models
{
	public class AllTasksDto
	{
		public string UserName { get; set; } = string.Empty;
		public string UserEmail { get; set; } = string.Empty;

		public string projectName { get; set; } = string.Empty;
		public string ActivityName { get; set; } = string.Empty;
		public int TotalHours { get; set; } = 0;
		public string Description { get;set; } = string.Empty;
		[Column(TypeName = "date")]
		public DateTime Created { get; set; }
	}
}
