using Team_5.Models.Clinic;
using Team_5.Models.ViewModels;

namespace Team_5.Services.Interfaces
{
    public interface IAnimalsService
    {
        Task<Animals> CreateAnimalAsync(CreateAnimalViewModel viewModel);

        Task<CreateAnimalViewModel> GetCreateAnimalViewModelAsync();

        Task<List<Animals>> GetAllAnimalsAsync();

        Task<List<Animals>> GetAnimalByMicrochipAsync(int microchipId);

        Task<List<Animals>> GetAnimalsWithoutOwnerAsync();
    }
}
