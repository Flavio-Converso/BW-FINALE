using Team_5.Models.Clinic;

namespace Team_5.Services.Interfaces
{
    public interface IBreedsService
    {
<<<<<<< HEAD
        public Task<List<Breeds>> GetAllBreedsAsync();
        public Task<Breeds> GetBreedByIdAsync(int id);
=======
        Task<List<Breeds>> GetAllBreedsAsync();
        Task<Breeds> GetBreedByIdAsync(int id);
>>>>>>> main
    }
}
