using POS.Presentation.Models;

namespace POS.Presentation.Services.Interfaces
{
    public interface IMenuService
    {
        Task<List<MenuModel>> GetDataAsync();
    }
}
