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

        public IActionResult CreateExaminationAsync()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateExaminationAsync(Examinations ex)
        {
            await _examinationService.CreateExaminationAsync(ex);
            return RedirectToAction("Index", "Home");
        }
    }
}
