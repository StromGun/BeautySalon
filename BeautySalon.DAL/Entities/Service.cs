﻿namespace BeautySalon.DAL.Entities
{
    public class Service
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }

        public virtual ICollection<Order>? Orders { get; set; }
    }
}
