using Microsoft.EntityFrameworkCore;
using Team_5.Context;
using Team_5.Models.Clinic;
using Team_5.Services.Interfaces;

namespace Team_5.Services
{
    public class BreedsService : IBreedsService
    {
        private readonly DataContext _ctx;

        public BreedsService(DataContext context)
        {
            _ctx = context;
        }

        public async Task<List<Breeds>> GetAllBreedsAsync()
        {
            return await _ctx.Breeds.ToListAsync();
        }

        public async Task<Breeds> GetBreedByIdAsync(int id)
        {
            var breed = await _ctx.Breeds.FindAsync(id);

            if (breed == null)
            {
                throw new ArgumentException($"Breed con ID {id} non trovato.");
            }

            return breed;
        }
    }
}
