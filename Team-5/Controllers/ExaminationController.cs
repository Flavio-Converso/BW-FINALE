using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Team_5.Context;
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

        // Azione per visualizzare l'elenco delle visite per ciascun animale
        public async Task<IActionResult> ExaminationsList()
        {
            var esami = await _examinationService.GetAllExaminationsAsync();
            return View(esami);
        }
    }
}
