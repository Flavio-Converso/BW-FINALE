
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
        private readonly ILogger<HospitalizationController> _logger;


        public HospitalizationController(IHospitalizationService hospitalizationService, DataContext dataContext, IBreedsService breedsService, IAnimalsService animalsService, ILogger<HospitalizationController> logger)
        {
            _hospitalizationService = hospitalizationService;
            _dataContext = dataContext;
            _breedsService = breedsService;
            _animalsService = animalsService;
            _logger = logger;
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


         [HttpGet]
        public async Task<IActionResult> CreateAnimalAndHospitalization()
        {
            var breeds = await _dataContext.Breeds.ToListAsync();
            var viewModel = new AnimalHospitalizationViewModel
            {
                Animal = new Animals
                {
                    Name = "",
                    Color = "",
                    RegistrationDate = DateTime.Now,
                    Breed = new Breeds()
                },
                Hospitalization = new Hospitalizations
                {
                    IsHospitalized = false,
                    HospDate = DateTime.Now
                },
                Breeds = await _breedsService.GetAllBreedsAsync()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAnimalAndHospitalization(AnimalHospitalizationViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Se è necessario, gestisci i file immagine
                    if (Request.Form.Files.Count > 0)
                    {
                        var file = Request.Form.Files[0];
                        using (var memoryStream = new MemoryStream())
                        {
                            await file.CopyToAsync(memoryStream);
                            viewModel.Animal.Image = memoryStream.ToArray();
                        }
                    }

                    // Imposta il breed selezionato
                    viewModel.Animal.Breed = await _breedsService.GetBreedByIdAsync(viewModel.Animal.Breed.IdBreed);

                    // Utilizza il servizio per creare l'animale e il ricovero
                    var createdViewModel = await _hospitalizationService.CreateAnimalAndHospitalizationAsync(viewModel);

                    return RedirectToAction("Index", "Home");
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Si è verificato un errore durante la creazione dell'animale e del ricovero: " + ex.Message);
                    _logger.LogError(ex, "Errore durante la creazione dell'animale e del ricovero");
                }
            }

            // Ricarica le razze nel view model in caso di errore di validazione
            viewModel.Breeds = await _breedsService.GetAllBreedsAsync();
            return View(viewModel);
        }
    }
}
    



