using System.ComponentModel.DataAnnotations;

namespace TIMESHEETAPI.DataModels
{
    public class ActivityModel
    {
        [Key]
        public int ActivityId { get; set; }
        [Required]
        public string ActivityName { get; set; }

        public virtual ICollection<TaskModel> Tasks { get; set; }
    }
}
