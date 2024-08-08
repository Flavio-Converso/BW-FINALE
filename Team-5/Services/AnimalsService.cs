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
        private readonly IBreedsService _breedSvc;
        private readonly IOwnerService _ownerSvc;

        public AnimalsService(DataContext dataContext, IBreedsService breedsService, IOwnerService ownerService)
        {
            _ctx = dataContext;
            _breedSvc = breedsService;
            _ownerSvc = ownerService;
        }

        public async Task<Animals> CreateAnimalAsync(CreateAnimalViewModel viewModel)
        {
            var owner = await _ctx.Owners.FindAsync(viewModel.SelectedOwnerId);

            var breed = await _ctx.Breeds.FindAsync(viewModel.SelectedBreedId);


            var existingAnimal = await _ctx.Animals
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
                RegistrationDate = DateTime.Now,
                BirthDate = viewModel.BirthDate,
                NumMicrochip = viewModel.NumMicrochip,
                Image = imageBytes,
                Color = viewModel.Color,
                Owner = owner,
                Breed = breed
            };

            _ctx.Animals.Add(animal);
            await _ctx.SaveChangesAsync();

            return animal;
        }

        public async Task<CreateAnimalViewModel> GetCreateAnimalViewModelAsync()
        {
            var viewModel = new CreateAnimalViewModel
            {
                Name = "",
                RegistrationDate = DateTime.Now,
                Color = "",
                Owners = await _ownerSvc.GetAllOwnersAsync(),
                Breeds = await _breedSvc.GetAllBreedsAsync()
            };

            return viewModel;
        }

        public async Task<List<Animals>> GetAllAnimalsAsync()
        {

            return await _ctx.Animals.Include(a => a.Breed).ToListAsync();
        }

        public async Task<List<Animals>> GetAnimalByMicrochipAsync(int microchipId)
        {

            return await _ctx.Animals
                    .Include(a => a.Hospitalization) 
                    .Where(a => a.NumMicrochip == microchipId)
                    .ToListAsync();

        }

        public async Task<List<Animals>> GetAnimalsWithoutOwnerAsync()
        {
            return await _ctx.Animals
                             .Where(a => a.OwnerId == null)
                             .Include(a => a.Breed)
                             .ToListAsync();
        }
    }
}
