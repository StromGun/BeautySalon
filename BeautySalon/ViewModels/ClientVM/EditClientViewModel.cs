using BeautySalon.DAL.Entities;
using BeautySalon.ViewModels.Base;

namespace BeautySalon.ViewModels
{
    internal class EditClientViewModel : ViewModel
    {
        public Client Client { get; set; }


        public EditClientViewModel(Client? client)
        {
            Client = new()
            {
                ID = client.ID,
                FirstName = client.FirstName,
                LastName = client.LastName,
                Email = client.Email,
                Phone = client.Phone,
                Gender = client.Gender,
                Patronymic = client.Patronymic,
                Status = client.Status,
                BirthDay = client.BirthDay,
                Image = client.Image,
            };

        }
    }
}
