using Microsoft.EntityFrameworkCore;
using Team_5.Context;
using Team_5.Models.Auth;
using Team_5.Models.Clinic;
using Team_5.Services.Interfaces;

namespace Team_5.Services
{
    public class OwnersService : IOwnersService
    {
        private readonly DataContext _ctx;

        public OwnersService(DataContext context)
        {
            _ctx = context;
        }

        public async Task<Owners> CreateOwnersAsync(Owners o)
        {
            var user = await _ctx.Users.FirstOrDefaultAsync(u => u.IdUser == o.User.IdUser);
            var owner = new Owners
            {
                Name = o.Name,
                Surname = o.Surname,
                PhoneNumber = o.PhoneNumber,
                CF = o.CF,
                User = user!
            };

            await _ctx.Owners.AddAsync(owner);
            await _ctx.SaveChangesAsync();
            return owner;
        }

        public async Task<List<Owners>> GetAllOwnersAsync()
        {
            return await _ctx.Owners.ToListAsync();
        }

        public async Task<List<Users>> GetAllUsers()
        {
            return await _ctx.Users.ToListAsync();

        }
    }
}
