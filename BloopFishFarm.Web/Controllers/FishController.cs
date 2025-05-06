using System.Threading.Tasks;
using BloopFishFarm.Core.Interfaces;
using BloopFishFarm.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace BloopFishFarm.Web.Controllers
{
    public class FishController : Controller
    {
        private readonly IFishService _fishService;

        public FishController(IFishService fishService)
        {
            _fishService = fishService;
        }

        public async Task<IActionResult> List(string type = null)
        {
            if (string.IsNullOrEmpty(type))
            {
                var fishProducts = await _fishService.GetAllFishAsync();
                return View(fishProducts);
            }
            else
            {
                var fishProducts = await _fishService.GetFishByTypeAsync(type);
                return View(fishProducts);
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            var fish = await _fishService.GetFishByIdAsync(id);
            if (fish == null)
            {
                return NotFound();
            }
            
            return View(fish);
        }

        [HttpGet]
        public IActionResult CalculatePrice(int fishId, decimal weight)
        {
            var fish = _fishService.GetFishByIdAsync(fishId).Result;
            if (fish == null)
            {
                return NotFound();
            }
            
            var price = _fishService.CalculatePriceForWeight(fish, weight);
            return Json(new { price });
        }
    }
}