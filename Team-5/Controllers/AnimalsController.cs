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
        public async Task<IActionResult> GetAnimalDataByMicrochip(int microchipId)
        {
            var animals = await _animalsSvc.GetAnimalByMicrochipAsync(microchipId);
            return Ok(animals);
        }

        public async Task<IActionResult> AnimalsWithoutOwner()
        {
            var animals = await _animalsSvc.GetAnimalsWithoutOwnerAsync();
            return View(animals);
        }
    }
}
