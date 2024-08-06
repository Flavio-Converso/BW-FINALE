using Team_5.Context;
using Team_5.Models.Clinic;
using Team_5.Services.Interfaces;

namespace Team_5.Services
{
    public class ExaminationService : IExaminationService
    {
        private readonly DataContext _dataContext;

        public ExaminationService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<Examinations> CreateExaminationAsync(Examinations ex)
        {
            var examination = new Examinations
            {
                Animal = ex.Animal,
                ExaminationDate = ex.ExaminationDate,
                ExaminationName = ex.ExaminationName,
                Treatment = ex.Treatment
            };
            await _dataContext.Examinations.AddAsync(examination);
            await _dataContext.SaveChangesAsync();
            return examination;
        }
    }
}
