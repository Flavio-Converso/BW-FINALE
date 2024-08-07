using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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


        public async Task<IActionResult> CreateProduct()
        {
            var companies = await _dataContext.Companies.ToListAsync();
            ViewBag.Companies = companies;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateProduct(Products products)
        {
            await _productService.CreateProducts(products);
            return RedirectToAction("ProductList", "Product");
        }


        [HttpGet]
        public async Task<IActionResult> ProductList()
        {
            var products = await _productService.GetAllProducts();
            return View(products);
        }        
    }
}
