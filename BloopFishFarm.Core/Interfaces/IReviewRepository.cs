// BloopFishFarm.Core/Interfaces/IReviewRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using BloopFishFarm.Core.Models;

namespace BloopFishFarm.Core.Interfaces
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetApprovedReviewsAsync();
        Task<IEnumerable<Review>> GetAllReviewsAsync();
        Task<Review> GetReviewByIdAsync(int id);
        Task AddReviewAsync(Review review);
        Task UpdateReviewAsync(Review review);
        Task DeleteReviewAsync(int id);
    }
}