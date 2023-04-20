using System.ComponentModel.DataAnnotations;

namespace BeautySalon.DAL.Entities
{
    public enum Gender
    {
        М = 0,
        Ж = 1
    }

    public class Client
    {
        public int ID { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? FirstName { get; set; }
        public string? Patronymic { get; set; }

        public Gender? Gender { get; set; }

        public string? Phone { get; set; }
        public string? Email { get; set; }

        public DateTime? BirthDay { get; set; }
        public byte[]? Image { get; set; }

        public virtual ICollection<Order>? Orders { get; set; }
    }
}
