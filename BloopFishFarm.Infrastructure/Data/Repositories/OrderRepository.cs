using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloopFishFarm.Core.Models;
using BloopFishFarm.Core.Services;
using BloopFishFarm.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BloopFishFarm.Infrastructure.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Fish)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Fish)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByCustomerPhoneAsync(string phone)
        {
            return await _context.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Fish)
                .Where(o => o.CustomerPhone == phone)
                .ToListAsync();
        }

        public async Task AddOrderAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }
    }
}