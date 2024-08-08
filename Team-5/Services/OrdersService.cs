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

            if (owner == null)
            {
                return null;
            }

            if (product == null)
            {
                return null;
            }

            var order = new Orders
            {
                OrderDate = orders.OrderDate,
                OrderQuantity = orders.OrderQuantity,
                PrescriptionNumber = orders.PrescriptionNumber,
                IdOwner = owner.IdOwner,
                IdProduct = product.IdProduct
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

        public async Task<Orders> DeleteOrders(int id)
        {
            // Cerca l'ordine nel database utilizzando l'id fornito
            var order = await _ctx.Orders.FindAsync(id);

            // Se l'ordine non esiste, potresti voler restituire null o lanciare un'eccezione
            if (order == null)
            {
                // Gestione del caso in cui l'ordine non esiste
                return null;
            }

            // Rimuovi l'ordine dal contesto
            _ctx.Orders.Remove(order);

            // Salva le modifiche nel database
            await _ctx.SaveChangesAsync();

            // Restituisci l'ordine eliminato
            return order;
        }
    }
}
