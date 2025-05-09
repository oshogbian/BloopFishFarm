namespace BloopFishFarm.Web.Models
{
    public class FishViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal PricePerKg { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
    }
}
