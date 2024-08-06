using Team_5.Models.Pharmacy;

namespace Team_5.Services.Interfaces
{
    public interface IProductService
    {
        Task<Products> CreateProducts(Products products);
        Task<List<Products>> GetAllProducts();
        Task<List<Companies>> GetAllCompanies();
    }
}
