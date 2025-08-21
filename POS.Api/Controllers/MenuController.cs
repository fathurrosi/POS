using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using POS.Application.Interfaces;
using POS.Domain.Entities;
using POS.Domain.Entities.Custom;
using POS.Infrastructure.Repositories;
using POS.Shared;

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

        [HttpGet]
        public ActionResult<List<Menu>> Get()
        {
            try
            {
                var results = _menuRepository.GetAll();
                if (results == null) return NotFound();
                return this.Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving items.");
                return this.StatusCode(500, "Internal Server Error");
            }

        }

        [HttpGet("{username}")]
        public ActionResult<List<Menu>> Get(string username)
        {
            try
            {
                var results = _menuRepository.GetByUsername(username);
                if (results == null) return NotFound();
                return this.Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving items.");
                return this.StatusCode(500, "Internal Server Error");
            }

        }

        [HttpGet("Paging/{pageIndex}/{pageSize}")]
        public async Task<ActionResult<PagingResult<Menu>>> GetDataPaging(int pageIndex = 1, int pageSize = 10)
        {
            try
            {
                var results = await _menuRepository.GetDataPaging(pageIndex, pageSize);
                if (results == null) return NotFound();
                return this.Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving items.");
                return this.StatusCode(500, "Internal Server Error");
            }

        }


    }
}
