using Microsoft.AspNetCore.Mvc;
using Team_5.Models.Clinic;
using Team_5.Services.Interfaces;

namespace Team_5.Controllers
{
    public class ExaminationController : Controller
    {
        private readonly IExaminationService _examinationSvc;

        public ExaminationController(IExaminationService examinationService)
        {
            _examinationSvc = examinationService;
        }

        [HttpGet]
        public async Task<IActionResult> CreateExaminationAsync()
        {
            var animals = await _examinationSvc.GetAllAnimalsAsync();
            ViewBag.Animals = animals;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateExaminationAsync(Examinations ex)
        {
            var animalExists = await _examinationSvc.AnimalExistsAsync(ex.AnimalId);
            if (!animalExists)
            {
                ModelState.AddModelError("AnimalId", "Invalid Animal ID");
                return View(ex);
            }

            await _examinationSvc.CreateExaminationAsync(ex);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> ExaminationsList()
        {
            var esami = await _examinationSvc.GetAllExaminationsAsync();
            return View(esami);
        }

        [HttpGet("Examination/ExaminationsListByIdAnimal")]
        public async Task<IActionResult> ExaminationsListByIdAnimal([FromQuery] int IdAnimal)
        {
            var esamiByAnimalId = await _examinationSvc.GetAllExaminationsByIdAnimalAsync(IdAnimal);
            return Ok(esamiByAnimalId);
        }
    }
}
