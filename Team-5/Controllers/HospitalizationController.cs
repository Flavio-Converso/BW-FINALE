using Microsoft.AspNetCore.Mvc;
using Team_5.Context;
using Team_5.Models.Clinic;
using Team_5.Services.Interfaces;

namespace Team_5.Controllers
{
    public class HospitalizationController : Controller
    {
        private readonly IHospitalizationService _hospitalizationService;
        private readonly DataContext _dataContext;
        public HospitalizationController(IHospitalizationService hospitalizationService, DataContext dataContext)
        {
            _hospitalizationService = hospitalizationService;
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Examinations>>> ActiveHospitalizations()
        {
            var isHospitalized = await _hospitalizationService.GetActiveHospitalizationsAsync();
            return View(isHospitalized);
        }
    }
}
