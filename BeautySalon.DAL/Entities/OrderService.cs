using BeautySalon.DAL.Entities.Base;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeautySalon.DAL.Entities
{
    public class OrderService : Entity
    {
        private Order? order;
        private Service? service;
        private int amount;
        private decimal? price;
        private decimal? discount;
        private decimal? totalPrice;

        [Required]
        public Order? Order { get => order; set => Set(ref order,value); }
        [Required]
        public Service? Service { get => service; set => Set(ref service,value); }

        public int Amount { get => amount; set => Set(ref amount, value); }

        [Column(TypeName = "money")]
        public decimal? Price { get => Service?.Price; set => Set(ref price, value); }
        [Column(TypeName = "decimal(3,2)")]
        [Required]
        public decimal? Discount { get => discount; set => Set(ref discount, value); }
        [Column(TypeName = "money")]
        public decimal? TotalPrice { get => Amount * Price - Amount * Price * Discount; set => Set(ref totalPrice, value); }
    }
}
