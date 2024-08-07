using Team_5.Models.Clinic;

namespace Team_5.Services.Interfaces
{
    public interface IRecoveryService
    {
        public Task<List<Hospitalizations>> GetActiveHospitalizationsAsync();
    }
}
