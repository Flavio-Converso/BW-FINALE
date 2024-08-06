using Microsoft.AspNetCore.Mvc;
using Team_5.Models.Clinic;
using Team_5.Services.Interfaces;

public class AnimalsController : Controller
{
    private readonly IAnimalsService _animalsService;
    private readonly IBreedsService _breedsService;

    public AnimalsController(IAnimalsService animalsService, IBreedsService breedsService)
    {
        _animalsService = animalsService;
        _breedsService = breedsService;
    }

    [HttpGet]
    public IActionResult CreateAnimal()
    {
        var breeds = _breedsService.GetAllBreeds();

        var model = new CreateAnimalViewModel
        {
            Animal = new Animals(),
            Breeds = breeds
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult CreateAnimal(Animals animal)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var createdAnimal = _animalsService.CreateAnimal(animal);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Errore durante la creazione dell'animale: {ex.Message}");
            }
        }
     
        var breeds = _breedsService.GetAllBreeds();
        var model = new CreateAnimalViewModel
        {
            Animal = animal,
            Breeds = breeds
        };
        return View(model);
    }
}
