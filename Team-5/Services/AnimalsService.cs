using System;
using Team_5.Context;
using Team_5.Models.Clinic;
using Team_5.Services.Interfaces;


namespace Team_5.Services
{
    public class AnimalsService : IAnimalsService
    {
        private readonly DataContext _context;

        public AnimalsService(DataContext context)
        {
            _context = context;
        }

        public Animals CreateAnimal(Animals animal)
        {
            if (animal == null)
            {
                throw new ArgumentNullException(nameof(animal));
            }

            _context.Animals.Add(animal);
            _context.SaveChanges();

            return animal;
        }
    }
}
