using Microsoft.EntityFrameworkCore;
using Team_5.Context;
using Team_5.Models.Clinic;
using Team_5.Models.ViewModels;
using Team_5.Services.Interfaces;

namespace Team_5.Services
{
    public class HospitalizationService : IHospitalizationService
    {
        private readonly DataContext _ctx;

        public HospitalizationService(DataContext dataContext)
        {
            _ctx = dataContext;
        }

        public async Task<Hospitalizations> CreateHospitalizationsAsync(Hospitalizations hosp)
        {
            var animal = await _ctx.Animals.FindAsync(hosp.AnimalId);
            if (animal == null)
            {
                throw new Exception("Animal not found");
            }

            var hospitalization = new Hospitalizations
            {
                Animal = animal,
                IsHospitalized = hosp.IsHospitalized = true,
                HospDate = hosp.HospDate,
            };

            await _ctx.Hospitalizations.AddAsync(hospitalization);
            await _ctx.SaveChangesAsync();

            return hospitalization;
        }

        public async Task<List<Hospitalizations>> GetHospitalizationsWithAnimalsAsync()
        {
            return await _ctx.Hospitalizations
                .Include(h => h.Animal)
                .ThenInclude(a => a.Breed)
                .Include(h => h.Animal)
                .ThenInclude(a => a.Owner)
                .ToListAsync();
        }

        public async Task<List<Hospitalizations>> GetActiveHospitalizationsAsync()
        {
            return await _ctx.Hospitalizations
                .Include(a => a.Animal)
                .ThenInclude(o => o.Owner)
                .Where(h => h.IsHospitalized)
                .ToListAsync();
        }

        public async Task<AnimalHospitalizationViewModel> CreateAnimalHospitalizationViewModel(AnimalHospitalizationViewModel viewModel)
        {
            var breed = await _ctx.Breeds.FindAsync(viewModel.IdBreed);

            byte[] imageBytes = null;

            if (viewModel.Image != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await viewModel.Image.CopyToAsync(memoryStream);
                    imageBytes = memoryStream.ToArray();
                }
            }

            var animal = new Animals
            {
                Name = viewModel.Animal.Name,
                Color = viewModel.Animal.Color,
                RegistrationDate = DateTime.Now,
                Breed = breed,
                BirthDate = viewModel.Animal.BirthDate,
                NumMicrochip = viewModel.Animal.NumMicrochip,
                Image = imageBytes,
            };

            await _ctx.Animals.AddAsync(animal);
            await _ctx.SaveChangesAsync();

            var hospitalization = new Hospitalizations
            {
                IsHospitalized = true,
                HospDate = DateTime.Now,
                AnimalId = animal.IdAnimal
            };

            await _ctx.Hospitalizations.AddAsync(hospitalization);
            await _ctx.SaveChangesAsync();

            return viewModel;
        }

        public async Task<List<Animals>> GetAllAnimalsAsync()
        {
            return await _ctx.Animals.ToListAsync();
        }

        public async Task<List<Breeds>> GetAllBreedsAsync()
        {
            return await _ctx.Breeds.ToListAsync();
        }
    }
}
