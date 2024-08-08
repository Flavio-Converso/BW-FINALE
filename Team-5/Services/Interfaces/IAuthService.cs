using Team_5.Models.Auth;

namespace Team_5.Services.Interfaces
{
    public interface IAuthService
    {
        Task<Users> RegisterAsync(Users user);
        Task<Users> LoginAsync(Users user);
    }
}
