using Microsoft.EntityFrameworkCore;
using Team_5.Context;
using Team_5.Models.Clinic;
using Team_5.Services.Interfaces;

namespace Team_5.Services
{
    public class ExaminationService : IExaminationService
    {
        private readonly DataContext _ctx;

        public ExaminationService(DataContext dataContext)
        {
            _ctx = dataContext;
        }

        public async Task<Examinations> CreateExaminationAsync(Examinations ex)
        {
            var animal = await _ctx.Animals.FindAsync(ex.AnimalId);
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

            await _ctx.Examinations.AddAsync(examination);
            await _ctx.SaveChangesAsync();
            return examination;
        }

        public async Task<List<Examinations>> GetAllExaminationsAsync()
        {
            return await _ctx.Examinations
                .Include(e => e.Animal)
                .OrderByDescending(e => e.ExaminationDate)
                .ToListAsync();
        }

        public async Task<List<Examinations>> GetAllExaminationsByIdAnimalAsync(int IdAnimal)
        {
            return await _ctx.Examinations
                .Where(e => e.AnimalId == IdAnimal)
                .Include(e => e.Animal)
                .OrderByDescending(e => e.ExaminationDate)
                .ToListAsync();
        }

        public async Task<List<Animals>> GetAllAnimalsAsync()
        {
            return await _ctx.Animals.ToListAsync();
        }

        public async Task<bool> AnimalExistsAsync(int animalId)
        {
            return await _ctx.Animals.AnyAsync(a => a.IdAnimal == animalId);
        }
    }
}
