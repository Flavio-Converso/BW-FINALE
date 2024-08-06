using Microsoft.AspNetCore.Mvc;
using Team_5.Models.Clinic;
using Team_5.Services.Interfaces;

public class AnimalsController : Controller
{
    private readonly IAnimalsService _animalsService;

    public AnimalsController(IAnimalsService animalsService)
    {
        _animalsService = animalsService;
    }


    [HttpGet]
    public IActionResult CreateAnimal ()
    {
        return View();
    }


    [HttpPost]
    public IActionResult CreateAnimal (Animals animal)
    {
        try
        {
            var createdAnimal = _animalsService.CreateAnimal(animal);
            return Ok(createdAnimal);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
