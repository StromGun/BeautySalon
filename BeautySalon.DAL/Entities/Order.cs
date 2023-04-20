namespace BeautySalon.DAL.Entities
{
    public class Order
    {
        public int ID { get; set; }
        public string? OrderName { get; set; }
        public int? ClientID { get; set; }
        public decimal? Price { get; set; }


        public virtual Client? Client { get; set; }
        public virtual ICollection<Service>? Services { get; set; }
    }
}
