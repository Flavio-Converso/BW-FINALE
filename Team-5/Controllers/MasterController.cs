using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Team_5.Models.Auth;
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

        public async Task<IActionResult> ManageRoles()
        {
            ViewBag.Users = await _masterService.GetAllUsersWithRolesAsync();
            ViewBag.Roles = await _masterService.GetAllRolesAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ManageRoles(int idUser, int idRole)
        {
            await _masterService.ToggleUserRoleAsync(idUser, idRole);
            return RedirectToAction("ManageRoles");
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(Roles role)
        {
            await _masterService.CreateRoleAsync(role);
            return RedirectToAction("ManageRoles");
        }
    }
}
