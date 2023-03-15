using EmployeeM.Models;
using EmployeeM.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JWTTokenController : ControllerBase
    {
        public IConfiguration _configuration;
        public readonly AuthService _service;

        public JWTTokenController(IConfiguration configuration,AuthService service)
        {
            _configuration = configuration;
            _service = service;
        }
        [HttpPost]

        public async Task<IActionResult> Post(User user)
        {
            if(user!=null && user.Username!=null&& user.Password!=null) 
            {
                var userData = await GetUser(user.Username, user.Password);
                var jwt = _configuration.GetSection("Jwt").Get<Jwt>();
                if(user!=null)
                {
                    var claims = new []
            {
                new Claim(JwtRegisteredClaimNames.Sub,jwt.Subject),
                new Claim("Username",user.Username),
                new Claim("Password",user.Password),
                new Claim(JwtRegisteredClaimNames.Iat,DateTime.UtcNow.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),

            };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.key));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken
                (
                    jwt.Issuer ,
                    jwt.Audience,
                    claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: signIn

                );
                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));

                }
                else
                {
                    return BadRequest("Invalid Credentials");
                }

            }
            else
            {
                return BadRequest("Invalid Credentials");
            }
        }

        
        [HttpGet]
        
        public async Task<User> GetUser(string username,string password)
        {
            return await _service.GetAsync(username, password);
        }

        


    }
}
