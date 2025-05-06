using System.Collections.Generic;
using System.Threading.Tasks;
using BloopFishFarm.Core.Models;

namespace BloopFishFarm.Core.Interfaces
{
    public interface IFishRepository
    {
        Task<IEnumerable<Fish>> GetAllFishAsync();
        Task<Fish> GetFishByIdAsync(int id);
        Task<IEnumerable<Fish>> GetFishByTypeAsync(string type);
        Task AddFishAsync(Fish fish);
        Task UpdateFishAsync(Fish fish);
        Task DeleteFishAsync(int id);
    }
}