using Microsoft.AspNetCore.Mvc;
using Team_5.Context;
using Team_5.Models.Pharmacy;
using Team_5.Services.Interfaces;

namespace Team_5.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly DataContext _dataContext;
        public ProductController (IProductService productService,DataContext dataContext)
        {
            _productService = productService;
            _dataContext = dataContext;
        }

        [HttpGet("CreateProduct")]
        public IActionResult CreateProduct()
        {
            return View();
        }
        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct(Products products)
        {
            if (!ModelState.IsValid)
            {
                // Gestisci errori di validazione
                return View(products);
            }

            try
            {
                var product = await _productService.CreateProducts(products);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                // Gestisci l'eccezione (puoi loggare l'errore e mostrare un messaggio all'utente)
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(products);
            }
        }
    }
}
