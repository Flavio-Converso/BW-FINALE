using Microsoft.AspNetCore.Mvc;
using Team_5.Models.Clinic;

namespace Team_5.Services.Interfaces
{
    public interface IHospitalizationService
    {
        public Task<Hospitalizations> CreateHospitalizationsAsync(Hospitalizations hosp);
        public Task<List<Hospitalizations>> GetActiveHospitalizationsAsync();
        public Task<(Animals animal, Hospitalizations hospitalization)> CreateAnimalAndHospitalizationAsync(Animals animal, Hospitalizations hospitalization);
    }
}
