using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TIMESHEETAPI.Data;
using TIMESHEETAPI.DataModels;

namespace TIMESHEETAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewAuth : ControllerBase
    {
        private readonly DataContext _context;

        public NewAuth(DataContext context)
        {
            _context = context;
        }
        /*[HttpPost]*/
       /* public async Task<IActionResult> Register(NewRegisterationEmployee newRegisteration)
        {
            if(_context.NewRegisterationEmployees.Any(u=> u.email == re))
        }*/

    }
}
