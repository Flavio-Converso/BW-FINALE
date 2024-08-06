using Team_5.Models.Clinic;

namespace Team_5.Services.Interfaces
{
    public interface IExaminationService
    {
        public Task<Examinations> CreateExaminationAsync(Examinations ex);
        Task<List<Examinations>> GetAllExaminationsAsync();
    }
}
