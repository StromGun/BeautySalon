using System.Windows;
using BeautySalon.DAL.Entities;
using BeautySalon.Services.Interfaces;
using BeautySalon.ViewModels;
using BeautySalon.Views.Windows;

namespace BeautySalon.Services
{
    internal class UserDialogService : IUserDialog
    {
      
        public bool EditClient(Client? client)
        {
            if (client == null) return false;
            var client_editor_model = new EditClientViewModel(client);
            var client_editor_window = new EditClientWindow()
            {
                DataContext = client_editor_model,
                Owner = App.ActiveWindow
            };

            if (client_editor_window.ShowDialog() != true) 
                return false;

            client.ID = client_editor_model.Client.ID;
            client.LastName = client_editor_model.Client.LastName;
            client.FirstName = client_editor_model.Client.FirstName;
            client.Patronymic = client_editor_model.Client.Patronymic;
            client.Status = client_editor_model.Client.Status;
            client.Email = client_editor_model.Client.Email;
            client.Phone = client_editor_model.Client.Phone;
            client.BirthDay = client_editor_model.Client.BirthDay;

            return true;
        }


        public bool ConfirmedInformation(string Information, string Caption) => MessageBox
            .Show(
            Information, Caption,
            MessageBoxButton.YesNo,
            MessageBoxImage.Information)
            == MessageBoxResult.Yes;

        public bool ConfirmError(string Error, string Caption) => MessageBox
             .Show(
            Error, Caption,
            MessageBoxButton.YesNo,
            MessageBoxImage.Warning)
            == MessageBoxResult.Yes;

        public bool ConfirmedWarning(string Warning, string Caption) => MessageBox
             .Show(
            Warning, Caption,
            MessageBoxButton.YesNo,
            MessageBoxImage.Error)
            == MessageBoxResult.Yes;
    }
}
