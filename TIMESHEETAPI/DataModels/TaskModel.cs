using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TIMESHEETAPI.DataModels
{
    public class TaskModelDto
    {
        [Required]
        [Key]
        public int TaskId { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
		[Column(TypeName = "date")]
		public DateTime Task_date { get; set; }

        [Required]
        public int Hours { get; set; }

        public int EmployeeID { get; set; }

        public int ActivityID { get; set; }
        [Required]
        public int ProjectID { get; set; }

		[ForeignKey("ProjectID")]
		public virtual ProjectModel ProjectModel { get; set; }  
        public virtual ActivityModel Activity { get; set; }
		[ForeignKey("EmployeeID")]
		public virtual Registeration registeration { get; set; }



    }
}
