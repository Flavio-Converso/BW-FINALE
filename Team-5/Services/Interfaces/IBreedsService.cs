using Team_5.Models.Clinic;

namespace Team_5.Services.Interfaces
{
    public interface IBreedsService
    {
        public Task<List<Breeds>> GetAllBreedsAsync();
    }
}
