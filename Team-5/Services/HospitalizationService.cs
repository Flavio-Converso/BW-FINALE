using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Team_5.Context;
using Team_5.Models.Clinic;
using Team_5.Models.ViewModels;
using Team_5.Services.Interfaces;

namespace Team_5.Services
{
    public class HospitalizationService : IHospitalizationService
    {
        private readonly DataContext _dataContext;
        private readonly ILogger<HospitalizationService> _logger;
        public HospitalizationService(DataContext dataContext, ILogger<HospitalizationService> logger)
        {
            _dataContext = dataContext;
            _logger = logger;
        }


        // ricovero animale già registrato
        public async Task<Hospitalizations> CreateHospitalizationsAsync(Hospitalizations hosp)
        {
            var animal = await _dataContext.Animals.FindAsync(hosp.AnimalId);
            if (animal == null)
            {
                throw new Exception("Animal not found");
            }

            var hospitalization = new Hospitalizations
            {
                Animal = animal,
                IsHospitalized=hosp.IsHospitalized=true,
                HospDate= hosp.HospDate,
            };

            await _dataContext.Hospitalizations.AddAsync(hospitalization);
            await _dataContext.SaveChangesAsync();
            return hospitalization;
        }


        // ricovero e registrazione animale
        public async Task<AnimalHospitalizationViewModel> CreateAnimalAndHospitalizationAsync(AnimalHospitalizationViewModel viewModel)
        {
            using var transaction = await _dataContext.Database.BeginTransactionAsync();

                // Aggiungi l'animale al contesto
                _dataContext.Animals.Add(viewModel.Animal);
                await _dataContext.SaveChangesAsync();

                // Associa l'animale appena creato al ricovero
                viewModel.Hospitalization.AnimalId = viewModel.Animal.IdAnimal;
                viewModel.Hospitalization.Animal = viewModel.Animal;

                // Aggiungi il ricovero al contesto
                _dataContext.Hospitalizations.Add(viewModel.Hospitalization);
                await _dataContext.SaveChangesAsync();

                await transaction.CommitAsync();

                return viewModel;

        }
        public async Task<List<Hospitalizations>> GetHospitalizationsWithAnimalsAsync()
        {
            return await _dataContext.Hospitalizations
                .Include(h => h.Animal)
                .ThenInclude(a => a.Breed)
                .Include(h => h.Animal)
                .ThenInclude(a => a.Owner)
                .ToListAsync();
        }
    

    [HttpGet]
        public async Task<List<Hospitalizations>> GetActiveHospitalizationsAsync()
        {
            return await _dataContext.Hospitalizations
                .Include(a => a.Animal)
                .ThenInclude(o => o.Owner)
                .Where(h => h.IsHospitalized == true)
                .ToListAsync();
        }

        
    }
}
