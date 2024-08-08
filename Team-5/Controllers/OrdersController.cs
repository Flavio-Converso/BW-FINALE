using Microsoft.AspNetCore.Mvc;
using Team_5.Context;
using Team_5.Models.Pharmacy;
using Team_5.Services.Interfaces;

namespace Team_5.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrdersService _ordersSvc;
        private readonly DataContext _ctx;

        public OrdersController(IOrdersService ordersService, DataContext dataContext)
        {
            _ordersSvc = ordersService;
            _ctx = dataContext;
        }

        public async Task<IActionResult> CreateOrder()
        {
            ViewBag.Products = await _ordersSvc.GetAllProducts();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> CreateOrder(Orders o, string cf)
        {
            var order = await _ordersSvc.CreateOrder(o, cf);

            if (order == null)
            {
                ModelState.AddModelError(string.Empty, "Il codice fiscale o il prodotto fornito non sono validi.");
                ViewBag.Products = await _ordersSvc.GetAllProducts();
                return View(o);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<ActionResult<List<Orders>>> OrderList()
        {
            var orderList = await _ordersSvc.GetOrders();
            return View(orderList);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteOrder(int id)
        {
            var deletedOrder = await _ordersSvc.DeleteOrders(id);

            if (deletedOrder == null)
            {
                // Se l'ordine non esiste, restituisci un 404 Not Found
                return NotFound(new { Message = "Order not found." });
            }

            return RedirectToAction("OrderList");
        }
    }
}
