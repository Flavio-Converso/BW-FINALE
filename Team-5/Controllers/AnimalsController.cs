using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Team_5.Models.ViewModels;
using Team_5.Services.Interfaces;

namespace Team_5.Controllers
{
    [Authorize]
    public class AnimalsController : Controller
    {
        private readonly IAnimalsService _animalsSvc;
        private readonly IBreedsService _breedSvc;

        public AnimalsController(IAnimalsService animalsService, IBreedsService breedsService)
        {
            _animalsSvc = animalsService;
            _breedSvc = breedsService;
        }

        [HttpGet]

        public async Task<IActionResult> CreateAnimal()
        {
            var viewModel = await _animalsSvc.GetCreateAnimalViewModelAsync();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> CreateAnimal(CreateAnimalViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var animal = await _animalsSvc.CreateAnimalAsync(viewModel);
                    return RedirectToAction("AnimalList");
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            viewModel.Breeds = await _breedSvc.GetAllBreedsAsync();
            return View(viewModel);
        }

        public async Task<IActionResult> AnimalList()
        {
            var animals = await _animalsSvc.GetAllAnimalsAsync();
            return View(animals);
        }

        public IActionResult GetAnimalByMicrochip()
        {
            return View();
        }


        [HttpGet("Animals/GetAnimalDataByMicrochip")]
        public async Task<IActionResult> GetAnimalDataByMicrochip(string microchipId)
        {
            var animals = await _animalsSvc.GetAnimalByMicrochipAsync(microchipId);
            var animalData = animals.Select(animal => new
            {
                animal.Name,
                RegistrationDate = animal.RegistrationDate.ToString("yyyy-MM-dd"),
                BirthDate = animal.BirthDate.ToString("yyyy-MM-dd"),
                animal.Color,
                Image = animal.Image != null ? Convert.ToBase64String(animal.Image) : null,
                IsHospitalized = animal.Hospitalization != null && animal.Hospitalization.Any(h => h.IsHospitalized)
            }).ToList();

            return Ok(animalData);
        }


        public async Task<IActionResult> AnimalsWithoutOwner()
        {
            var animals = await _animalsSvc.GetAnimalsWithoutOwnerAsync();
            return View(animals);
        }

        // eliminazione animale

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAnimal(int id)
        {
            var success = await _animalsSvc.DeleteAnimalAsync(id);
            if (!success)
            {
                return NotFound();
            }

            return RedirectToAction("AnimalList");
        }
    }
}
