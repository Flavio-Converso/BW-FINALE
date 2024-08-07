
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Team_5.Context;
using Team_5.Models.Clinic;
using Team_5.Models.ViewModels;
using Team_5.Services.Interfaces;

namespace Team_5.Controllers
{
    public class HospitalizationController : Controller
    {
        private readonly IHospitalizationService _hospitalizationService;
        private readonly DataContext _dataContext;
        private readonly IBreedsService _breedsService;
        private readonly IAnimalsService _animalsService;


        public HospitalizationController(IHospitalizationService hospitalizationService, DataContext dataContext, IBreedsService breedsService, IAnimalsService animalsService)
        {
            _hospitalizationService = hospitalizationService;
            _dataContext = dataContext;
            _breedsService = breedsService;
            _animalsService = animalsService;
        }


        [HttpGet]
        public async Task<ActionResult<List<Examinations>>> ActiveHospitalizations()
        {
            var isHospitalized = await _hospitalizationService.GetActiveHospitalizationsAsync();
            return View(isHospitalized);
        }


        [HttpGet]
        public IActionResult CreateHospitalization()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateHospitalization(Hospitalizations hospitalization)
        {
            try
            {
                var createdHospitalization = await _hospitalizationService.CreateHospitalizationsAsync(hospitalization);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {

                ViewBag.ErrorMessage = ex.Message;
                return View(hospitalization);
            }
        }


        // crea animale e ricovero assieme



        public async Task<IActionResult> CreateAnimalAndHospitalization()
        {

            ViewBag.Breeds = await _dataContext.Breeds.ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAnimalAndHospitalization(AnimalHospitalizationViewModel viewModel)
        {
            var breed = await _dataContext.Breeds.FindAsync(viewModel.IdBreed);
            var animal = new Animals
            {
                Name = viewModel.Animal.Name,
                Color = viewModel.Animal.Color,
                RegistrationDate = viewModel.Animal.RegistrationDate,
                Breed = breed,
                BirthDate = DateTime.Now,
                NumMicrochip = viewModel.Animal.NumMicrochip,
            };


            await _dataContext.Animals.AddAsync(animal);
            await _dataContext.SaveChangesAsync();

            var hospitalization = new Hospitalizations
            {
                IsHospitalized = viewModel.Hospitalization.IsHospitalized,
                HospDate = viewModel.Hospitalization.HospDate,
                AnimalId = animal.IdAnimal // Assumendo che Hospitalizations abbia una proprietà AnimalId come chiave esterna
            };

            await _dataContext.Hospitalizations.AddAsync(hospitalization);
            await _dataContext.SaveChangesAsync();


            return RedirectToAction("Index", "Home");
        }
    }
}



