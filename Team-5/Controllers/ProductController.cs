using Microsoft.AspNetCore.Mvc;
using Team_5.Models.Pharmacy;
using Team_5.Services.Interfaces;

namespace Team_5.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            var companies = await _productService.GetAllCompanies();
            var drawers = await _productService.GetAllDrawers();
            ViewBag.Companies = companies;
            ViewBag.Drawers = drawers;
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

        [HttpGet("Product/FindLockers")]
        public async Task<IActionResult> FindLockers(int id)
        {
            var product = await _productService.FindLockers(id);
            return Ok(product);
        }

        [HttpGet("Product/GetProductsFromDate")]
        public async Task<IActionResult> GetProductsFromDate(DateTime date)
        {
            var list = await _productService.GetProductsFromDate(date);
            return Ok(list);
        }

        [HttpGet("Product/GetProductsFromCF")]
        public async Task<IActionResult> GetProductsFromCF(string cf)
        {
            var list = await _productService.GetProductsFromCF(cf);
            return Ok(list);
        }
    }
}
