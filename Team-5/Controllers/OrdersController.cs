using Microsoft.AspNetCore.Mvc;
using Team_5.Models.Pharmacy;
using Team_5.Services.Interfaces;

namespace Team_5.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrdersService _ordersSvc;

        public OrdersController(IOrdersService ordersService)
        {
            _ordersSvc = ordersService;
        }

        public async Task<IActionResult> CreateOrder()
        {
            ViewBag.Products = await _ordersSvc.GetAllProducts();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(Orders o, string cf)
        {
            var order = await _ordersSvc.CreateOrder(o, cf);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<ActionResult<List<Orders>>> OrderList()
        {
            var orderList = await _ordersSvc.GetOrders();
            return View(orderList);
        }
    }
}
