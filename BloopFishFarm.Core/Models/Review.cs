// BloopFishFarm.Core/Models/Review.cs
using System;
using System.ComponentModel.DataAnnotations;

namespace BloopFishFarm.Core.Models
{
    public class Review
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }
        
        [Required]
        public string ProductPurchased { get; set; }
        
        [Required]
        public string ReviewText { get; set; }
        
        public bool IsApproved { get; set; } = false;
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}