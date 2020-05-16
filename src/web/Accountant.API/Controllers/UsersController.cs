using Accountant.API.DTOs;
using Accountant.API.DTOs.UserModels;
using Accountant.BLL.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Accountant.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<UsersController> _logger;
        private readonly IConfiguration _configuration;

        public UsersController(IUserService service, IMapper mapper, ILogger<UsersController> logger,
            IConfiguration configuration)
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginModel model)
        {
            _logger.LogInformation($"Authenticating user [{model.Name}] with password [*******]...");

            var user = await _service.AuthenticateUserAsync(model.Name, model.Password);

            _logger.LogInformation($"Authenticated user [{user.Name}].");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["AppSettings:Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.Id.ToString())
                    }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new
            {
                Id = user.Id,
                Name = user.Name,
                Token = tokenString
            });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] UpdateModel user)
        {
            _logger.LogInformation($"Registering user [{user.Name}]...");

            var created = await _service.CreateUserAsync(
                _mapper.Map<DAL.Entities.User>(user), user.Password);

            _logger.LogInformation($"Registered user [{created.Id}.");

            return NoContent();
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<User>>> GetAllAsync([FromQuery(Name = "groupId")] int[] groupIds)
        {
            _logger.LogInformation($"Getting all users in group(s): {string.Join(", ", groupIds)}.");

            return _mapper.Map<List<User>>(await _service.GetUsersAsync(groupIds));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetAsync(int id)
        {
            _logger.LogInformation($"Getting user [{id}]...");

            return _mapper.Map<User>(await _service.GetUserAsync(id));
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] UpdateModel user)
        {
            _logger.LogInformation($"Updating user [{user.Name}]...");

            await _service.UpdateUserAsync(
                _mapper.Map<DAL.Entities.User>(user),
                user.Password);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            _logger.LogInformation($"Deleting user [{id}]...");

            await _service.DeleteUserAsync(id);

            return NoContent();
        }
    }
}
