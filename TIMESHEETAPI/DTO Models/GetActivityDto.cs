using System.ComponentModel.DataAnnotations;

namespace TIMESHEETAPI.DTO_Models
{
    public class GetActivityDto
    {
        public int ActivityId { get; set; }
  
        public string ActivityName { get; set; }
    }
}
