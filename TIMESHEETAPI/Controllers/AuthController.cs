using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TIMESHEETAPI.Data;
using TIMESHEETAPI.DataModels;
using TIMESHEETAPI.DTO_Models;

namespace TIMESHEETAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        
       // public static Registeration registeration = new Registeration();
        private DataContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult<Registeration>> Register(Register_Dto request)
        {
            var registeration = new Registeration
            {
                Email = request.Email,
                UsserName = request.UsserName
            };
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passworSalt);
            
            registeration.PasswordHash = passwordHash;

            registeration.PasswordSalt= passworSalt;
          _context.registerations.Add(registeration);
          await _context.SaveChangesAsync();
            return Ok( registeration);
        }
        [HttpPost ("login")]
        public async Task<ActionResult<string>> login(loginDto login)
        {
            var registrations = await _context.registerations.FirstOrDefaultAsync(r=> r.Email == login.Email);

            if(registrations == null || registrations.Email!= login.Email)
            {
                return BadRequest("No user Founf");
            }
            if (!VerifyPassword(login.Password, registrations.PasswordHash, registrations.PasswordSalt))
            {
                return BadRequest("Wrong Password");
            }
           
            string token = CreateToken(registrations);
            var loginresponse = new loginResponse
            {
                id = registrations.EmployeeID,
                code = token

            };
            return Ok(loginresponse);
        }

        private string CreateToken(Registeration registeration)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, registeration.Email),
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials:   cred
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
        private void CreatePasswordHash(string Password,out byte[] passwordHash,out byte[] passwordSalt)
        {
            using(var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));
            }
        }
        private bool VerifyPassword(string Password, byte[] passswordHash, byte[] passwordsalt)

        {
            using(var hmac = new HMACSHA512(passwordsalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));
                return computedHash.SequenceEqual(passswordHash);
            }
        }
    }
}
