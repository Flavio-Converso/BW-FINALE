using Microsoft.EntityFrameworkCore;
using Team_5.Context;
using Team_5.Models.Auth;
using Team_5.Services.Interfaces;

namespace Team_5.Services
{
    public class MasterService : IMasterService
    {
        private readonly DataContext _ctx;

        public MasterService(DataContext dataContext)
        {
            _ctx = dataContext;
        }

        public async Task<List<Users>> GetAllUsersWithRolesAsync()
        {
            return await _ctx.Users.Include(u => u.Roles).ToListAsync();
        }

        public async Task<List<Roles>> GetAllRolesAsync()
        {
            return await _ctx.Roles.ToListAsync();
        }

        public async Task<bool> ToggleUserRoleAsync(int idUser, int idRole)
        {
            var user = await _ctx.Users
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.IdUser == idUser);

            if (user == null)
                return false;

            var role = await _ctx.Roles.FindAsync(idRole);
            if (role == null)
                return false;

            if (user.Roles.Contains(role))
            {
                user.Roles.Remove(role);
            }
            else
            {
                user.Roles.Add(role);
            }

            await _ctx.SaveChangesAsync();
            return true;
        }

        public async Task CreateRoleAsync(Roles role)
        {
            var r = new Roles { Name = role.Name };
            await _ctx.Roles.AddAsync(r);
            await _ctx.SaveChangesAsync();
        }
    }
}
