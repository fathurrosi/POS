using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using POS.Application.Interfaces;
using POS.Domain.Entities;
using POS.Infrastructure.Repositories;
using System.Collections.Generic;

namespace POS.Api.Controllers
{
    [EnableCors("AllowSpecificMethods")]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly ILogger<RoleController> _logger;
        private readonly IRoleRepository _roleRepository;

        public RoleController(ILogger<RoleController> logger, IRoleRepository roleRepository)
        {
            _logger = logger;
            _roleRepository = roleRepository;
        }
        // GET: api/<RoleController>
        [HttpGet]
        public IEnumerable<Role> Get()
        {
            IEnumerable<Role> roles = new List<Role>();
            try
            {
                roles = _roleRepository.GetAll();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving roles.");
                //throw; // Re-throw the exception after logging it
            }
           
            return roles;
        }

        // GET api/<RoleController>/5
        [HttpGet("{username}")]
        public Role Get(string username)
        {
           Role role = new Role();
            try
            {
                role = _roleRepository.GetByUsername(username);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving roles.");
                //throw; // Re-throw the exception after logging it
            }

            return role;
        }

        // POST api/<RoleController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<RoleController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RoleController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
