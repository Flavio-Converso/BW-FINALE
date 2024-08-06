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
        public ProductController(IProductService productService, DataContext dataContext)
        {
            _productService = productService;
            _dataContext = dataContext;
        }


        public IActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(Products products)
        {
            await _productService.CreateProducts(products);
            return RedirectToAction("Index", "Home");
        }
    }
}
