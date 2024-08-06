using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Team_5.Context;
using Team_5.Models.Clinic;
using Team_5.Services.Interfaces;

namespace Team_5.Services
{
    public class ExaminationService : IExaminationService
    {

        private readonly DataContext _context;

        public ExaminationService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Examinations>> GetAllExaminationsAsync()
        {
            return await _context.Examinations
                .Include(e => e.Animal)
                .OrderByDescending(e => e.ExaminationDate)
                .ToListAsync();
        }
    }
}
