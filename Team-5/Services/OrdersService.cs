using Microsoft.EntityFrameworkCore;
using Team_5.Context;
using Team_5.Models.Pharmacy;
using Team_5.Services.Interfaces;

namespace Team_5.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly DataContext _ctx;

        public OrdersService(DataContext dataContext)
        {
            _ctx = dataContext;
        }

        public async Task<Orders> CreateOrder(Orders orders, string cf)
        {
            var product = await _ctx.Products.FirstOrDefaultAsync(p => p.IdProduct == orders.IdProduct);
            var owner = await _ctx.Owners.FirstOrDefaultAsync(o => o.CF == cf);

            var order = new Orders
            {
                OrderDate = orders.OrderDate,
                OrderQuantity = orders.OrderQuantity,
                PrescriptionNumber = orders.PrescriptionNumber,
                IdOwner = owner?.IdOwner ?? 0,
                IdProduct = product?.IdProduct ?? 0
            };

            await _ctx.Orders.AddAsync(order);
            await _ctx.SaveChangesAsync();

            return order;
        }

        public async Task<List<Orders>> GetOrders()
        {
            return await _ctx.Orders
                .Include(o => o.Product)
                .Include(o => o.Owner)
                .ToListAsync();
        }

        public async Task<List<Products>> GetAllProducts()
        {
            return await _ctx.Products.ToListAsync();
        }
    }
}
