using Team_5.Models.Auth;

namespace Team_5.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<Users> RegisterAsync(Users user);
        public Task<Users> LoginAsync(Users user);
    }
}
