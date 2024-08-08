using Team_5.Models.Clinic;

namespace Team_5.Services.Interfaces
{
    public interface IBreedsService
    {
        Task<List<Breeds>> GetAllBreedsAsync();
        Task<Breeds> GetBreedByIdAsync(int id);
    }
}
