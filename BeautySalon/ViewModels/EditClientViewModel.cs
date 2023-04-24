using BeautySalon.DAL.Entities;
using BeautySalon.ViewModels.Base;
using System;
using System.Runtime.CompilerServices;

namespace BeautySalon.ViewModels
{
    internal class EditClientViewModel : ViewModel
    {
        public Client Client { get; set; } = new();


        public EditClientViewModel(Client? client)
        {

            Client.ID = client!.ID;
            Client.FirstName = client.FirstName;
            Client.LastName = client.LastName;
            Client.Email = client.Email;
            Client.Phone = client.Phone;
            Client.Patronymic = client.Patronymic;
            Client.Status = client.Status;
            Client.BirthDay = client.BirthDay;
            Client.Image = client.Image;

        }
    }
}
