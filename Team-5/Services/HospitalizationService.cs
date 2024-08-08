using Microsoft.EntityFrameworkCore;
using Team_5.Context;
using Team_5.Models.Clinic;
using Team_5.Models.ViewModels;
using Team_5.Services.Interfaces;

namespace Team_5.Services
{
    public class HospitalizationService : IHospitalizationService
    {
        private readonly DataContext _dataContext;
        private readonly ILogger<HospitalizationService> _logger;
        public HospitalizationService(DataContext dataContext, ILogger<HospitalizationService> logger)
        {
            _dataContext = dataContext;
            _logger = logger;
        }


        // ricovero animale già registrato
        public async Task<Hospitalizations> CreateHospitalizationsAsync(Hospitalizations hosp)
        {
            var animal = await _dataContext.Animals.FindAsync(hosp.AnimalId);
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

            await _dataContext.Hospitalizations.AddAsync(hospitalization);
            await _dataContext.SaveChangesAsync();
            return hospitalization;
        }

        public async Task<List<Hospitalizations>> GetHospitalizationsWithAnimalsAsync()
        {
            return await _dataContext.Hospitalizations
                .Include(h => h.Animal)
                .ThenInclude(a => a.Breed)
                .Include(h => h.Animal)
                .ThenInclude(a => a.Owner)
                .ToListAsync();
        }

        public async Task<List<Hospitalizations>> GetActiveHospitalizationsAsync()
        {
            return await _dataContext.Hospitalizations
                .Include(a => a.Animal)
                .ThenInclude(o => o.Owner)
                .Where(h => h.IsHospitalized == true)
                .ToListAsync();
        }

        public async Task<AnimalHospitalizationViewModel> CreateAnimalHospitalizationViewModel(AnimalHospitalizationViewModel viewModel)
        {
            var breed = await _dataContext.Breeds.FindAsync(viewModel.IdBreed);
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


            await _dataContext.Animals.AddAsync(animal);
            await _dataContext.SaveChangesAsync();

            var hospitalization = new Hospitalizations
            {
                IsHospitalized = true,
                HospDate = DateTime.Now,
                AnimalId = animal.IdAnimal
            };

            await _dataContext.Hospitalizations.AddAsync(hospitalization);
            await _dataContext.SaveChangesAsync();
            return viewModel;
        }


    }
}
