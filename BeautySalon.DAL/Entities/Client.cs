using BeautySalon.DAL.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeautySalon.DAL.Entities
{
    public enum Gender
    {
        М = 0,
        Ж = 1
    }

    public enum Status
    {
        Постоянный = 0,
        Потенциальный = 1,
        Потерянный = 2
    }

    public class Client : Entity
    {
        private int iD;
        private string? lastName;
        private string? firstName;
        private string? patronymic;
        private DateTime dateAdded;
        private Gender? gender;
        private string? phone;
        private string? email;
        private Status? status;
        private DateTime? birthDay;
        private byte[]? image;
        private ICollection<Order>? orders;

        public int ID { get => iD; set => Set(ref iD,value); }
        [Required]
        public string? LastName { get => lastName; set => Set(ref lastName,value); }
        [Required]
        public string? FirstName { get => firstName; set => Set(ref firstName, value); }
        public string? Patronymic { get => patronymic; set => Set(ref patronymic, value); }

        public Gender? Gender { get => gender; set => Set(ref gender, value); }
        [Column(TypeName = "Date")]
        public DateTime DateAdded { get => dateAdded; set => Set(ref dateAdded, value); }

        [Phone]
        public string? Phone { get => phone; set => Set(ref phone, value); }
        [EmailAddress]
        public string? Email { get => email; set => Set(ref email, value); }

        public Status? Status { get => status; set => Set(ref status, value); }

        public DateTime? BirthDay { get => birthDay; set => Set(ref birthDay, value); }
        public byte[]? Image { get => image; set => Set(ref image, value); }

        public virtual ICollection<Order>? Orders { get => orders; set => Set(ref orders, value); }
    }
}
