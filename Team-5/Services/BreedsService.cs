using System.Collections.Generic;
using System.Linq;
using Team_5.Context;
using Team_5.Models.Clinic;
using Team_5.Services.Interfaces;

namespace Team_5.Services
{
    public class BreedsService : IBreedsService
    {
        private readonly DataContext _context;

        public BreedsService(DataContext context)
        {
            _context = context;
        }

        public List<Breeds> GetAllBreeds()
        {
            return _context.Breeds.ToList();
        }
    }
}
