using Microsoft.AspNetCore.Mvc;
using Team_5.Models.Clinic;
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
        public async Task<IActionResult> CreateOwner(Owners owner)
        {
            if (!ModelState.IsValid)
            {
                return View(owner);
            }

            try
            {
                await _ownersService.CreateOwnersAsync(owner);
                return RedirectToAction("OwnersList");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while creating the owner. Please try again.");
                return View(owner);
            }
        }

        [HttpGet]
        public async Task<IActionResult> OwnersList()
        {
            var owners = await _ownersService.GetAllOwnersAsync();
            return View(owners);
        }
    }
}
