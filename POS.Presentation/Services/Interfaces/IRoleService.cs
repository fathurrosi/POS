using POS.Presentation.Models;

namespace POS.Presentation.Services.Interfaces
{
    public interface IRoleService
    {
        Task<List<RoleModel>> GetDataAsync();
    }
}
