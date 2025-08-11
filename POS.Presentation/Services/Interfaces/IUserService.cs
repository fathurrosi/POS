using POS.Presentation.Models;

namespace POS.Presentation.Services.Interfaces
{
    public interface  IUserService
    {
        Task<List<UserModel>> GetDataAsync();
    }
}
