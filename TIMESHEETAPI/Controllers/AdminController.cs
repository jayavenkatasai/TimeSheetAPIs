using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Formats.Asn1;
using System.Security.Claims;
using System.Threading.Tasks;
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
        [HttpPost("Project"), Authorize]
        public async Task<ActionResult<List<ProjectModel>>> AddaProject(ProjectDTO dto)
        {
            var reg = new Registeration();
			var userEmail = User?.FindFirstValue(ClaimTypes.Email);
			var oath = await _context.registerations.FirstOrDefaultAsync(i => i.Email == userEmail);
			var oaths = await (from i in _context.registerations where i.Email == userEmail select i.EmployeeID).FirstOrDefaultAsync();
			var id = oath.EmployeeID;
			//var oath = await _context.registerations.FirstOrDefaultAsync(from i in reg where i.Email = userEmail select i);
			var Project = new ProjectModel();
            Project.ProjectName = dto.ProjectName;
            _context.ProjectModels.Add(Project);
            await _context.SaveChangesAsync();
            var activityList = await _context.ProjectModels.ToListAsync();
            return activityList;
        }

        [HttpGet("GetAllProjects")]
        public async Task<ActionResult<List<GetProjectDto>>> GetProj()
        {
            var projlist = await _context.ProjectModels.ToListAsync();
            var proj = (from projl in projlist
                        select new GetProjectDto
                        {
                            ProjectId = projl.ProjectId,
                            ProjectName = projl.ProjectName
                        }
                        ).ToList();
            return Ok(proj);
        }
        [HttpPut("ProjectDto")]
        public async Task<ActionResult<ProjectModel>> updateProj(ProjectModelDto model)
        {
            var proj = await _context.ProjectModels.FindAsync(model.ProjectId);


		    if(proj == null)
			{
				return NotFound("Task not found");
			}

			if(model.ProjectId != proj.ProjectId)
			{
				return BadRequest("TaskId cannot be changed");
			}
            proj.ProjectName = model.ProjectName;
             _context.ProjectModels.Update(proj);
            await _context.SaveChangesAsync();
            return Ok(proj);

         

		}

        [HttpDelete("Projectdelete")]
        public async Task<ActionResult> DeleteProj(int id)
        {
            var delp = await _context.ProjectModels.FindAsync(id);
            if(delp == null)
            {
                return NotFound("ThePojectNotFound");
            }
            if(delp.ProjectId != id)
            {
                return BadRequest("you cannot delete project");
            }
            _context.ProjectModels.Remove(delp);
            await _context.SaveChangesAsync();
            return Ok("DeletedSucessfully");
        }

        [HttpPut("ActivityUpdate")]
		public async Task<ActionResult<GetActivityDto>> updateAct(GetActivityDto model)
		{
			var proj = await _context.ActivityModels.FindAsync(model.ActivityId);


			if (proj == null)
			{
				return NotFound("Task not found");
			}

			if (model.ActivityId != proj.ActivityId)
			{
				return BadRequest("TaskId cannot be changed");
			}
			proj.ActivityName = model.ActivityName;
			_context.ActivityModels.Update(proj);
			await _context.SaveChangesAsync();
			return Ok(proj);

		}
        [HttpDelete("ActivityDelete")]
		public async Task<ActionResult> DeleteAct(int id)
		{
			var delp = await _context.ActivityModels.FindAsync(id);
			if (delp == null)
			{
				return NotFound("ThePojectNotFound");
			}
			if (delp.ActivityId != id)
			{
				return BadRequest("you cannot delete project");
			}
			_context.ActivityModels.Remove(delp);
			await _context.SaveChangesAsync();
			return Ok("DeletedSucessfully");
		}

        [HttpGet("AllTasks")]
        public async Task<ActionResult<List<TaskModelDto>>> AllTAsks()
        {
            var tasklist = await (from i in _context.TaskModels
                                  join P in _context.ProjectModels on i.ProjectID equals P.ProjectId
                                  join A in _context.ActivityModels on i.ActivityID equals A.ActivityId
                                  join R in _context.registerations on i.EmployeeID equals R.EmployeeID
                                  select new AllTasksDto
                                  {

                                      ActivityName = i.Activity.ActivityName,
                                      projectName = i.ProjectModel.ProjectName,
                                      Description = i.Description,
                                      TotalHours = i.Hours,
                                      Created = i.Task_date,
                                      UserEmail = i.registeration.Email,
                                      UserName = i.registeration.UsserName
                                  }
                                  ).ToListAsync();
            return Ok(tasklist);
                                 
        }
		[HttpGet("AllTasksByIDDate")]
		public async Task<ActionResult<List<TaskModelDto>>> AllTAsksByIdDate(int id,DateTime date)
		{
			
			var tasklist = await (from i in _context.TaskModels
								  where i.EmployeeID == id && i.Task_date.Date == date.Date
								  join P in _context.ProjectModels on i.ProjectID equals P.ProjectId
								  join A in _context.ActivityModels on i.ActivityID equals A.ActivityId
								  join R in _context.registerations on i.EmployeeID equals R.EmployeeID
								  select new AllTasksDto
								  {

									  ActivityName = i.Activity.ActivityName,
									  projectName = i.ProjectModel.ProjectName,
									  Description = i.Description,
									  TotalHours = i.Hours,
									  Created = i.Task_date,
									  UserEmail = i.registeration.Email,
									  UserName = i.registeration.UsserName
								  }
								  ).ToListAsync();
			return Ok(tasklist);

		}
		[HttpGet("AllTasksByDateRange")]
		public async Task<ActionResult<List<TaskModelDto>>> AllTAsksByDateRange(int id, DateTime startdate,DateTime enddate)
		{

			var tasklist = await (from i in _context.TaskModels
								  where i.EmployeeID == id && i.Task_date.Date >=startdate.Date && i.Task_date.Date <= enddate.Date
								  join P in _context.ProjectModels on i.ProjectID equals P.ProjectId
								  join A in _context.ActivityModels on i.ActivityID equals A.ActivityId
								  join R in _context.registerations on i.EmployeeID equals R.EmployeeID
								  select new AllTasksDto
								  {

									  ActivityName = i.Activity.ActivityName,
									  projectName = i.ProjectModel.ProjectName,
									  Description = i.Description,
									  TotalHours = i.Hours,
									  Created = i.Task_date,
									  UserEmail = i.registeration.Email,
									  UserName = i.registeration.UsserName
								  }
								  ).ToListAsync();
			return Ok(tasklist);

		}
  
	}
}
