using Microsoft.EntityFrameworkCore;
using Team_5.Context;
using Team_5.Models.Pharmacy;
using Team_5.Services.Interfaces;

namespace Team_5.Services
{
    public class ProductService : IProductService
    {
        private readonly DataContext _ctx;

        public ProductService(DataContext dataContext)
        {
            _ctx = dataContext;
        }

        public async Task<Products> CreateProducts(Products products)
        {
            var company = await _ctx.Companies.FirstOrDefaultAsync(c => c.IdCompany == products.Company.IdCompany);

            Drawers drawer = null;

            if (products.Drawers != null && products.Drawers.IdDrawer > 0)
            {
                drawer = await _ctx.Drawers.FirstOrDefaultAsync(d => d.IdDrawer == products.Drawers.IdDrawer);
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

            _ctx.Products.Add(product);
            await _ctx.SaveChangesAsync();
            return product;
        }

        public async Task<List<Companies>> GetAllCompanies()
        {
            return await _ctx.Companies.ToListAsync();
        }

        public async Task<List<Drawers>> GetAllDrawers()
        {
            return await _ctx.Drawers.Include(d => d.Lockers).ToListAsync();
        }

        public async Task<List<Products>> GetAllProducts()
        {
            return await _ctx.Products.Include(p => p.Company).ToListAsync();
        }

        public async Task<Products> DeleteProduct(int id)
        {
            var product = await _ctx.Products.FindAsync(id);
            _ctx.Products.Remove(product);
            await _ctx.SaveChangesAsync();
            return product;
        }

        public async Task<Products> FindLockers(int id)
        {
            return await _ctx.Products
                .Where(p => p.IdProduct == id)
                .Include(p => p.Drawers)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Orders>> GetProductsFromDate(DateTime date)
        {
            return await _ctx.Orders
                .Include(o => o.Product)
                .Where(o => o.OrderDate.Date == date.Date && o.Product.Type == "Farmaco")
                .ToListAsync();
        }

        public async Task<List<Orders>> GetProductsFromCF(string cf)
        {
            return await _ctx.Orders
                .Include(o => o.Owner).Include(o => o.Product)
                .Where(o => o.Owner.CF == cf)
                .ToListAsync();
        }
    }
}
