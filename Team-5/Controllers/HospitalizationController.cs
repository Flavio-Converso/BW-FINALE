using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Team_5.Models.Clinic;
using Team_5.Models.ViewModels;
using Team_5.Services.Interfaces;

namespace Team_5.Controllers
{
    [Authorize(Policy = "VetPolicy")]
    public class HospitalizationController : Controller
    {
        private readonly IHospitalizationService _hospitalizationSvc;

        public HospitalizationController(IHospitalizationService hospitalizationService)
        {
            _hospitalizationSvc = hospitalizationService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Hospitalizations>>> ActiveHospitalizations()
        {
            var isHospitalized = await _hospitalizationSvc.GetActiveHospitalizationsAsync();
            return View(isHospitalized);
        }

        [HttpGet]
        public async Task<IActionResult> CreateHospitalization()
        {
            ViewBag.Animals = await _hospitalizationSvc.GetAllAnimalsAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateHospitalization(Hospitalizations hospitalization)
        {
            try
            {
                var createdHospitalization = await _hospitalizationSvc.CreateHospitalizationsAsync(hospitalization);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(hospitalization);
            }
        }

        [HttpGet]
        public async Task<IActionResult> CreateAnimalAndHospitalization()
        {
            ViewBag.Breeds = await _hospitalizationSvc.GetAllBreedsAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAnimalAndHospitalization(AnimalHospitalizationViewModel viewModel)
        {
            await _hospitalizationSvc.CreateAnimalHospitalizationViewModel(viewModel);
            return RedirectToAction("Index", "Home");
        }
    }
}
