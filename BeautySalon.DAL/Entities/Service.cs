using BeautySalon.DAL.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeautySalon.DAL.Entities
{
    public class Service : Entity
    {
        private int iD;
        private string? name;
        private decimal price;
        private TimeSpan? time;
        private ICollection<OrderService>? orderService;
        private ICollection<Order>? orders;
        private ICollection<Product>? products;
        private ServiceType? serviceType;
        

        public int ID { get => iD; set => Set(ref iD, value); }
        [Required]
        public string? Name { get => name; set => Set(ref name, value); }
        [Column(TypeName ="money")]
        public decimal Price { get => price; set => Set(ref price, value); }
        public TimeSpan? Time { get => time; set => Set(ref time,value); }

        public virtual ICollection<OrderService>? OrderService { get => orderService; set => Set(ref orderService, value); }
        public virtual ICollection<Product>? Products { get => products; set => Set(ref products, value); }

        public ServiceType? ServiceType { get => serviceType; set => Set(ref serviceType,value); }
        public ICollection<Order>? Orders { get => orders; set => Set(ref orders, value); }
    }
}
