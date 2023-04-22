using BeautySalon.DAL.Entities.Base;

namespace BeautySalon.DAL.Entities
{
    public class Service : Entity
    {
        private int iD;
        private string? name;
        private decimal? price;
        private ICollection<Order>? orders;

        public int ID { get => iD; set => Set(ref iD, value); }
        public string? Name { get => name; set => Set(ref name, value); }
        public decimal? Price { get => price; set => Set(ref price, value); }

        public virtual ICollection<Order>? Orders { get => orders; set => Set(ref orders, value); }
        public ServiceType? ServiceType { get; set; }
    }
}
