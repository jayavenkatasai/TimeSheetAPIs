namespace TIMESHEETAPI.DTO_Models
{
	public class UpdateTaskDto
	{
		public int TaskId { get; set; }
		public string Description { get; set; }

		public DateTime Task_date { get; set; }


		public int Hours { get; set; }

		public int EmployeeID { get; set; }

		public int ActivityID { get; set; }

		public int ProjectID { get; set; }
	}
}
