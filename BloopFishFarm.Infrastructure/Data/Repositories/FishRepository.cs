using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloopFishFarm.Core.Interfaces;
using BloopFishFarm.Core.Models;
using BloopFishFarm.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BloopFishFarm.Infrastructure.Data.Repositories
{
    public class FishRepository : IFishRepository
    {
        private readonly ApplicationDbContext _context;

        public FishRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Fish>> GetAllFishAsync()
        {
            return await _context.Fish.ToListAsync();
        }

        public async Task<Fish> GetFishByIdAsync(int id)
        {
            return await _context.Fish.FindAsync(id);
        }

        public async Task<IEnumerable<Fish>> GetFishByTypeAsync(string type)
        {
            return await _context.Fish
                .Where(f => f.Type == type)
                .ToListAsync();
        }

        public async Task AddFishAsync(Fish fish)
        {
            await _context.Fish.AddAsync(fish);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateFishAsync(Fish fish)
        {
            _context.Fish.Update(fish);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFishAsync(int id)
        {
            var fish = await _context.Fish.FindAsync(id);
            if (fish != null)
            {
                _context.Fish.Remove(fish);
                await _context.SaveChangesAsync();
            }
        }
    }
}