using Microsoft.AspNetCore.Mvc;
using Team_5.Models.ViewModels;
using Team_5.Services.Interfaces;

namespace Team_5.Controllers
{
    public class OwnersController : Controller
    {
        private readonly IOwnersService _ownersService;

        public OwnersController(IOwnersService ownersService)
        {
            _ownersService = ownersService;

        }

        [HttpGet]
        public async Task<IActionResult> CreateOwner()
        {
            ViewBag.Users = await _ownersService.GetAllUsers();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOwner(OwnerViewModel ow)
        {
            await _ownersService.CreateOwnersAsync(ow);
            return RedirectToAction("OwnersList");
        }


        [HttpGet]
        public async Task<IActionResult> OwnersList()
        {
            var owners = await _ownersService.GetAllOwnersAsync();
            return View(owners);
        }
    }
}
