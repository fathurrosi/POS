using POS.Domain.Entities;
using POS.Domain.Entities.Custom;
using POS.Presentation.Models;
using POS.Shared;

namespace POS.Presentation.Services.Interfaces
{
    public interface IMenuService
    {
        Task<List<Menu>> GetDataAsync();
        Task<List<Menu>> GetDataByUsernameAsync(string username);
        Task<PagingResult<Menu>> GetPagingAsync(int pageIndex, int pageSize);
    }
}
