using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TIMESHEETAPI.DataModels
{
    public class ProjectModel
    {
        [Key]
        public int ProjectId { get; set; }

        [Required]
        public string ProjectName { get; set; }

        public virtual ICollection<TaskModel> Tasks { get; set; }
    }
}
