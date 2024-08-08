using Microsoft.AspNetCore.Mvc;
using Team_5.Models.Clinic;
using Team_5.Models.ViewModels;
using Team_5.Services.Interfaces;

namespace Team_5.Controllers
{
    public class HospitalizationController : Controller
    {
        private readonly IHospitalizationService _hospitalizationService;

        public HospitalizationController(IHospitalizationService hospitalizationService)
        {
            _hospitalizationService = hospitalizationService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Hospitalizations>>> ActiveHospitalizations()
        {
            var isHospitalized = await _hospitalizationService.GetActiveHospitalizationsAsync();
            return View(isHospitalized);
        }

        [HttpGet]
        public async Task<IActionResult> CreateHospitalization()
        {
            ViewBag.Animals = await _hospitalizationService.GetAllAnimalsAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateHospitalization(Hospitalizations hospitalization)
        {
            try
            {
                var createdHospitalization = await _hospitalizationService.CreateHospitalizationsAsync(hospitalization);
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
            ViewBag.Breeds = await _hospitalizationService.GetAllBreedsAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAnimalAndHospitalization(AnimalHospitalizationViewModel viewModel)
        {
            await _hospitalizationService.CreateAnimalHospitalizationViewModel(viewModel);
            return RedirectToAction("Index", "Home");
        }
    }
}
