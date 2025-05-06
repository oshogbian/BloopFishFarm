using System;
namespace BloopFishFarm.Core.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int FishId { get; set; }
        public Fish Fish { get; set; }
        public decimal Weight { get; set; }
        public decimal PricePerKg { get; set; }
        public decimal TotalPrice { get; set; }
    }
}