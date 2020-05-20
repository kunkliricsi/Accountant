using Accountant.API.DTOs;
using Accountant.BLL.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accountant.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserGroupController : ControllerBase
    {
        private readonly IUserGroupService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<UserGroupController> _logger;

        public UserGroupController(IUserGroupService service, IMapper mapper, ILogger<UserGroupController> logger)
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<User>> PostUserGroupAsync([FromBody] UserGroup userGroup)
        {
            _logger.LogInformation($"Creating user[{userGroup.UserId}] group[{userGroup.GroupId}] connection...");

            return _mapper.Map<User>(await _service.CreateUserGroupAsync(userGroup.UserId, userGroup.GroupId));
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteUserGroupAsync([FromBody] UserGroup userGroup)
        {
            _logger.LogInformation($"Deleting user[{userGroup.UserId}] group[{userGroup.GroupId}] connection...");

            await _service.DeleteUserGroupAsync(userGroup.UserId, userGroup.GroupId);

            return NoContent();
        }
    }
}
