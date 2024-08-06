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
            Animal = new Animals
            {
                // Assicurati che tutti i membri obbligatori siano inizializzati, se possibile
                RegistrationDate = DateTime.Now,
                BirthDate = DateTime.Now,
                Color = "", // Può essere una stringa vuota per l'inizio
                Name = "",
                Breed = breeds.FirstOrDefault() ?? new Breeds()
            },
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
                
                if (animal.Breed != null && animal.Breed.IdBreed == 0)
                {
                    ModelState.AddModelError("Animal.Breed", "Razza non valida.");
                    return View(new CreateAnimalViewModel
                    {
                        Animal = animal,
                        Breeds = _breedsService.GetAllBreeds()
                    });
                }

                
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