using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Team_5.Context;
using Team_5.Models.Clinic;
using Team_5.Services.Interfaces;

namespace Team_5.Services
{
    public class RecoveryService : IRecoveryService
    {
        private readonly DataContext _dataContext;
        public RecoveryService(DataContext dataContext)
        {
            _dataContext = dataContext;
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
        public async Task<(Animals, Hospitalizations)> CreateAnimalAndHospitalizationAsync(Animals animal, Hospitalizations hospitalization)
        {
            // Aggiungi l'animale al contesto
            _dataContext.Animals.Add(animal);
            await _dataContext.SaveChangesAsync();

            // Associa l'animale appena creato al ricovero
            hospitalization.AnimalId = animal.IdAnimal;
            hospitalization.Animal = animal;

            // Aggiungi il ricovero al contesto
            _dataContext.Hospitalizations.Add(hospitalization);
            await _dataContext.SaveChangesAsync();

            return (animal, hospitalization);
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
