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
            var drawers = await _dataContext.Drawers.Include(d => d.Lockers).ToListAsync();
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
            var product = await _dataContext.Products
            .Where(p => p.IdProduct == id)
            .Include(p => p.Drawers)
            .FirstOrDefaultAsync();

            return Ok(product);
        }

        [HttpGet("Product/GetProductsFromDate")]
        public async Task<IActionResult> GetProductsFromDate(DateTime date)
        {
            var list = await _dataContext.Orders
                .Include(o => o.Product)
                .Where(o => o.OrderDate.Date == date.Date && o.Product.Type == "Farmaco")
                .ToListAsync();

            return Ok(list);
        }

        [HttpGet("Product/GetProductsFromCF")]
        public async Task<IActionResult> GetProductsFromCF(string cf)
        {
            var list = await _dataContext.Orders
                .Include(o => o.Owner).Include(o => o.Product)
                .Where(o => o.Owner.CF == cf)
                .ToListAsync();

            return Ok(list);
        }

    }
}
