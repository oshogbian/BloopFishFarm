using System.Collections.Generic;
using System.Threading.Tasks;
using BloopFishFarm.Core.Models;

namespace BloopFishFarm.Core.Services
{
    public interface IOrderRepository
    {
        Task<Order> GetOrderByIdAsync(int id);
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<IEnumerable<Order>> GetOrdersByCustomerPhoneAsync(string phone);
        Task AddOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(int id);
    }
}
