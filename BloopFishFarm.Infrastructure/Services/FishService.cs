using System.Collections.Generic;
using System.Threading.Tasks;
using BloopFishFarm.Core.Interfaces;
using BloopFishFarm.Core.Models;

namespace BloopFishFarm.Infrastructure.Services
{
    public class FishService : IFishService
    {
        private readonly IFishRepository _fishRepository;

        public FishService(IFishRepository fishRepository)
        {
            _fishRepository = fishRepository;
        }

        public async Task<IEnumerable<Fish>> GetAllFishAsync()
        {
            return await _fishRepository.GetAllFishAsync();
        }

        public async Task<Fish> GetFishByIdAsync(int id)
        {
            return await _fishRepository.GetFishByIdAsync(id);
        }

        public async Task<IEnumerable<Fish>> GetFishByTypeAsync(string type)
        {
            return await _fishRepository.GetFishByTypeAsync(type);
        }

        public async Task AddFishAsync(Fish fish)
        {
            await _fishRepository.AddFishAsync(fish);
        }

        public async Task UpdateFishAsync(Fish fish)
        {
            await _fishRepository.UpdateFishAsync(fish);
        }

        public async Task DeleteFishAsync(int id)
        {
            await _fishRepository.DeleteFishAsync(id);
        }

        public decimal CalculatePriceForWeight(Fish fish, decimal weight)
        {
            return fish.PricePerKg * weight;
        }
    }
} 