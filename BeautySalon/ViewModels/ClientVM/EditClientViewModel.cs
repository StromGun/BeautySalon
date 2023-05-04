using BeautySalon.Commands;
using BeautySalon.DAL.Entities;
using BeautySalon.Services.Interfaces;
using BeautySalon.ViewModels.Base;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace BeautySalon.ViewModels
{
    internal class EditClientViewModel : ViewModel
    {
        private readonly IUserDialog userDialog = App.Services.GetRequiredService<IUserDialog>();

        public Client Client { get; set; }


        private RelayCommand? openImage;
        public RelayCommand? OpenImageCmd => openImage ??= new(obj => OpenImageDialog());
        private void OpenImageDialog()
        {
            string path = @"C:\";
            if (!userDialog.OpenFileDialog(ref path)) return;

            byte[] img = File.ReadAllBytes(path);
  
            Client.Image = img;
                //new ImageSourceConverter().ConvertFromString(path);
        }

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
