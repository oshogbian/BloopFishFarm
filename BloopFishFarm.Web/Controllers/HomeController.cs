using System.Diagnostics;
using System.Threading.Tasks;
using BloopFishFarm.Core.Interfaces;
using BloopFishFarm.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace BloopFishFarm.Web.Controllers
{
    public class HomeController : Controller
    {
         public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }
        private readonly IFishService _fishService;

        public HomeController(IFishService fishService)
        {
            _fishService = fishService;
        }

        public async Task<IActionResult> Index()
        {
            var fishProducts = await _fishService.GetAllFishAsync();
            return View(fishProducts);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}