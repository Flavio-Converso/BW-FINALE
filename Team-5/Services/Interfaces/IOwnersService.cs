using Team_5.Models.Auth;
using Team_5.Models.Clinic;

namespace Team_5.Services.Interfaces
{
    public interface IOwnersService
    {
        Task<List<Owners>> GetAllOwnersAsync();
        Task<Owners> CreateOwnersAsync(Owners owners);
        Task<List<Users>> GetAllUsers();

    }
}
