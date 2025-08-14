using POS.Presentation.Models;

namespace POS.Presentation.Services.Interfaces
{
    public interface IRoleService
    {
        Task<RoleModel> GetByUsername(string username);
        Task<List<RoleModel>> GetDataAsync();
    }
}
