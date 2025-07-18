﻿namespace ProductDemo.Models
{
    public class Product : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
    }
}
