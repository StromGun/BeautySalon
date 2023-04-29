using BeautySalon.DAL.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeautySalon.DAL.Entities
{
    public class Order : Entity
    {
        private int iD;
        private string? orderName;
        private decimal? totalPrice;
        private DateTime? dateStart;
        private TimeSpan? timeEnd;

        private Client? client;
        private ICollection<OrderService>? orderServices;
        private ICollection<Service>? services;



        public int ID { get => iD; set => Set(ref iD, value); }
        [Required]
        public string? OrderName { get => orderName; set => Set(ref orderName, value); }
        [Column(TypeName = "money")]
        public decimal? TotalPrice { get => totalPrice; set => Set(ref totalPrice, value); }

        [Required]
        public DateTime? DateStart { get => dateStart; set => Set(ref dateStart, value); }
        [Required]
        public TimeSpan? TimeEnd { get => timeEnd; set => Set(ref timeEnd,value); }
        [Required]
        public Client? Client { get => client; set => Set(ref client, value); }

        public virtual ICollection<OrderService>? OrderServices { get => orderServices; set => Set(ref orderServices, value); }
        public ICollection<Service>? Services { get => services; set => Set(ref services,value); }
    }
}
