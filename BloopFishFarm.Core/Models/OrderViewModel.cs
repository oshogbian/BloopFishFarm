using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BloopFishFarm.Core.Models
{
    public class OrderViewModel
    {
        [Required]
        public string CustomerName { get; set; }
        
        [Required]
        [Phone]
        public string CustomerPhone { get; set; }
        
        [Required]
        [EmailAddress]
        public string CustomerEmail { get; set; }
        
        public string DeliveryAddress { get; set; }
        
        public bool IsPickup { get; set; }
        
        public List<OrderItemViewModel> Items { get; set; } = new List<OrderItemViewModel>();
        
        public decimal TotalAmount => Items.Sum(i => i.TotalPrice);
    }

    public class OrderItemViewModel
    {
        public int FishId { get; set; }
        public string FishName { get; set; }
        public string FishType { get; set; }
        public decimal Weight { get; set; }
        public decimal PricePerKg { get; set; }
        public decimal TotalPrice => Weight * PricePerKg;
    }
}