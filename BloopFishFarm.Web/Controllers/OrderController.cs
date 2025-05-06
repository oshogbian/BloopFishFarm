using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloopFishFarm.Core.Interfaces;
using BloopFishFarm.Core.Models;
using BloopFishFarm.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace BloopFishFarm.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderService _orderService;
        private readonly IFishService _fishService;

        public OrderController(OrderService orderService, IFishService fishService)
        {
            _orderService = orderService;
            _fishService = fishService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new OrderViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Get fish details for each item in the order
                var fishIds = model.Items.Select(i => i.FishId).ToList();
                var fishList = new List<Fish>();
                
                foreach (var id in fishIds)
                {
                    var fish = await _fishService.GetFishByIdAsync(id);
                    if (fish != null)
                    {
                        fishList.Add(fish);
                    }
                }

                // Create the order
                var order = await _orderService.CreateOrderAsync(model, fishList);
                
                // Redirect to order confirmation
                return RedirectToAction("Confirmation", new { id = order.Id });
            }
            
            return View(model);
        }

        public async Task<IActionResult> Confirmation(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            
            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int fishId, decimal weight)
        {
            var fish = await _fishService.GetFishByIdAsync(fishId);
            if (fish == null)
            {
                return NotFound();
            }
            
            // In a real application, you would use a shopping cart service
            // For now, we'll just return the item details to be added via JavaScript
            var totalPrice = _fishService.CalculatePriceForWeight(fish, weight);
            
            var cartItem = new OrderItemViewModel
            {
                FishId = fish.Id,
                FishName = fish.Name,
                FishType = fish.Type,
                Weight = weight,
                PricePerKg = fish.PricePerKg
            };
            
            return Json(new { success = true, item = cartItem });
        }
    }
}