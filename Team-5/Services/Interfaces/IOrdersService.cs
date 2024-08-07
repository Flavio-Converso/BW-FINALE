using Team_5.Models.Pharmacy;

namespace Team_5.Services.Interfaces
{
    public interface IOrdersService
    {
        Task<Orders> CreateOrder(Orders orders, string cf);
    }
}
