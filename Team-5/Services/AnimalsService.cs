using Microsoft.EntityFrameworkCore;
using Team_5.Context;
using Team_5.Models.Clinic;
using Team_5.Models.ViewModels;
using Team_5.Services.Interfaces;

namespace Team_5.Services
{
    public class AnimalsService : IAnimalsService
    {
        private readonly DataContext _dataContext;

        public AnimalsService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Animals> CreateAnimalAsync(CreateAnimalViewModel viewModel)
        {
            var breed = await _dataContext.Breeds.FindAsync(viewModel.SelectedBreedId);
            if (breed == null)
            {
                throw new ArgumentException("Invalid breed ID.");
            }

            var existingAnimal = await _dataContext.Animals
                .FirstOrDefaultAsync(a => a.NumMicrochip == viewModel.NumMicrochip);
            if (existingAnimal != null)
            {
                throw new ArgumentException("The microchip number is already in use.");
            }

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
                Name = viewModel.Name,
                RegistrationDate = viewModel.RegistrationDate,
                BirthDate = viewModel.BirthDate,
                NumMicrochip = viewModel.NumMicrochip,
                Image = imageBytes,
                Color = viewModel.Color,
                OwnerId = viewModel.OwnerId,
                Breed = breed
            };

            _dataContext.Animals.Add(animal);
            await _dataContext.SaveChangesAsync();

            return animal;
        }

        public async Task<List<Breeds>> GetAllBreedsAsync()
        {
            return await _dataContext.Breeds.ToListAsync();
        }

        public async Task<CreateAnimalViewModel> GetCreateAnimalViewModelAsync()
        {
            var viewModel = new CreateAnimalViewModel
            {
                Name = "",
                RegistrationDate = DateTime.Now,
                Color = "",
                Breeds = await GetAllBreedsAsync()
            };

            return viewModel;
        }

        public async Task<List<Animals>> GetAllAnimalsAsync()
        {
            return await _dataContext.Animals.Include(a => a.Breed).ToListAsync();
        }

        public async Task<List<Animals>> GetAnimalByMicrochipAsync(string microchipId)
        {
            return await _dataContext.Animals.Where(a => a.NumMicrochip == microchipId).ToListAsync();
        }
    }
}
