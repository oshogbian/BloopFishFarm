// BloopFishFarm.Core/Interfaces/IReviewService.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using BloopFishFarm.Core.Models;

namespace BloopFishFarm.Core.Interfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<Review>> GetApprovedReviewsAsync();
        Task<IEnumerable<Review>> GetAllReviewsAsync();
        Task<Review> GetReviewByIdAsync(int id);
        Task AddReviewAsync(Review review);
        Task ApproveReviewAsync(int id);
        Task DeleteReviewAsync(int id);
    }
}