using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Team_5.Services.Interfaces;

namespace Team_5.Controllers
{
    [Authorize(Policy = "MasterPolicy")]
    public class MasterController : Controller
    {
        private readonly IMasterService _masterService;

        public MasterController(IMasterService masterService)
        {
            _masterService = masterService;
        }

        public IActionResult ManageRoles()
        {
            return View();
        }
    }
}
