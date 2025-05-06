using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BloopFishFarm.Core.Models;

namespace BloopFishFarm.Core.Services
{
    public class WhatsAppService : IWhatsAppService
    {
        private readonly HttpClient _httpClient;
        private readonly string _whatsAppApiUrl;
        private readonly string _whatsAppApiToken;

        public WhatsAppService(HttpClient httpClient, string whatsAppApiUrl, string whatsAppApiToken)
        {
            _httpClient = httpClient;
            _whatsAppApiUrl = whatsAppApiUrl;
            _whatsAppApiToken = whatsAppApiToken;
        }

        public async Task SendOrderConfirmationAsync(Order order)
        {
            // Format order details for WhatsApp
            var message = FormatOrderConfirmationMessage(order);
            
            // Send message via WhatsApp API
            await SendWhatsAppMessageAsync(order.CustomerPhone, message);
        }

        public async Task SendOrderStatusUpdateAsync(Order order)
        {
            // Format status update message
            var message = $"Your order #{order.Id} status has been updated to: {order.OrderStatus}";
            
            // Send message via WhatsApp API
            await SendWhatsAppMessageAsync(order.CustomerPhone, message);
        }

        private string FormatOrderConfirmationMessage(Order order)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Thank you for your order #{order.Id} from Bloop Fish Farm!");
            sb.AppendLine();
            sb.AppendLine("Order Details:");
            
            foreach (var item in order.Items)
            {
                sb.AppendLine($"- {item.Fish.Name} ({item.Fish.Type}): {item.Weight}kg at ${item.PricePerKg}/kg = ${item.TotalPrice}");
            }
            
            sb.AppendLine();
            sb.AppendLine($"Total Amount: ${order.TotalAmount}");
            sb.AppendLine();
            
            if (order.IsPickup)
            {
                sb.AppendLine("Your order will be available for pickup at our farm.");
            }
            else
            {
                sb.AppendLine($"Delivery Address: {order.DeliveryAddress}");
            }
            
            sb.AppendLine("We will notify you when your order is ready!");
            
            return sb.ToString();
        }

        private async Task SendWhatsAppMessageAsync(string phoneNumber, string message)
        {
            // In a real implementation, this would use the WhatsApp Business API
            // For now, this is a placeholder implementation
            
            var payload = new
            {
                phone = phoneNumber,
                message = message
            };
            
            var content = new StringContent(
                JsonSerializer.Serialize(payload),
                Encoding.UTF8,
                "application/json");
                
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_whatsAppApiToken}");
            
            var response = await _httpClient.PostAsync(_whatsAppApiUrl, content);
            response.EnsureSuccessStatusCode();
        }
    }
}
