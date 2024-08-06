using Microsoft.EntityFrameworkCore;
using Team_5.Context;
using Team_5.Models.Pharmacy;
using Team_5.Services.Interfaces;

namespace Team_5.Services
{
    public class ProductService : IProductService
    {
        private readonly DataContext _dataContext;
        public ProductService(DataContext dataContext) { _dataContext = dataContext; }
        public async Task<Products> CreateProducts(Products products)
        {
            if (products.Company == null || products.Company.IdCompany == 0)
            {
                throw new ArgumentException("Invalid company information.");
            }

            var company = await _dataContext.Companies.FirstOrDefaultAsync(c => c.IdCompany == products.Company.IdCompany);
            if (company == null)
            {
                throw new InvalidOperationException("Company not found.");
            }

            var product = new Products
            {
                ProductName = products.ProductName,
                Type = products.Type,
                Use = products.Use,
                Quantity = products.Quantity,
                Availability = false,
                Company = company
            };

            _dataContext.Products.Add(product);
            await _dataContext.SaveChangesAsync();
            return product;
        }

        public async Task<List<Companies>> GetAllCompanies()
        {
            return await _dataContext.Companies.ToListAsync();
        }

        public Task<List<Products>> GetAllProducts()
        {
            throw new NotImplementedException();
        }
    }
}
