using Team_5.Models.Auth;
using Team_5.Models.Clinic;
using Team_5.Models.ViewModels;

namespace Team_5.Services.Interfaces
{
    public interface IOwnersService
    {
        Task<List<Owners>> GetAllOwnersAsync();
        Task<OwnerViewModel> CreateOwnersAsync(OwnerViewModel ow);
        Task<List<Users>> GetAllUsers();
        Task<Owners> DeleteOwner(int id);


    }
}
