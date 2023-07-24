using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TIMESHEETAPI.Data;
using TIMESHEETAPI.DataModels;
using TIMESHEETAPI.DTO_Models;

namespace TIMESHEETAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly DataContext _context;

		public UserController(DataContext context)
        {
			_context = context;
		}
		[HttpPost("TASK")]
		public async Task<ActionResult<TaskDto>> AddTask(TaskDto dto)
		{
			var taskk = new TaskModel();
			
			taskk.Description = dto.Description;
			taskk.ActivityID = dto.ActivityID;
			taskk.EmployeeID = dto.EmployeeID;
			taskk.ProjectID = dto.ProjectID;
			taskk.Task_date = dto.Task_date;
			taskk.Hours = dto.Hours;
			_context.TaskModels.Add(taskk);
			await _context.SaveChangesAsync();


			return Ok(taskk);
		}
    }
}
