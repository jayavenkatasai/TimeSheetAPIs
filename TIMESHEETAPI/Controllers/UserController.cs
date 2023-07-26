using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
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
		[HttpPost("TASK"),Authorize]
		public async Task<ActionResult<TaskUpdateDto>> AddTask(TaskUpdateDto dto)
		{
			var taskk = new TaskModelDto();
			var userEmail = User?.FindFirstValue(ClaimTypes.Email);
			//var oath = await _context.registerations.FirstOrDefaultAsync(i => i.Email == userEmail);
			var oaths = await (from i in _context.registerations where i.Email == userEmail select i.EmployeeID).FirstOrDefaultAsync();
			taskk.EmployeeID = oaths;
			taskk.Description = dto.Description;
			taskk.ActivityID = dto.ActivityID;
			//taskk.EmployeeID = dto.EmployeeID;
			taskk.ProjectID = dto.ProjectID;
			taskk.Task_date = dto.Task_date;
			taskk.Hours = dto.Hours;
			_context.TaskModels.Add(taskk);
			await _context.SaveChangesAsync();


			return Ok(taskk);
		}
	/*	[HttpPut("Update Task")]
		public async Task<ActionResult<TaskModelDto>> updatetask(UpdateTaskDto dto)
		{
			 var taskid = TaskModels.
		   
			if (dto.TaskId != taskk.TaskId)
			{
				return BadRequest("IT IS NOT POSSIBLE");
			}
			taskk.Description = dto.Description;
			taskk.ActivityID = dto.ActivityID;
			taskk.EmployeeID = dto.EmployeeID;
			taskk.ProjectID = dto.ProjectID;
			taskk.Task_date = dto.Task_date;
			taskk.Hours = dto.Hours;
			_context.TaskModels.Update(taskk);
		
			await _context.SaveChangesAsync();
			return Ok(taskk);
		}*/


		[HttpPut("UpdateTask"),Authorize]
		public async Task<ActionResult<TaskModelDto>> UpdateTask(UpdateTaskDto dto)
		{
			var taskk = await _context.TaskModels.FindAsync(dto.TaskId);

			if (taskk == null)
			{
				return NotFound("Task not found");
			}

			if (dto.TaskId != taskk.TaskId)
			{
				return BadRequest("TaskId cannot be changed");
			}

			taskk.Description = dto.Description;
			taskk.ActivityID = dto.ActivityID;
			taskk.EmployeeID = dto.EmployeeID;
			taskk.ProjectID = dto.ProjectID;
			taskk.Task_date = dto.Task_date;
			taskk.Hours = dto.Hours;

			_context.TaskModels.Update(taskk);
			await _context.SaveChangesAsync();

			return Ok(taskk);
		}
		[HttpDelete("Delete The Task"),Authorize]
		public async Task<ActionResult<TaskModelDto>> deleteid(int id)
		{
			var taskk = await _context.TaskModels.FindAsync(id);
			if (taskk == null)
			{
				return BadRequest("Please Keep Id ");
			}
			if (taskk.TaskId!= id)
			{
				return BadRequest("Task Not found");
			}
			_context.TaskModels.Remove(taskk);
			await _context.SaveChangesAsync();
			return Ok("Deleted Task Succesfully");
		}
		[HttpGet("AllTasksByID"),Authorize]
		public async Task<ActionResult<List<TaskModelDto>>> AllTAsksById()
		{
			var userEmail = User?.FindFirstValue(ClaimTypes.Email);
			//var oath = await _context.registerations.FirstOrDefaultAsync(i => i.Email == userEmail);
			var oaths = await (from i in _context.registerations where i.Email == userEmail select i.EmployeeID).FirstOrDefaultAsync();
			var tasklist = await (from i in _context.TaskModels
								  where i.EmployeeID == oaths
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
		[HttpGet("TasksByDate"),Authorize]
		public async Task<ActionResult<List<TaskModelDto>>> TaskByDate(DateTime date)
		{
			var oemail = User?.FindFirstValue(ClaimTypes.Email);
			var oid = await(from i in _context.registerations where i.Email == oemail select i.EmployeeID).FirstOrDefaultAsync();
			var tasklist =  await (from i in _context.TaskModels
                                   where i.EmployeeID == oid && i.Task_date == date
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
                                   }).ToListAsync();
			return Ok(tasklist);

		}
        [HttpGet("TasksByDateRange"),Authorize]
        public async Task<ActionResult<List<TaskModelDto>>> TaskByDateRange(DateTime startdate,DateTime EndDate)
        {
            var oemail = User?.FindFirstValue(ClaimTypes.Email);
            var oid = await (from i in _context.registerations where i.Email == oemail select i.EmployeeID).FirstOrDefaultAsync();
            var tasklist = await (from i in _context.TaskModels
                                  where i.EmployeeID == oid && i.Task_date >= startdate && i.Task_date <= EndDate
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
                                  }).ToListAsync();
            return Ok(tasklist);

        }
		[HttpGet("TaskByCurrentweek"), Authorize]
        public async Task<ActionResult<List<TaskModelDto>>> TaskBycurrentweek()
        {
            var oemail = User?.FindFirstValue(ClaimTypes.Email);
            var oid = await (from i in _context.registerations where i.Email == oemail select i.EmployeeID).FirstOrDefaultAsync();

            // Calculate the start date and end date for the current week
            DateTime currentDate = DateTime.Today;
            DateTime startOfWeek = currentDate.AddDays(-(int)currentDate.DayOfWeek);
            DateTime endOfWeek = startOfWeek.AddDays(6);

            var tasklist = await (from i in _context.TaskModels
                                  where i.EmployeeID == oid && i.Task_date >= startOfWeek && i.Task_date <= endOfWeek
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
                                  }).ToListAsync();

            return Ok(tasklist);
        }

    }
}
