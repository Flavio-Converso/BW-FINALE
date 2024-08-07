using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Team_5.Context;
using Team_5.Models.Clinic;
using Team_5.Services.Interfaces;

namespace Team_5.Services
{
    public class HospitalizationService : IHospitalizationService
    {
        private readonly DataContext _dataContext;
        public HospitalizationService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<List<Hospitalizations>> GetActiveHospitalizationsAsync()
        {
            return await _dataContext.Hospitalizations
                .Include(a => a.Animal)
                .ThenInclude(o => o.Owner)
                .Where(h => h.IsHospitalized == true)
                .ToListAsync();
        }
    }
}
