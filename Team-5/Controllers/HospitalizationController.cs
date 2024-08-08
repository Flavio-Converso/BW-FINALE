
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Team_5.Context;
using Team_5.Models.Clinic;
using Team_5.Models.ViewModels;
using Team_5.Services.Interfaces;

namespace Team_5.Controllers
{
    public class HospitalizationController : Controller
    {
        private readonly IHospitalizationService _hospitalizationService;
        private readonly DataContext _dataContext;
        private readonly IBreedsService _breedsService;
        private readonly IAnimalsService _animalsService;


        public HospitalizationController(IHospitalizationService hospitalizationService, DataContext dataContext, IBreedsService breedsService, IAnimalsService animalsService)
        {
            _hospitalizationService = hospitalizationService;
            _dataContext = dataContext;
            _breedsService = breedsService;
            _animalsService = animalsService;
        }


        [HttpGet]
        public async Task<ActionResult<List<Examinations>>> ActiveHospitalizations()
        {
            var isHospitalized = await _hospitalizationService.GetActiveHospitalizationsAsync();
            return View(isHospitalized);
        }


        [HttpGet]
        public IActionResult CreateHospitalization()
        {
            return View();
        }


        [HttpPost]
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


        // crea animale e ricovero assieme



        public async Task<IActionResult> CreateAnimalAndHospitalization()
        {

            ViewBag.Breeds = await _dataContext.Breeds.ToListAsync();
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



