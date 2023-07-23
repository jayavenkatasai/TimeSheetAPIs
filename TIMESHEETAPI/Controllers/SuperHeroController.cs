using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using TIMESHEETAPI.Data;

namespace TIMESHEETAPI.Controllers
{
    
   [Route("api/[controller]")]
    [ApiController]
   
    public class SuperHeroController : ControllerBase
    {
        private static List<SuperHero> Heros = new List<SuperHero>
        {
            new SuperHero
            {
                id = 1,
                Name = "Test",
                LastName    = "Test",
                City = "Test",
                Hero = "Test",
            }
        };
        private DataContext _context;

        public SuperHeroController(DataContext context)
        {
            _context = context;
            
        }
        [HttpGet("superHero"),Authorize]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            return Ok(Heros);
        }
        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero Super)
        {
            Heros.Add(Super);
            return Ok(Heros);   
        }
        [HttpGet("id")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            var hero = Heros.Find(h => h.id == id);
            if(hero == null)
            {
                return BadRequest("Hero Not Found");
            }
            return Ok(hero);
        }
        [HttpPut]
        public async Task<ActionResult<SuperHero>> update(SuperHero heros)
        {
            var hero = Heros.Find(h => h.id == heros.id);
            if (hero == null)
            {
                return BadRequest("Hero Not Found");
            }
            hero.Name = heros.Name;
            hero.LastName = heros.LastName;
            hero.City = heros.City;
            hero.Hero = heros.Hero;
             return Ok(hero);

        }
        [HttpDelete ]
        public async Task <ActionResult> Delete(int id)
        {
            var hero = Heros.Find(h => h.id == id);
            if (hero == null)
            {
                return BadRequest("Hero Not Found");
            }
            Heros.Remove(hero);
            return Ok("Deleted Sucessfully");

        }
    }
}
