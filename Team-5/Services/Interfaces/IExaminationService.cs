using Team_5.Models.Clinic;

namespace Team_5.Services.Interfaces
{
    public interface IExaminationService
    {
        Task<Examinations> CreateExaminationAsync(Examinations ex);
        Task<List<Examinations>> GetAllExaminationsAsync();
        Task<List<Examinations>> GetAllExaminationsByIdAnimalAsync(int IdAnimal);
        Task<List<Animals>> GetAllAnimalsAsync();
        Task<bool> AnimalExistsAsync(int animalId);
        Task<Examinations> DeleteExamination(int id);
    }
}
