using Microsoft.EntityFrameworkCore;
using Team_5.Context;
using Team_5.Models.Pharmacy;
using Team_5.Services.Interfaces;

namespace Team_5.Services
{
    public class OrdersService : IOrdersService
    {

        private readonly DataContext _dataContext;
        public OrdersService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<Orders> CreateOrder(Orders orders, string cf)
        {
            var product = await _dataContext.Products.FirstOrDefaultAsync(p => p.IdProduct == orders.IdProduct);
            var owner = await _dataContext.Owners.FirstOrDefaultAsync(o => o.CF == cf);
            var order = new Orders
            {
                OrderDate = orders.OrderDate,
                OrderQuantity = orders.OrderQuantity,
                PrescriptionNumber = orders.PrescriptionNumber,
                IdOwner = owner.IdOwner,
                IdProduct = orders.IdProduct,
            };
            await _dataContext.Orders.AddAsync(order);
            _dataContext.SaveChanges();
            return order;
        }
    }
}
