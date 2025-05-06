using System.Threading.Tasks;
using BloopFishFarm.Core.Models;

namespace BloopFishFarm.Core.Services
{
    public interface IWhatsAppService
    {
        Task SendOrderConfirmationAsync(Order order);
        Task SendOrderStatusUpdateAsync(Order order);
    }
}