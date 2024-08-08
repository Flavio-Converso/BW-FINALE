using Microsoft.AspNetCore.Mvc;
using Team_5.Models.Pharmacy;
using Team_5.Services.Interfaces;

namespace Team_5.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productSvc;

        public ProductController(IProductService productService)
        {
            _productSvc = productService;
        }

        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            var companies = await _productSvc.GetAllCompanies();
            var drawers = await _productSvc.GetAllDrawers();
            ViewBag.Companies = companies;
            ViewBag.Drawers = drawers;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(Products products)
        {
            await _productSvc.CreateProducts(products);
            return RedirectToAction("ProductList", "Product");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productSvc.DeleteProduct(id);
            return RedirectToAction("ProductList");
        }

        [HttpGet]
        public async Task<IActionResult> ProductList()
        {
            var products = await _productSvc.GetAllProducts();
            return View(products);
        }

        [HttpGet("Product/FindLockers")]
        public async Task<IActionResult> FindLockers(int id)
        {
            var product = await _productSvc.FindLockers(id);
            return Ok(product);
        }

        [HttpGet("Product/GetProductsFromDate")]
        public async Task<IActionResult> GetProductsFromDate(DateTime date)
        {
            var list = await _productSvc.GetProductsFromDate(date);
            return Ok(list);
        }

        [HttpGet("Product/GetProductsFromCF")]
        public async Task<IActionResult> GetProductsFromCF(string cf)
        {
            var list = await _productSvc.GetProductsFromCF(cf);
            return Ok(list);
        }
    }
}
