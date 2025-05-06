// BloopFishFarm.Web/Controllers/ReviewController.cs
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BloopFishFarm.Core.Interfaces;
using BloopFishFarm.Core.Models;
using Microsoft.AspNetCore.Authorization;

namespace BloopFishFarm.Web.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        // GET: /Review/Contact
        public async Task<IActionResult> Contact()
        {
            var approvedReviews = await _reviewService.GetApprovedReviewsAsync();
            ViewBag.Reviews = approvedReviews;
            return View();
        }

        // POST: /Review/SubmitReview
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitReview(Review review)
        {
            if (ModelState.IsValid)
            {
                await _reviewService.AddReviewAsync(review);
                return Json(new { success = true, message = "Thank you! Your review has been submitted and will appear after moderation." });
            }
            
            return Json(new { success = false, message = "Please fill out all required fields correctly." });
        }

        // GET: /Review/Manage (Admin)
        [Authorize] // Add appropriate roles if you have them
        public async Task<IActionResult> Manage()
        {
            var allReviews = await _reviewService.GetAllReviewsAsync();
            return View(allReviews);
        }

        // POST: /Review/Approve/5 (Admin)
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(int id)
        {
            await _reviewService.ApproveReviewAsync(id);
            return RedirectToAction(nameof(Manage));
        }

        // POST: /Review/Delete/5 (Admin)
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _reviewService.DeleteReviewAsync(id);
            return RedirectToAction(nameof(Manage));
        }
    }
}