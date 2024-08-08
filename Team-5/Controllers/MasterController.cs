using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Team_5.Context;
using Team_5.Models.Auth;
using Team_5.Services.Interfaces;

namespace Team_5.Controllers
{
    [Authorize(Policy = "MasterPolicy")]
    public class MasterController : Controller
    {
        private readonly IMasterService _masterService;
        private readonly DataContext _dataContext;
        public MasterController(IMasterService masterService, DataContext dataContext)
        {
            _masterService = masterService;
            _dataContext = dataContext;
        }

        public async Task<IActionResult> ManageRoles()
        {
            ViewBag.Users = await _dataContext.Users.Include(u => u.Roles).ToListAsync();

            ViewBag.Roles = await _dataContext.Roles.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ManageRoles(int idUser, int idRole)
        {
            var user = await _dataContext.Users
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.IdUser == idUser);

            var role = await _dataContext.Roles.FindAsync(idRole);

            if (user.Roles.Contains(role))
            {
                user.Roles.Remove(role);
            }
            else
            {
                user.Roles.Add(role);
            }

            await _dataContext.SaveChangesAsync();
            return RedirectToAction("ManageRoles");
        }



        [HttpPost]
        public async Task<IActionResult> CreateRole(Roles role)
        {
            var r = new Roles() { Name = role.Name };
            await _dataContext.Roles.AddAsync(r);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction("ManageRoles");
        }
    }

}
