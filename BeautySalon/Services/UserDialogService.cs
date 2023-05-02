using System.Collections;
using System.Windows;
using BeautySalon.DAL.Entities;
using BeautySalon.Services.Interfaces;
using BeautySalon.ViewModels;
using BeautySalon.Views.Windows;

namespace BeautySalon.Services
{
    internal class UserDialogService : IUserDialog
    {
      
        public bool EditClient(ref Client? client)
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

            client.LastName = client_editor_model.Client.LastName;
            client.FirstName = client_editor_model.Client.FirstName;
            client.Patronymic = client_editor_model.Client.Patronymic;
            client.Status = client_editor_model.Client.Status;
            client.Email = client_editor_model.Client.Email;
            client.Gender = client_editor_model.Client.Gender;
            client.Phone = client_editor_model.Client.Phone;
            client.BirthDay = client_editor_model.Client.BirthDay;

            return true;
        }

        public bool EditOrder (ref Order? order)
        {
            if (order == null) return false;
            var order_editor_model = new EditOrderViewModel(order);
            var order_editor_window = new EditOrderWindow()
            {
                DataContext = order_editor_model,
                Owner = App.ActiveWindow
            };

            if (order_editor_window.ShowDialog() != true)
                return false;

            order.Client = order_editor_model.Order!.Client;
            order.OrderName = order_editor_model.Order.OrderName;
            order.Status = order_editor_model.Order.Status;
            order.DateStart = order_editor_model.Order.DateStart;
            order.TimeEnd = order_editor_model.Order.TimeEnd;
            order.OrderServices = order_editor_model.OrderServiceBindingList;
            order.TotalPrice = order_editor_model.Order.TotalPrice;

            return true;
        }

        public bool OpenServices(ICollection services)
        {
            var services_model = new ServicesViewModel(services);
            var services_window = new ServicesWindow()
            {
                DataContext = services_model,
                Owner = App.ActiveWindow
            };

            if (services_window.ShowDialog() != true)
                return false;

            return true;
        }

        public bool OpenAboutBox()
        {
            var aboutBox = new AboutWindow();
            aboutBox.ShowDialog();
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
