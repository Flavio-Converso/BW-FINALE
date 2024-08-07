using Microsoft.AspNetCore.Mvc;
using Team_5.Context;
using Team_5.Models.Clinic;
using Team_5.Services.Interfaces;

namespace Team_5.Controllers
{
    public class RecoveryController : Controller
    {
        private readonly IRecoveryService _recoveryService;
        private readonly DataContext _dataContext;
        public RecoveryController (IRecoveryService recoveryService,DataContext dataContext)
        {
            _recoveryService = recoveryService;
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Examinations>>> ActiveHospitalizations()
        {
            var isHospitalized = await _recoveryService.GetActiveHospitalizationsAsync();
            return View(isHospitalized);
        }
    }
}
