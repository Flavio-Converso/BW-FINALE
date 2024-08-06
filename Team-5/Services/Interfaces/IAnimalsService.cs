using Team_5.Models.Clinic;
using Team_5.Models.ViewModels;

namespace Team_5.Services.Interfaces
{
    public interface IAnimalsService
    {
        public Task<List<Breeds>> GetAllBreedsAsync();
        public Task<Animals> CreateAnimals(CreateAnimalViewModel viewModel);
    }
}
