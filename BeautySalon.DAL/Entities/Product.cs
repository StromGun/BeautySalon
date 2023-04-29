using BeautySalon.DAL.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeautySalon.DAL.Entities
{
    public class Product : Entity
    {
        private int id;
        private string? name;
        private string? description;
        private string? brand;
        private decimal price;
        private ProductType? productType;

        public int Id { get => id; set => Set(ref id, value); }

        [Required]
        public string? Name { get => name; set => name = value; }

        public string? Brand { get => brand; set => Set(ref brand, value); }

        [Column(TypeName = "money")]
        public decimal Price { get => price; set => Set(ref price, value); }

        public string? Description { get => description; set => Set(ref description, value); }

        [Required]
        public ProductType? ProductType { get => productType; set => Set(ref productType, value); }
    }
}
