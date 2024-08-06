using Microsoft.AspNetCore.Mvc;
using Team_5.Models.ViewModels;
using Team_5.Services.Interfaces;

public class AnimalsController : Controller
{
    private readonly IAnimalsService _animalsService;

    public AnimalsController(IAnimalsService animalsService)
    {
        _animalsService = animalsService;
    }

    // GET: /Animals/Create
    public async Task<IActionResult> CreateAnimal()
    {
        var viewModel = new CreateAnimalViewModel
        {
            Name = "Fido",
            RegistrationDate = DateTime.Now,
            Color = "Brown",
            Breeds = await _animalsService.GetAllBreedsAsync()
        };

        return View(viewModel); // Returns a view with a form for creating an animal
    }

    // POST: /Animals/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateAnimal(CreateAnimalViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var animal = await _animalsService.CreateAnimals(viewModel);
                // Redirect to a success page or display a success message
                return RedirectToAction("Index", "Home"); // or another appropriate action
            }
            catch (ArgumentException ex)
            {
                // Handle error, e.g., invalid breed ID
                ModelState.AddModelError("", ex.Message);
            }
        }

        // If we got this far, something failed. Redisplay the form with the existing data.
        viewModel.Breeds = await _animalsService.GetAllBreedsAsync();
        return View(viewModel);
    }

}
