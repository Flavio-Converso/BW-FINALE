using Microsoft.EntityFrameworkCore;
using Team_5.Context;
using Team_5.Models.Clinic;
using Team_5.Services.Interfaces;

namespace Team_5.Services
{
    public class OwnerService : IOwnerService
    {
        private readonly DataContext _ctx;

        public OwnerService(DataContext context)
        {
            _ctx = context;
        }

        public async Task<List<Owners>> GetAllOwnersAsync()
        {
            return await _ctx.Owners.ToListAsync();
        }
    }
}
