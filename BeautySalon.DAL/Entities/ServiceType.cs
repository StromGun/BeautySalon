using BeautySalon.DAL.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace BeautySalon.DAL.Entities
{
    public class ServiceType : Entity
    {
        private int id;
        private string? name;

        public int Id { get => id; set => Set(ref id,value); }
        [Required]
        public string? Name { get => name; set => Set(ref name,value); }

        public ICollection<Service>? Services { get; set; }
    }
}
