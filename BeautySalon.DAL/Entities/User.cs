using BeautySalon.DAL.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace BeautySalon.DAL.Entities
{
    public class User : Entity
    {
        public int Id { get; set; }
        [Required]
        public string? Login { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
