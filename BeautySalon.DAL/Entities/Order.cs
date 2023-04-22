using BeautySalon.DAL.Entities.Base;

namespace BeautySalon.DAL.Entities
{
    public class Order : Entity
    {
        private int iD;
        private string? orderName;
        private int? clientID;
        private decimal? price;
        private Client? client;
        private ICollection<Service>? services;



        public int ID { get => iD; set => Set(ref iD, value); }
        public string? OrderName { get => orderName; set => Set(ref orderName, value); }
        public int? ClientID { get => clientID; set => Set(ref clientID, value); }
        public decimal? Price { get => price; set => Set(ref price, value); }


        public virtual Client? Client { get => client; set => Set(ref client, value); }
        public virtual ICollection<Service>? Services { get => services; set => Set(ref services, value); }
    }
}
