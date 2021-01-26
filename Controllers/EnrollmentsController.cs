using System;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using apbd_int_cw5.Requests;
using apbd_int_cw5.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace apbd_int_cw5.Controllers
{
    [ApiController]
    [Route("api/enrollments")]
    public class EnrollmentsController : ControllerBase
    {
        private IEnrollmentDbServices _enrollmentDbServices;
        public IConfiguration Configuration { get; set; }

        private readonly IRefreshTokenServices _rTokenServices = new RefreshTokenServices();
        public EnrollmentsController(IEnrollmentDbServices enrollmentDbServices, IConfiguration configuration)
        {
            _enrollmentDbServices = enrollmentDbServices;
            Configuration = configuration;
        }

        [HttpPost]
        [Authorize(Roles = "employee")]
        public IActionResult EnrollStudent(EnrollStudentRequest request)
        {
            try
            {
                var response = _enrollmentDbServices.EnrollStudent(request);
                return CreatedAtAction("EnrollStudent", response);
            }
            catch (ArgumentException)
            {
                return NotFound("Studies with given name not found");
            }
            catch (InvalidOperationException)
            {
                return BadRequest("Student with given index number already exists");
            }
        }

        
        [Route("promotions")]
        [HttpPost]
        [Authorize(Roles = "employee")]
        [ActionName("PromoteStudents")]
        public IActionResult PromoteStudents(PromoteStudentRequest request)
        {
            try
            {
                var response = _enrollmentDbServices.PromoteStudents(request);
                return CreatedAtAction("PromoteStudents", response);
            }
            catch (ArgumentNullException)
            {
                return BadRequest("Invalid request");
            }
            catch (SqlException)
            {
                return NotFound("Studies with given name not found");
            }
            catch (ArgumentException)
            {
                return BadRequest("No values for response found");
            }
        }
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public IActionResult Login(LoginRequest request)
        {
            var student = _enrollmentDbServices.GetStudentPassword(request.Login);
            if (student.Password.Equals(request.Password))
            {
                Console.WriteLine("Hasło poprawne");
            }
            else
            {
                return BadRequest("Niepoprawne hasło");
            }

            var claims = new[]
 {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Role, "student")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken

            (
                issuer: "s17174",
                audience: "Students",
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: creds
            );

            var rToken = Guid.NewGuid();
            _rTokenServices.SetToken(rToken);

            return Ok(new
            {
                accessToken = new JwtSecurityTokenHandler().WriteToken(token),
                refreshToken = rToken
            });
        }
        [HttpPost()]
        [Route("refresh")]
        [AllowAnonymous]
        public IActionResult RefreshToken(RefreshRequest reftoken)
        {
            if (!_rTokenServices.CheckToken(reftoken.RefreshToken))
            {
                return BadRequest();
            }

            var claims = new[]
{
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Role, "employee")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken

           (
               issuer: "s17174",
               audience: "Students",
               claims: claims,
               expires: DateTime.Now.AddMinutes(10),
               signingCredentials: creds
           );

            var rToken = Guid.NewGuid();
            _rTokenServices.SetToken(rToken);
            return Ok(new
            {
                accessToken = new JwtSecurityTokenHandler().WriteToken(token),
                refreshToken = rToken
            });
        }
    }

}