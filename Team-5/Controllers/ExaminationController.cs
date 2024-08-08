using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Team_5.Models.Clinic;
using Team_5.Services.Interfaces;

namespace Team_5.Controllers
{
    [Authorize(Policy = "VetPolicy")]
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateExaminationAsync(Examinations ex)
        {
            var animalExists = await _examinationSvc.AnimalExistsAsync(ex.AnimalId);
            if (!animalExists)
            {
                ModelState.AddModelError("AnimalId", "Invalid Animal ID");
                return View(ex);
            }

            await _examinationSvc.CreateExaminationAsync(ex);
            return RedirectToAction("ExaminationsList");
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteExamination(int id)
        {
            var deletedExam = await _examinationSvc.DeleteExamination(id);

            if (deletedExam == null)
            {
                // Se l'ordine non esiste, restituisci un 404 Not Found
                return NotFound(new { Message = "Order not found." });
            }

            return RedirectToAction("ExaminationsList");

        }
    }
}
