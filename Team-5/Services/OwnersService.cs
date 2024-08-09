using Microsoft.EntityFrameworkCore;
using Team_5.Context;
using Team_5.Models.Auth;
using Team_5.Models.Clinic;
using Team_5.Models.ViewModels;
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

        public async Task<OwnerViewModel> CreateOwnersAsync(OwnerViewModel o)
        {
            var user = await _ctx.Users.FindAsync(o.IdUser);
            var owner = new Owners
            {
                Name = o.Owners.Name,
                Surname = o.Owners.Surname,
                PhoneNumber = o.Owners.PhoneNumber,
                CF = o.Owners.CF,
                User = user!
            };

            await _ctx.Owners.AddAsync(owner);
            await _ctx.SaveChangesAsync();
            return o;
        }

        public async Task<List<Owners>> GetAllOwnersAsync()
        {
            return await _ctx.Owners.Include(o => o.User).ToListAsync();
        }

        public async Task<List<Users>> GetAllUsers()
        {
            return await _ctx.Users.ToListAsync();

        }

        public async Task<Owners> DeleteOwner(int id)
        {
            var owner = await _ctx.Owners.FindAsync(id);
            _ctx.Owners.Remove(owner);
            await _ctx.SaveChangesAsync();
            return owner;
        }
    }
}
