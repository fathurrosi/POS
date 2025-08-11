using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using POS.Application.Interfaces;
using POS.Domain.Entities;
using POS.Infrastructure.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace POS.Api.Controllers
{
    [EnableCors("AllowSpecificMethods")]
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly ILogger<MenuController> _logger;
        private readonly IMenuRepository _menuRepository;

        public MenuController(ILogger<MenuController> logger, IMenuRepository menuRepository)
        {
            _logger = logger;
            _menuRepository = menuRepository;
        }
        // GET: api/<MenuController>
        // GET: api/<MenuController>
        [HttpGet]
        public IEnumerable<Menu> Get()
        {
            IEnumerable<Menu> items = new List<Menu>();
            try
            {
                items = _menuRepository.GetAll();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving items.");
                //throw; // Re-throw the exception after logging it
            }

            return items;
        }

        // GET api/<MenuController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<MenuController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<MenuController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MenuController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
