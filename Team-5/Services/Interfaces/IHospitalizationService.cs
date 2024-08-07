using Microsoft.AspNetCore.Mvc;
using Team_5.Models.Clinic;
using Team_5.Models.ViewModels;

namespace Team_5.Services.Interfaces
{
    public interface IHospitalizationService
    {
        public Task<Hospitalizations> CreateHospitalizationsAsync(Hospitalizations hosp);
        public Task<List<Hospitalizations>> GetActiveHospitalizationsAsync();
        public Task<AnimalHospitalizationViewModel> CreateAnimalAndHospitalizationAsync(AnimalHospitalizationViewModel viewModel);
    }
}
