using System.ComponentModel.DataAnnotations;

namespace TIMESHEETAPI.DataModels
{
    public class TaskModel
    {
        [Required]
        [Key]
        public int TaskId { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime Task_date { get; set; }

        [Required]
        public int Hours { get; set; }

        public int EmployeeID { get; set; }

        public int ActivityID { get; set; }

        public int ProjectID { get; set; }

        public virtual ProjectModel ProjectModel { get; set; }  
        public virtual ActivityModel Activity { get; set; }

        public virtual Registeration registeration { get; set; }



    }
}
