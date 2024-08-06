using Microsoft.EntityFrameworkCore;
using Team_5.Context;
using Team_5.Models.Auth;
using Team_5.Services.Interfaces;

namespace Team_5.Services
{
    public class AuthService : IAuthService
    {

        private readonly DataContext _ctx;
        public AuthService(DataContext dataContext)
        {
            _ctx = dataContext;
        }

        public async Task<Users> RegisterAsync(Users user)
        {
            user.Password = PasswordHelper.HashPassword(user.Password);
            var userRole = await _ctx.Roles.Where(r => r.IdRole == 3).FirstOrDefaultAsync(); //1 = master _ 2 = admin _ 3 = user
            user.Roles.Add(userRole);
            await _ctx.Users.AddAsync(user);
            await _ctx.SaveChangesAsync();
            return user;
        }

        public async Task<Users> LoginAsync(Users user)
        {
            string hashedPassword = PasswordHelper.HashPassword(user.Password);

            var existingUser = await _ctx.Users
                 .Include(u => u.Roles)
                 .Where(u => u.Username == user.Username && u.Password == hashedPassword)
                 .FirstOrDefaultAsync();

            if (existingUser == null)
            {
                throw new Exception("Invalid username or password.");
            }
            return existingUser;
        }


    }
}
