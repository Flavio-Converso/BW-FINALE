using Microsoft.AspNetCore.Mvc;
using Team_5.Context;
using Team_5.Models.Clinic;
using Team_5.Models.Pharmacy;
using Team_5.Services;
using Team_5.Services.Interfaces;

namespace Team_5.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrdersService _ordersService;
        private readonly DataContext _dataContext;

        public OrdersController(IOrdersService ordersService)
        {
            _ordersService = ordersService;

        }

        public IActionResult CreateOrder()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(Orders o, string cf)
        {
            var order = await _ordersService.CreateOrder(o, cf);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<ActionResult<List<Examinations>>> OrderList()
        {
            var orderList = await _ordersService.GetOrders();
            return View(orderList);
        }
    }
}
