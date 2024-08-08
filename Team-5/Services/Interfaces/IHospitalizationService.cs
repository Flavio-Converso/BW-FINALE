using Team_5.Models.Clinic;
using Team_5.Models.ViewModels;

namespace Team_5.Services.Interfaces
{
    public interface IHospitalizationService
    {
        Task<Hospitalizations> CreateHospitalizationsAsync(Hospitalizations hosp);
        Task<List<Hospitalizations>> GetActiveHospitalizationsAsync();
        Task<AnimalHospitalizationViewModel> CreateAnimalHospitalizationViewModel(AnimalHospitalizationViewModel viewModel);

    }
}
