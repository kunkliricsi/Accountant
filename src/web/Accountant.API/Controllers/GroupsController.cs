﻿using Accountant.API.DTOs;
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
    public class GroupsController : ControllerBase
    {
        private readonly IGroupService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<GroupsController> _logger;

        public GroupsController(IGroupService service, IMapper mapper, ILogger<GroupsController> logger)
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Group>>> GetAllGroupsAsync([FromQuery(Name = "userId")] int[] userIds)
        {
            _logger.LogInformation($"Getting all reports of user(s) with ID(s): [{string.Join(", ", userIds)}]");

            return _mapper.Map<List<Group>>(await _service.GetGroupsAsync(userIds));
        }

        [HttpGet("{id}", Name = "GetGroup")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Group>> GetGroupAsync(int id)
        {
            _logger.LogInformation($"Getting group with ID: [{id}]...");

            return _mapper.Map<Group>(await _service.GetGroupAsync(id));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Group>> PostAsync([FromBody] Group group)
        {
            _logger.LogInformation("Creating group...");

            var created = await _service.CreateGroupAsync(
                _mapper.Map<DAL.Entities.Group>(group));

            _logger.LogInformation($"Created group [{created.Id}].");

            return CreatedAtAction(
                "GetGroup",
                new { id = created.Id },
                _mapper.Map<Group>(created));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutAsync([FromBody] Group group)
        {
            _logger.LogInformation($"Updating group [{group.Id}]...");

            await _service.UpdateGroupAsync(
                _mapper.Map<DAL.Entities.Group>(group));

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            _logger.LogInformation($"Deleting group [{id}]...");

            await _service.DeleteGroupAsync(id);

            return NoContent();
        }
    }
}
