using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Team_5.Models.ViewModels;
using Team_5.Services.Interfaces;

namespace Team_5.Controllers
{
    [Authorize]
    public class AnimalsController : Controller
    {
        private readonly IAnimalsService _animalsService;

        public AnimalsController(IAnimalsService animalsService)
        {
            _animalsService = animalsService;
        }

        [HttpGet]

        public async Task<IActionResult> CreateAnimal()
        {
            var viewModel = await _animalsService.GetCreateAnimalViewModelAsync();
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
                    var animal = await _animalsService.CreateAnimalAsync(viewModel);
                    return RedirectToAction("AnimalList");
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            viewModel.Breeds = await _animalsService.GetAllBreedsAsync();
            return View(viewModel);
        }

        public async Task<IActionResult> AnimalList()
        {
            var animals = await _animalsService.GetAllAnimalsAsync();
            return View(animals);
        }

        public IActionResult GetAnimalByMicrochip()
        {
            return View();
        }

        [HttpGet("Animals/GetAnimalDataByMicrochip")]
        public async Task<IActionResult> GetAnimalDataByMicrochip(string microchipId)
        {
            var animals = await _animalsService.GetAnimalByMicrochipAsync(microchipId);
            return Ok(animals);
        }
    }
}
