// BloopFishFarm.Infrastructure/Services/ReviewService.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using BloopFishFarm.Core.Interfaces;
using BloopFishFarm.Core.Models;

namespace BloopFishFarm.Infrastructure.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<IEnumerable<Review>> GetApprovedReviewsAsync()
        {
            return await _reviewRepository.GetApprovedReviewsAsync();
        }

        public async Task<IEnumerable<Review>> GetAllReviewsAsync()
        {
            return await _reviewRepository.GetAllReviewsAsync();
        }

        public async Task<Review> GetReviewByIdAsync(int id)
        {
            return await _reviewRepository.GetReviewByIdAsync(id);
        }

        public async Task AddReviewAsync(Review review)
        {
            await _reviewRepository.AddReviewAsync(review);
        }

        public async Task ApproveReviewAsync(int id)
        {
            var review = await _reviewRepository.GetReviewByIdAsync(id);
            if (review != null)
            {
                review.IsApproved = true;
                await _reviewRepository.UpdateReviewAsync(review);
            }
        }

        public async Task DeleteReviewAsync(int id)
        {
            await _reviewRepository.DeleteReviewAsync(id);
        }
    }
}