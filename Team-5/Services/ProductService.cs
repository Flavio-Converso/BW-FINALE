using Microsoft.EntityFrameworkCore;
using Team_5.Context;
using Team_5.Models.Pharmacy;
using Team_5.Services.Interfaces;

namespace Team_5.Services
{
    public class ProductService : IProductService
    {
        private readonly DataContext _dataContext;

        public ProductService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Products> CreateProducts(Products products)
        {
            var company = await _dataContext.Companies.FirstOrDefaultAsync(c => c.IdCompany == products.Company.IdCompany);

            Drawers drawer = null;

            // Check if IdDrawer is greater than 0
            if (products.Drawers != null && products.Drawers.IdDrawer > 0)
            {
                drawer = await _dataContext.Drawers.FirstOrDefaultAsync(d => d.IdDrawer == products.Drawers.IdDrawer);
            }

            var product = new Products
            {
                ProductName = products.ProductName,
                Type = products.Type,
                Use = products.Use,
                Quantity = products.Quantity,
                Availability = products.Availability,
                Company = company,
                Drawers = drawer,
            };

            _dataContext.Products.Add(product);
            await _dataContext.SaveChangesAsync();
            return product;
        }

        public async Task<List<Companies>> GetAllCompanies()
        {
            return await _dataContext.Companies.ToListAsync();
        }

        public async Task<List<Drawers>> GetAllDrawers()
        {
            return await _dataContext.Drawers.Include(d => d.Lockers).ToListAsync();
        }

        public async Task<List<Products>> GetAllProducts()
        {
            return await _dataContext.Products.Include(p => p.Company).ToListAsync();
        }

        public async Task<Products> FindLockers(int id)
        {
            return await _dataContext.Products
                .Where(p => p.IdProduct == id)
                .Include(p => p.Drawers)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Orders>> GetProductsFromDate(DateTime date)
        {
            return await _dataContext.Orders
                .Include(o => o.Product)
                .Where(o => o.OrderDate.Date == date.Date && o.Product.Type == "Farmaco")
                .ToListAsync();
        }

        public async Task<List<Orders>> GetProductsFromCF(string cf)
        {
            return await _dataContext.Orders
                .Include(o => o.Owner).Include(o => o.Product)
                .Where(o => o.Owner.CF == cf)
                .ToListAsync();
        }
    }
}
