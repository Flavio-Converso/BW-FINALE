using Team_5.Models.Clinic;

namespace Team_5.Services.Interfaces
{
    public interface IAnimalsService
    {
        public Task<List<Breeds>> GetAllBreedsAsync();
        public Task<Animals> CreateAnimals(CreateAnimalViewModel viewModel);
    }
}
