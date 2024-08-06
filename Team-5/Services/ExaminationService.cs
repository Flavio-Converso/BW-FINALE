using Microsoft.EntityFrameworkCore;
using Team_5.Context;
using Team_5.Models.Clinic;
using Team_5.Services.Interfaces;

namespace Team_5.Services
{
    public class ExaminationService : IExaminationService
    {
        private readonly DataContext _dataContext;

        public ExaminationService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<Examinations> CreateExaminationAsync(Examinations ex)
        {
            var animal = await _dataContext.Animals.FindAsync(ex.AnimalId);
            if (animal == null)
            {
                throw new Exception("Animal not found");
            }

            var examination = new Examinations
            {
                Animal = animal,
                ExaminationDate = ex.ExaminationDate,
                ExaminationName = ex.ExaminationName,
                Treatment = ex.Treatment
            };

            await _dataContext.Examinations.AddAsync(examination);
            await _dataContext.SaveChangesAsync();
            return examination;
        }


        public async Task<List<Examinations>> GetAllExaminationsAsync()
        {
            return await _dataContext.Examinations
                .Include(e => e.Animal)
                .OrderByDescending(e => e.ExaminationDate)
                .ToListAsync();
        }
    }
}
