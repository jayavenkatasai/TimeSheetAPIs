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
using static TIMESHEETAPI.Services.emailService;

namespace TIMESHEETAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        
       // public static Registeration registeration = new Registeration();
        private DataContext _context;
        private readonly IConfiguration _configuration;
		private readonly IEmailService _emailService;

		public AuthController(DataContext context, IConfiguration configuration, IEmailService emailService)
        {
            _context = context;
            _configuration = configuration;
		    _emailService = emailService;
		}

        [HttpPost("register")]
        public async Task<ActionResult> Register(Register_Dto request)
        {
			var emailv = await _context.registerations.AnyAsync(r=> r.Email == request.Email);
			var emailvv = await(from i in _context.registerations where i.Email == request.Email select i).AnyAsync();	
			if (emailv)
			{
				return BadRequest("User Already Exsist");
			}
			var verificationToken = GenerateRandomString();
			//	var otptoken = GenerateOtp();
			var registeration = new Registeration
			{
				Email = request.Email,
				UsserName = request.UsserName,

				VerificationToken = verificationToken,
				IsVerified = false,// Set to false initially,
				IsOtpVerified = false,
			};
			if (request.Email == "srikanth@smbxl.com" || request.Email == "pthudia@smbxl.com" || request.Email == "krishna@smbxl.com")
			{
				registeration.RoleID = 3;
			}
			else if(request.Email == "hr@smbxl.com"|| request.Email=="umak@smbxl.com"|| request.Email == "umak@smbxl.com" || request.Email == "sindhu@smbxl.com")
			{
				registeration.RoleID = 2;
			}
			else 
			{ 
			   registeration.RoleID = 1;
			}

			// OtpVerificationToken = otptoken


			_context.registerations.Add(registeration);
			await _context.SaveChangesAsync();
			CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passworSalt);
            var Userauthh = new UserOauth
            {
                
                PasswordHash = passwordHash,
                PasswordSalt = passworSalt


            };
            
            Userauthh.EmployeeID = registeration.EmployeeID;
            _context.UserOauths.Add(Userauthh);
			await _context.SaveChangesAsync();
			await _emailService.SendVerificationEmailAsync(request.Email, verificationToken);

			return Ok("User Created Confirm with Otp sent via your Mail");
        }
		[HttpPost("verify")]
		public async Task<ActionResult> Verify(string email, string token)
		{
			// Find the user with the provided email and verification token
			var user = await _context.registerations.FirstOrDefaultAsync(r => r.Email == email && r.VerificationToken == token);

			if (user == null)
			{
				return BadRequest("Invalid verification token or email");
			}
      
			// Mark the user as verified in the database
			user.IsVerified = true;
			user.VerificationToken = null;
			_context.Update(user);
			await _context.SaveChangesAsync();

			// You can return a success message or redirect the user to a success page after verification.
			return Ok("Email verification successful.");
		}

		[HttpPost ("login")]
        public async Task<ActionResult<string>> login(loginDto login)
        {
            var registrations = await _context.registerations.FirstOrDefaultAsync(r=> r.Email == login.Email);
            var Ouathusers = await _context.UserOauths.FirstOrDefaultAsync(o => o.Id == registrations.EmployeeID);
			
			if (registrations == null || registrations.Email!= login.Email)
            {
                return BadRequest("No user Founf");
            }
            if (!VerifyPassword(login.Password, Ouathusers.PasswordHash, Ouathusers.PasswordSalt))
            {
                return BadRequest("Wrong Password");
            }

            var otpToken = GenerateOtp();
			registrations.OtpVerificationToken = otpToken;
            await _context.SaveChangesAsync();
            string token = CreateToken(registrations);
            var loginresponse = new loginResponse
            {
                id = registrations.EmployeeID,
                code = token,
				RoleID = registrations.RoleID

            };
			await _emailService.SendOtpVerificationEmailAsync(registrations.Email, registrations.OtpVerificationToken);

			return Ok(loginresponse);
        }
		[HttpPost("verifyLogin")]
		public async Task<ActionResult> VerifyLogin(string email, string token)
		{
			// Find the user with the provided email and verification token
			var user = await _context.registerations.FirstOrDefaultAsync(r => r.Email == email && r.OtpVerificationToken == token);

			if (user == null)
			{
				return BadRequest("Invalid verification token or email");
			}

			// Mark the user as verified in the database
			user.IsOtpVerified = true;
			user.OtpVerificationToken = null;
			_context.Update(user);
			await _context.SaveChangesAsync();

			// You can return a success message or redirect the user to a success page after verification.
			return Ok("Email verification successful.");
		}
		[HttpPost("ForgotPassword")]
		public async Task<ActionResult> ForgotPassword(string email)
		{
			var userdt = await _context.registerations.FirstOrDefaultAsync(r=>r.Email ==  email);
			if (userdt == null)
			{
				return BadRequest("no user found");
			}
			string tok = GenerateOtp();
			userdt.PasswordToken = tok;
			await _context.SaveChangesAsync();
            await _emailService.SendOtpVerificationEmailAsync(userdt.Email, userdt.PasswordToken);

			return Ok("Verification Otp is sent to mail");

        }
		[HttpPost("ResetPassword")]
		public async Task<ActionResult> ResetPassword(ForgotPasswordDto forgot)
		{
			var userdt = await _context.registerations.FirstOrDefaultAsync(r => r.Email == forgot.Email);

            var oauthdt = (from u in _context.UserOauths
                           where u.EmployeeID == userdt.EmployeeID
                           select u).FirstOrDefault();
            if (userdt == null) 
			{
				return BadRequest("Not Found The User");
			}
			if(forgot.ConfirmPassword == null)
			{
			    return BadRequest("Please Enter the password");
			}
			if(forgot.Token != userdt.PasswordToken)
			{
				return BadRequest("Invalid Token");
			}
			CreatePasswordHash(forgot.ConfirmPassword, out byte[] passwordHash, out byte[] passworSalt);
			if(oauthdt == null)
			{
				return BadRequest("No user found");
			}

			oauthdt.PasswordHash = passwordHash;
			oauthdt.PasswordSalt = passworSalt;
			userdt.PasswordToken = null;
			 
            await _context.SaveChangesAsync();

            return Ok("Password reset successfully");

        }
		[HttpPost("UserRole")]
		public async Task<ActionResult> Addrole(RoleDto dto)
		{
			var roles = new RolesModel();
			roles.RoleName = dto.RoleName;
			_context.RoleModels.Add(roles);
			await _context.SaveChangesAsync();	
			return Ok(roles);

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
		public static string GenerateRandomString()
		{
			const string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
			const int length = 6;

			Random random = new Random();
			char[] chars = new char[length];

			for (int i = 0; i < length; i++)
			{
				chars[i] = allowedChars[random.Next(0, allowedChars.Length)];
			}

			return new string(chars);
		}
		public static string GenerateOtp()
		{
			const string allowedChars = "0123456789";
			const int length = 6;

			Random random = new Random();
			char[] chars = new char[length];

			for (int i = 0; i < length; i++)
			{
				chars[i] = allowedChars[random.Next(0, allowedChars.Length)];
			}

			return new string(chars);
		}

	}
}
 