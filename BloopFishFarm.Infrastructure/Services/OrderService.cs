using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BloopFishFarm.Core.Models;
using BloopFishFarm.Core.Services;

namespace BloopFishFarm.Infrastructure.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IWhatsAppService _whatsAppService;

        public OrderService(IOrderRepository orderRepository, IWhatsAppService whatsAppService)
        {
            _orderRepository = orderRepository;
            _whatsAppService = whatsAppService;
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _orderRepository.GetOrderByIdAsync(id);
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetAllOrdersAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByCustomerPhoneAsync(string phone)
        {
            return await _orderRepository.GetOrdersByCustomerPhoneAsync(phone);
        }

        public async Task<Order> CreateOrderAsync(OrderViewModel model, List<Fish> fishItems)
        {
            var order = new Order
            {
                CustomerName = model.CustomerName,
                CustomerPhone = model.CustomerPhone,
                CustomerEmail = model.CustomerEmail,
                DeliveryAddress = model.DeliveryAddress,
                IsPickup = model.IsPickup,
                OrderDate = DateTime.Now,
                OrderStatus = "Pending",
                Items = new List<OrderItem>(),
                TotalAmount = 0
            };

            foreach (var item in model.Items)
            {
                var fish = fishItems.Find(f => f.Id == item.FishId);
                if (fish != null)
                {
                    var orderItem = new OrderItem
                    {
                        FishId = fish.Id,
                        Fish = fish,
                        Weight = item.Weight,
                        PricePerKg = fish.PricePerKg,
                        TotalPrice = item.Weight * fish.PricePerKg
                    };
                    
                    order.Items.Add(orderItem);
                    order.TotalAmount += orderItem.TotalPrice;
                }
            }

            await _orderRepository.AddOrderAsync(order);
            
            // Send WhatsApp notification
            await _whatsAppService.SendOrderConfirmationAsync(order);
            
            return order;
        }

        public async Task UpdateOrderStatusAsync(int orderId, string status)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order != null)
            {
                order.OrderStatus = status;
                await _orderRepository.UpdateOrderAsync(order);
                
                // Send WhatsApp notification about status update
                await _whatsAppService.SendOrderStatusUpdateAsync(order);
            }
        }

        public async Task DeleteOrderAsync(int id)
        {
            await _orderRepository.DeleteOrderAsync(id);
        }
    }
}
