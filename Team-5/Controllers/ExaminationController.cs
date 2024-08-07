using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Team_5.Context;
using Team_5.Models.Clinic;
using Team_5.Services.Interfaces;

namespace Team_5.Controllers
{
    public class ExaminationController : Controller
    {
        private readonly IExaminationService _examinationService;
        private readonly DataContext _dataContext;

        public ExaminationController(IExaminationService examinationService, DataContext dataContext)
        {
            _dataContext = dataContext;
            _examinationService = examinationService;
        }

        public async Task<IActionResult> CreateExaminationAsync()
        {
            var animals = await _dataContext.Animals.ToListAsync();
            ViewBag.Animals = animals;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateExaminationAsync(Examinations ex)
        {
            // Verifica se l'AnimalId esiste
            var animalExists = _dataContext.Animals.Any(a => a.IdAnimal == ex.AnimalId);
            if (!animalExists)
            {
                ModelState.AddModelError("AnimalId", "Invalid AnimalId");
                return View(ex); // Ritorna la vista con l'errore
            }

            await _examinationService.CreateExaminationAsync(ex);
            return RedirectToAction("Index", "Home");
        }

        // Azione per visualizzare l'elenco delle visite per ciascun animale
        public async Task<IActionResult> ExaminationsList()
        {
            var esami = await _examinationService.GetAllExaminationsAsync();
            return View(esami);
        }

        [HttpGet("Examination/ExaminationsListByIdAnimal")]
        public async Task<IActionResult> ExaminationsListByIdAnimal([FromQuery] int IdAnimal)
        {
            var esamiByAnimalId = await _examinationService.GetAllExaminationsByIdAnimalAsync(IdAnimal);
            return Ok(esamiByAnimalId);
        }
    }
}
