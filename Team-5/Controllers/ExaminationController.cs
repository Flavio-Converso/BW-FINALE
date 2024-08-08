using Microsoft.AspNetCore.Mvc;
using Team_5.Models.Clinic;
using Team_5.Services.Interfaces;

namespace Team_5.Controllers
{
    public class ExaminationController : Controller
    {
        private readonly IExaminationService _examinationService;

        public ExaminationController(IExaminationService examinationService)
        {
            _examinationService = examinationService;
        }

        [HttpGet]
        public async Task<IActionResult> CreateExaminationAsync()
        {
            var animals = await _examinationService.GetAllAnimalsAsync();
            ViewBag.Animals = animals;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateExaminationAsync(Examinations ex)
        {
            var animalExists = await _examinationService.AnimalExistsAsync(ex.AnimalId);
            if (!animalExists)
            {
                ModelState.AddModelError("AnimalId", "Invalid Animal ID");
                return View(ex);
            }

            await _examinationService.CreateExaminationAsync(ex);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
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
