using Team_5.Models.Auth;

namespace Team_5.Services.Interfaces
{
    public interface IMasterService
    {
        Task<List<Users>> GetAllUsersWithRolesAsync();
        Task<List<Roles>> GetAllRolesAsync();
        Task<bool> ToggleUserRoleAsync(int idUser, int idRole);
        Task CreateRoleAsync(Roles role);
    }
}
