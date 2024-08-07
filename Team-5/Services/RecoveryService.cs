using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Team_5.Context;
using Team_5.Models.Clinic;
using Team_5.Services.Interfaces;

namespace Team_5.Services
{
    public class RecoveryService : IRecoveryService
    {
        private readonly DataContext _dataContext;
        public RecoveryService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<List<Hospitalizations>> GetExaminationsAsync()
        {
            return await _dataContext.Hospitalizations
                .Include(a => a.Animal)
                .ThenInclude(o => o.Owner)
                .Where(h => h.IsHospitalized == true)
                .ToListAsync();
        }
    }
}
