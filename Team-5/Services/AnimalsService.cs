using Microsoft.EntityFrameworkCore;
using Team_5.Context;
using Team_5.Models.Clinic;
using Team_5.Models.ViewModels;
using Team_5.Services.Interfaces;

namespace Team_5.Services
{
    public class AnimalsService : IAnimalsService
    {
        private readonly DataContext _ctx;

        public AnimalsService(DataContext dataContext)
        {
            _ctx = dataContext;
        }

        public async Task<Animals> CreateAnimals(CreateAnimalViewModel viewModel)
        {
            var breed = await _ctx.Breeds.FindAsync(viewModel.SelectedBreedId);


            var animal = new Animals
            {
                Name = viewModel.Name,
                RegistrationDate = viewModel.RegistrationDate,
                BirthDate = viewModel.BirthDate,
                NumMicrochip = viewModel.NumMicrochip,
                Image = viewModel.Image,
                Color = viewModel.Color,
                OwnerId = viewModel.OwnerId,
                Breed = breed
            };

            _ctx.Animals.Add(animal);
            await _ctx.SaveChangesAsync();

            return animal;
        }

        public async Task<List<Breeds>> GetAllBreedsAsync()
        {
            var breeds = await _ctx.Breeds.ToListAsync();
            return breeds;
        }
    }
}
