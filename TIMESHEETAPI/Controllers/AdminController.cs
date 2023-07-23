using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TIMESHEETAPI.Data;
using TIMESHEETAPI.DataModels;
using TIMESHEETAPI.DTO_Models;

namespace TIMESHEETAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly DataContext _context;

        public AdminController(DataContext context)
        {
            _context = context;
        }
        [HttpPost("Actvity")]
        public async Task<ActionResult< List<ActivityModel>>> Addactivity(ActivityDto dto)
        {
            var activity = new ActivityModel();
            activity.ActivityName  = dto.ActivityName;
            _context.ActivityModels.Add(activity);
           await _context.SaveChangesAsync();
            var activityList = await _context.ActivityModels.ToListAsync();
            return activityList;
        }
        [HttpGet("AllCtivities")]
        public async Task<ActionResult<List<GetActivityDto>>> GetActivity()
        {
            var activityList = await _context.ActivityModels.ToListAsync();

            // Map ActivityModel to GetActivityDto using LINQ Select
            var getActivityList = (from activity in activityList
                                   select new GetActivityDto
                                   {
                                       ActivityId = activity.ActivityId,
                                       ActivityName = activity.ActivityName
                                   }).ToList();

            // Return the list of GetActivityDto
            return getActivityList;
        }
        [HttpPost("Project")]
        public async Task<ActionResult<List<ActivityModel>>> AddaProject(ActivityDto dto)
        {
            var activity = new ActivityModel();
            activity.ActivityName = dto.ActivityName;
            _context.ActivityModels.Add(activity);
            await _context.SaveChangesAsync();
            var activityList = await _context.ActivityModels.ToListAsync();
            return activityList;
        }








    }
}
