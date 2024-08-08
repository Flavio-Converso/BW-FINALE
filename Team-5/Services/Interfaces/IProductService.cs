using Team_5.Models.Pharmacy;

namespace Team_5.Services.Interfaces
{
    public interface IProductService
    {
        Task<Products> CreateProducts(Products products);
        Task<List<Products>> GetAllProducts();
        Task<List<Companies>> GetAllCompanies();
        Task<List<Drawers>> GetAllDrawers();
        Task<Products> FindLockers(int id);
        Task<List<Orders>> GetProductsFromDate(DateTime date);
        Task<List<Orders>> GetProductsFromCF(string cf);
    }
}
