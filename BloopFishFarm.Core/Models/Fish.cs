using System;

namespace BloopFishFarm.Core.Models
{
    public class Fish
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; } // Live Catfish or Smoked Catfish
        public decimal PricePerKg { get; set; }
        public decimal Weight { get; set; }
        public decimal TotalPrice => PricePerKg * Weight;
        public string ImageUrl { get; set; }
        public bool IsAvailable { get; set; }
        public string Description { get; set; }


  public string DisplayImageUrl 
{
    get 
    {
        return Type == "Live" ? "/images/live.png" : 
               Type == "Smoked" ? "/images/smoked.png" : 
               ImageUrl;
    }
}
     }
}
